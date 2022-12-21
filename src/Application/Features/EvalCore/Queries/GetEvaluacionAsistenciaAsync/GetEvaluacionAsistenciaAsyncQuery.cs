using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using EvaluacionCore.Application.Features.EvalCore.Dto;
using EvaluacionCore.Application.Features.Permisos.Dto;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Application.Features.Vacaciones.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.EvalCore.Queries.GetEvaluacionAsistenciaAsync;

public record GetEvaluacionAsistenciaAsyncQuery(string Identificacion, DateTime FechaDesde, DateTime FechaHasta, string Udn, string Area, string Departamento) : IRequest<ResponseType<List<EvaluacionAsistenciaResponseType>>>;

public class GetEvaluacionAsistenciaAsyncHandler : IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>
{
    private readonly IApisConsumoAsync _ApiConsumoAsync;
    private readonly IBitacoraMarcacion _repoBitMarcacionAsync;
    private readonly IRepositoryAsync<Cliente> _repositoryClienteAsync;
    private readonly IRepositoryAsync<ClaseTurno> _repositoryClassAsync;
    private readonly IRepositoryAsync<SubclaseTurno> _repositorySubcAsync;
    private readonly IRepositoryAsync<TipoTurno> _repositoryTurnoAsync;
    private readonly IRepositoryAsync<TurnoColaborador> _repositoryTurnoColAsync;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private string uriEnpoint = "";

    //private readonly ITurnoRepository _repository;


    public GetEvaluacionAsistenciaAsyncHandler(IRepositoryAsync<ClaseTurno> repository, IRepositoryAsync<SubclaseTurno> repositorySubt, IRepositoryAsync<Cliente> repositoryCli,
        IConfiguration config, IRepositoryAsync<TipoTurno> repositoryTurno, IRepositoryAsync<TurnoColaborador> repositoryTurnoCol, IBitacoraMarcacion repoBitMarcacionAsync, IMapper mapper, IApisConsumoAsync apisConsumoAsync)
    {
        _ApiConsumoAsync = apisConsumoAsync;
        _repoBitMarcacionAsync = repoBitMarcacionAsync;
        _repositoryClienteAsync = repositoryCli;
        _repositorySubcAsync = repositorySubt;
        _repositoryTurnoAsync = repositoryTurno;
        _repositoryClassAsync = repository;
        _repositoryTurnoColAsync = repositoryTurnoCol;
        _mapper = mapper;
        _config = config;
    }


    async Task<ResponseType<List<EvaluacionAsistenciaResponseType>>> IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>.Handle(GetEvaluacionAsistenciaAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var uri = "http://10.0.0.8:5208/api/v1/Solicitudes/GetSolicitudesGeneral?FechaDesde=" + request.FechaDesde.ToShortDateString() +
                "&FechaHasta=" + request.FechaHasta.ToShortDateString() + "&Udn=" + request.Udn + "&Area=" + request.Area + "&ScCosto=" + request.Departamento + "&SeleccionTodos=true";

            var objClaseTurno = await _repositoryClassAsync.ListAsync(cancellationToken);
            var objTurnoCol = await _repositoryTurnoColAsync.ListAsync(new TurnoColaboradorTreeSpec(), cancellationToken);
            var objSubclaseTurno = await _repositorySubcAsync.ListAsync(cancellationToken);
            var objTipoTurno = await _repositoryTurnoAsync.ListAsync(cancellationToken);
            var objCliente = await _repositoryClienteAsync.ListAsync(new ClientesByEmpresaSpec(), cancellationToken);
            var (Success, Data) = await _ApiConsumoAsync.GetEndPoint(" ", uri, uriEnpoint);
            Guid estadoAprobado = _config.GetSection("Estados:Aprobada").Get<Guid>();

            List<SolicitudGeneralType> solicitudGeneralType = (List<SolicitudGeneralType>)Data;

            List<TipoJornadaType> listaTipoJornada = new();
            List<ModalidadJornadaType> listaModalidadJornada = new(); 

            List<Cliente> objClienteTemp = new();      
            List<Cliente> objClienteFinal = new();

            List<EvaluacionAsistenciaResponseType> listaEvaluacionAsistencia = new();


            GetBitacoraMarcacionRequest requestMarcacion = new()
            {
                CodUdn = request.Udn,
                CodArea = (request.Area) ?? "",
                CodSubcentro = (request.Departamento) ?? "",
                FechaDesde = request.FechaDesde.ToString("dd/MM/yyyy"),
                FechaHasta = request.FechaHasta.ToString("dd/MM/yyyy"),
                CodMarcacion =  "",
                Suscriptor = request.Identificacion.ToString(),
            };

            var bitacora = await  _repoBitMarcacionAsync.GetBitacoraMarcacionAsync(requestMarcacion);

            //objClienteTemp.AddRange(objCliente.Where(e => e.Cargo.Departamento.Nombre == request.Departamento &&
            //                                         e.Cargo.Departamento.Area.Nombre == request.Area &&
            //                                         e.Cargo.Departamento.Area.Empresa.RazonSocial == request.Udn).ToList());

            for (DateTime dtm = request.FechaDesde; dtm <= request.FechaHasta; dtm = dtm.AddDays(1))
            {
                List<Solicitud> solicitudes = new();
                List<Novedad> novedades = new();
                //novedades.Clear();

                foreach (var item in bitacora)
                {

                    DateTime fechaConsulta = dtm;
                    var u = dtm.ToShortDateString();

                    var colaborador = objCliente.Where(e => e.Identificacion == item.Cedula).FirstOrDefault();

                    if (colaborador == null) continue;

                    var validador = listaEvaluacionAsistencia.Where(e => e.Identificacion == colaborador.Identificacion && e.Fecha == fechaConsulta).FirstOrDefault();

                    if (validador != null) continue;

                    #region consulta y procesamiento de turno laboral

                    var turnoFiltro = objTurnoCol.Where(e => e.FechaAsignacion == fechaConsulta && e.IdColaborador == colaborador.Id && e.Turno.IdTurnoPadre == null).FirstOrDefault();

                    string codMarcacionEntrada = (turnoFiltro?.Turno?.CodigoEntrada?.ToString()) ?? "10";
                    string codMarcacionSalida = (turnoFiltro?.Turno?.CodigoSalida?.ToString()) ?? "11";

                    var marcacionEntradaFiltro = bitacora.Where(e => e.Codigo == colaborador.CodigoConvivencia && e.CodEvento == codMarcacionEntrada && DateTime.Parse(e.Fecha).ToShortDateString() == dtm.ToShortDateString()).FirstOrDefault();
                    var marcacionSalidaFiltro = bitacora.Where(e => e.Codigo == colaborador.CodigoConvivencia && e.CodEvento == codMarcacionSalida && DateTime.Parse(e.Fecha).ToShortDateString() == dtm.ToShortDateString()).OrderByDescending(e => e.Fecha).FirstOrDefault();

                    TurnoLaboral turnoLaboral = new()
                    {
                        Id = turnoFiltro?.Id,
                        Entrada = turnoFiltro?.Turno?.Entrada == null ? fechaConsulta.AddHours(8) : turnoFiltro?.Turno?.Entrada,
                        Salida = turnoFiltro?.Turno?.Salida == null ? fechaConsulta.AddHours(17) : turnoFiltro?.Turno?.Salida,
                        TotalHoras = (turnoFiltro?.Turno?.TotalHoras) ?? "0",
                        MarcacionEntrada = marcacionEntradaFiltro?.Hora != null ? DateTime.Parse(marcacionEntradaFiltro.Hora) : null,
                        MarcacionSalida = marcacionSalidaFiltro?.Hora != null ? DateTime.Parse(marcacionSalidaFiltro.Hora) : null
                    };

                    #endregion


                    #region consulta y procesamiento de turno de receso

                    var subturnoFiltro = objTurnoCol.Where(e => e.FechaAsignacion == dtm && e.IdColaborador == colaborador.Id && e.Turno.IdTurnoPadre != null).FirstOrDefault();

                    string codMarcacionEntradaReceso = (subturnoFiltro?.Turno?.CodigoEntrada?.ToString()) ?? "14";
                    string codMarcacionSalidaReceso = (subturnoFiltro?.Turno?.CodigoEntrada?.ToString()) ?? "15";

                    var marcacionEntradaRecesoFiltro = bitacora.Where(e => e.Codigo == colaborador.CodigoConvivencia && e.CodEvento == codMarcacionEntradaReceso && DateTime.Parse(e.Fecha).ToShortDateString() == dtm.ToShortDateString()).FirstOrDefault();
                    var marcacionSalidaRecesoFiltro = bitacora.Where(e => e.Codigo == colaborador.CodigoConvivencia && e.CodEvento == codMarcacionSalidaReceso && DateTime.Parse(e.Fecha).ToShortDateString() == dtm.ToShortDateString()).OrderByDescending(e => e.Fecha).FirstOrDefault();


                    TurnoReceso turnoReceso = new()
                    {
                        Id = subturnoFiltro?.Id,
                        Entrada = subturnoFiltro?.Turno?.Entrada == null ? fechaConsulta.AddHours(12) : subturnoFiltro?.Turno?.Entrada,
                        Salida = subturnoFiltro?.Turno?.Salida == null ? fechaConsulta.AddHours(14) : subturnoFiltro?.Turno?.Salida,
                        TotalHoras = (subturnoFiltro?.Turno?.TotalHoras) ?? "0",
                        MarcacionEntrada = marcacionEntradaRecesoFiltro?.Hora != null ? DateTime.Parse(marcacionEntradaRecesoFiltro?.Hora) : null,
                        MarcacionSalida = marcacionSalidaRecesoFiltro?.Hora != null ? DateTime.Parse(marcacionSalidaRecesoFiltro?.Hora) : null
                    };

                    #endregion


                    #region Consulta y procesamiento de novedades

                    if (!string.IsNullOrEmpty(marcacionEntradaFiltro?.Novedad))
                    {
                        novedades.Add(new Novedad
                        {
                            Descripcion = marcacionEntradaFiltro.Novedad,
                            MinutosNovedad = marcacionEntradaFiltro.Minutos_Novedad
                        });
                    }

                    if (!string.IsNullOrEmpty(marcacionSalidaFiltro?.Novedad))
                    {
                        novedades.Add(new Novedad
                        {
                            Descripcion = marcacionSalidaFiltro.Novedad,
                            MinutosNovedad = marcacionSalidaFiltro.Minutos_Novedad
                        });
                    }

                    if (!string.IsNullOrEmpty(marcacionEntradaRecesoFiltro?.Novedad))
                    {
                        novedades.Add(new Novedad
                        {
                            Descripcion = marcacionEntradaRecesoFiltro.Novedad,
                            MinutosNovedad = marcacionEntradaRecesoFiltro.Minutos_Novedad
                        });
                    }

                    if (!string.IsNullOrEmpty(marcacionSalidaRecesoFiltro?.Novedad))
                    {
                        novedades.Add(new Novedad
                        {
                            Descripcion = marcacionSalidaRecesoFiltro.Novedad,
                            MinutosNovedad = marcacionSalidaRecesoFiltro.Minutos_Novedad
                        });
                    }

                    #endregion


                    #region Procesamiento de solicitudes

                    int codigo = string.IsNullOrEmpty(colaborador?.CodigoConvivencia) ? 0 : int.Parse(colaborador?.CodigoConvivencia);
                    if (solicitudGeneralType != null)
                    {
                        var solicitudesObj = solicitudGeneralType.Where(e => e.IdBeneficiario == codigo && e.FechaAfectacion?.ToShortDateString() == fechaConsulta.ToShortDateString() && e.IdEstadoSolicitud == estadoAprobado).ToList();
                        if (solicitudesObj.Any())
                        {
                            foreach (var soli in solicitudesObj)
                            {
                                solicitudes.Add(new Solicitud()
                                {
                                    IdSolicitud = soli.Id,
                                    IdTipoSolicitud = Guid.Parse(soli?.IdFeature),
                                    TipoSolicitud = soli.CodigoFeature,
                                    AplicaDescuento = soli.AplicaDescuento
                                });
                            }
                        }
                    }

                    #endregion


                    listaEvaluacionAsistencia.Add(new EvaluacionAsistenciaResponseType()
                    {
                        Colaborador = colaborador.Nombres,
                        Identificacion = item.Cedula,
                        CodBiometrico = colaborador.CodigoConvivencia,
                        Udn = request.Udn,
                        Area = request.Area,
                        SubCentroCosto = request.Departamento,
                        Fecha = dtm,
                        Novedades = novedades,
                        TurnoLaboral = turnoLaboral,
                        TurnoReceso = turnoReceso,
                        Solicitudes = solicitudes
                    });

                }
                
            }

            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        }
        catch (Exception e)
        {
            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }

    }
   
}