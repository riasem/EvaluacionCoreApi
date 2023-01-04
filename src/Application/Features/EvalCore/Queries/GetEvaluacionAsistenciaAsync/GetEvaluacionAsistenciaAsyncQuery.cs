using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using EvaluacionCore.Application.Features.EvalCore.Dto;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.Permisos.Dto;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Application.Features.Vacaciones.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Justificacion;
using EvaluacionCore.Domain.Entities.Permisos;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace EvaluacionCore.Application.Features.EvalCore.Queries.GetEvaluacionAsistenciaAsync;

public record GetEvaluacionAsistenciaAsyncQuery(string Suscriptor, DateTime FechaDesde, DateTime FechaHasta, string Udn, string Area, string Departamento) : IRequest<ResponseType<List<EvaluacionAsistenciaResponseType>>>;

public class GetEvaluacionAsistenciaAsyncHandler : IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>
{
    private readonly IEvaluacion _EvaluacionAsync;
    private readonly IApisConsumoAsync _ApiConsumoAsync;
    private readonly IBitacoraMarcacion _repoBitMarcacionAsync;
    private readonly IRepositoryAsync<SolicitudPermiso> _repositorySoliPermisoAsync;
    private readonly IRepositoryAsync<SolicitudJustificacion> _repositorySoliJustificacionAsync;
    private readonly IRepositoryAsync<Cliente> _repositoryClienteAsync;
    private readonly IRepositoryAsync<TurnoColaborador> _repositoryTurnoColAsync;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly string UrlBaseApiWorkFlow = "";
    private string ConnectionString { get; }



    public GetEvaluacionAsistenciaAsyncHandler(IEvaluacion repository, 
                                                IRepositoryAsync<Cliente> repositoryCli,
                                                IConfiguration config, 
                                                IRepositoryAsync<SolicitudPermiso> repositoryPermiso, 
                                                IRepositoryAsync<SolicitudJustificacion> repositoryJustificacion, 
                                                IRepositoryAsync<TurnoColaborador> repositoryTurnoCol, 
                                                IBitacoraMarcacion repoBitMarcacionAsync, 
                                                IMapper mapper, 
                                                IApisConsumoAsync apisConsumoAsync)
    {
        _EvaluacionAsync = repository;
        _ApiConsumoAsync = apisConsumoAsync;
        _repoBitMarcacionAsync = repoBitMarcacionAsync;
        _repositoryClienteAsync = repositoryCli;
        _repositoryTurnoColAsync = repositoryTurnoCol;
        _repositorySoliPermisoAsync = repositoryPermiso;
        _repositorySoliJustificacionAsync = repositoryJustificacion;
        _mapper = mapper;
        _config = config;
        UrlBaseApiWorkFlow = _config.GetSection("ConsumoApis:UrlBaseApiWorkFlow").Get<string>();
        ConnectionString = _config.GetConnectionString("ConnectionStrings:DefaultConnection");
    }


    async Task<ResponseType<List<EvaluacionAsistenciaResponseType>>> IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>.Handle(GetEvaluacionAsistenciaAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Guid estadoAprobado = _config.GetSection("Estados:Aprobada").Get<Guid>();
            //incluir dentro del recorrido del colaborador 


            //incluir el colaborador en el request (de ser necesario) / adicional el rango de fechas 
            //var objTurnoCol = await _repositoryTurnoColAsync.ListAsync(new TurnoColaboradorTreeSpec(request.FechaDesde,request.FechaHasta), cancellationToken);

            var objCliente = await _repositoryClienteAsync.ListAsync(new ClientesByEmpresaSpec(), cancellationToken);

            List<TipoJornadaType> listaTipoJornada = new();
            List<ModalidadJornadaType> listaModalidadJornada = new(); 
            List<Cliente> objClienteTemp = new();      
            List<Cliente> objClienteFinal = new();
            List<EvaluacionAsistenciaResponseType> listaEvaluacionAsistencia = new();


            //GetBitacoraMarcacionRequest requestMarcacion = new()
            //{
            //    CodUdn = request.Udn,
            //    CodArea = (request.Area) ?? "",
            //    CodSubcentro = (request.Departamento) ?? "",
            //    FechaDesde = request.FechaDesde.ToString("dd/MM/yyyy"),
            //    FechaHasta = request.FechaHasta.ToString("dd/MM/yyyy"),
            //    CodMarcacion =  "",
            //    Suscriptor = request.Suscriptor.ToString(),
            //};
            //var bitacora = await  _repoBitMarcacionAsync.GetBitacoraMarcacionAsync(requestMarcacion);




            List<TurnoColaborador> objTurnoCol;
            List<BitacoraMarcacionType> objMarcacionCol;
            List<SolicitudPermiso> objPermiso;
            SolicitudJustificacion objJustificacion;
            bool poseeTurno = false;
            bool poseeTurnoReceso = false;

            TurnoLaboral turnoLaboral = new();

            List<Solicitud> solicitudes = new();
            List<Novedad> novedades = new();
            TurnoColaborador turnoRecesoFiltro = new();

            var listaColaboradores =  await _EvaluacionAsync.ConsultaColaboradores(request.Udn, request.Area, request.Departamento, request.Suscriptor);

            foreach (var itemCol in listaColaboradores)
            {

                for (DateTime dtm = request.FechaDesde; dtm <= request.FechaHasta; dtm = dtm.AddDays(1))
                {
                    //Se obtiene el turno laboral asignado al colaborador de la fecha en proceso
                    var turnoFiltro = await _repositoryTurnoColAsync.FirstOrDefaultAsync(new TurnoColaboradorTreeSpec(itemCol.Identificacion, dtm), cancellationToken);



                    //RECORRIDO DE TURNO LABORAL
                    #region consulta y procesamiento de turno laboral

                    DateTime turnoLabDesde = dtm; //= dtm.AddHours(8);
                    DateTime turnoLabHasta = dtm.AddHours(23).AddMinutes(59); //= dtm.AddHours(17);

                    if (turnoFiltro != null)
                    {
                        turnoLabDesde = dtm.AddHours(turnoFiltro?.Turno?.Entrada.Hour ?? 0).AddMinutes(turnoFiltro?.Turno?.Entrada.Minute ?? 0);
                        turnoLabHasta = dtm.AddHours(turnoFiltro?.Turno?.Salida.Hour ?? 0).AddMinutes(turnoFiltro?.Turno?.Salida.Minute ?? 0);
                        turnoRecesoFiltro = await _repositoryTurnoColAsync.FirstOrDefaultAsync(new TurnoRecesoColaboradorTreeSpec(itemCol.Identificacion, dtm, turnoFiltro?.Turno.Id), cancellationToken);
                        poseeTurno = true;
                        if (turnoRecesoFiltro != null) poseeTurnoReceso = true;
                    }
                    if (turnoLabDesde > turnoLabHasta) turnoLabHasta.AddDays(1);

                    //SE OBTIENE MARCACIONES DE LA FECHA EN PROCESO

                    string codMarcacionEntrada = (turnoFiltro?.Turno?.CodigoEntrada?.ToString()) ?? "10";
                    string codMarcacionSalida = (turnoFiltro?.Turno?.CodigoSalida?.ToString()) ?? "11";


                    List<BitacoraMarcacionType> objMarcacionColEntrada_ = await _EvaluacionAsync.ConsultaMarcaciones(itemCol.Identificacion, dtm, dtm.AddHours(23).AddMinutes(59), codMarcacionEntrada);
                    BitacoraMarcacionType objMarcacionColEntrada = objMarcacionColEntrada_.FirstOrDefault();
                    List<BitacoraMarcacionType> objMarcacionColSalida_ = new();
                    BitacoraMarcacionType objMarcacionColSalida;

                    if (objMarcacionColEntrada != null)
                    {
                        string timeString = objMarcacionColEntrada?.Time;
                        DateTime date3 = DateTime.ParseExact(timeString, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        objMarcacionColSalida_ = await _EvaluacionAsync.ConsultaMarcaciones(itemCol.Identificacion, date3, turnoLabDesde.AddDays(1), codMarcacionSalida);
                    }

                    objMarcacionColSalida = objMarcacionColSalida_.FirstOrDefault();
                    

                    string fEntrada = objMarcacionColEntrada?.Time.ToString();
                    DateTime? fechaEntrada = !string.IsNullOrEmpty(fEntrada) ? Convert.ToDateTime(fEntrada, CultureInfo.InvariantCulture) : null;
                    

                    string fSalida = objMarcacionColSalida?.Time.ToString();
                    DateTime? fechaSalida = !string.IsNullOrEmpty(fSalida) ? Convert.ToDateTime(fSalida, CultureInfo.InvariantCulture) : null;

                    //SE PREPARA LA INFORMACION DE RETORNO
                    TurnoLaboral turnoLaborall = new()
                    {
                        //turno
                        Id = turnoFiltro?.Id,
                        Entrada = turnoFiltro?.Turno?.Entrada ?? null,
                        Salida = turnoFiltro?.Turno?.Salida ?? null,
                        TotalHoras = (turnoFiltro?.Turno?.TotalHoras) ?? "0",

                        //marcacion ENTRADA
                        MarcacionEntrada = fechaEntrada,
                        EstadoEntrada = objMarcacionColEntrada?.EstadoMarcacion ?? "",
                        FechaSolicitudEntrada = objMarcacionColEntrada?.FechaSolicitud != null ? DateTime.Parse(objMarcacionColEntrada?.FechaSolicitud) : DateTime.Parse("01-01-1900"),
                        UsuarioSolicitudEntrada = objMarcacionColEntrada?.UsuarioSolicitud ?? "0",
                        IdSolicitudEntrada   = objMarcacionColEntrada?.IdSolicitud,

                        //MARCACION SALIDA
                        MarcacionSalida = fechaSalida,
                        EstadoSalida = objMarcacionColSalida?.EstadoMarcacion ?? "",
                        FechaSolicitudSalida = objMarcacionColSalida?.FechaSolicitud != null ? DateTime.Parse(objMarcacionColSalida?.FechaSolicitud) : DateTime.Parse("01-01-1900"),
                        UsuarioSolicitudSalida = objMarcacionColSalida?.UsuarioSolicitud ?? "0",
                        IdSolicitudSalida = objMarcacionColSalida?.IdSolicitud
                    };


                    #endregion


                    #region consulta y procesamiento de turno de receso


                    DateTime turnoRecesoDesde = turnoLaborall.MarcacionEntrada ?? dtm;
                    DateTime turnoRecesoHasta = turnoLaborall.MarcacionSalida ?? dtm.AddHours(23).AddMinutes(59);

                    string codMarcacionEntradaReceso = (turnoRecesoFiltro?.Turno?.CodigoEntrada?.ToString()) ?? "14";
                    string codMarcacionSalidaReceso = (turnoRecesoFiltro?.Turno?.CodigoSalida?.ToString()) ?? "15";


                    List<BitacoraMarcacionType> marcacionEntradaRecesoFiltro_ = await _EvaluacionAsync.ConsultaMarcaciones(itemCol.Identificacion, turnoRecesoDesde, turnoRecesoHasta, codMarcacionEntradaReceso);
                    BitacoraMarcacionType marcacionEntradaRecesoFiltro = marcacionEntradaRecesoFiltro_.FirstOrDefault();
                    List<BitacoraMarcacionType> marcacionSalidaRecesoFiltro_ = new();
                    BitacoraMarcacionType marcacionSalidaRecesoFiltro;
                    if (marcacionEntradaRecesoFiltro != null)
                    {
                        string timeString = marcacionEntradaRecesoFiltro?.Time;
                        DateTime date2 = DateTime.ParseExact(timeString, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        marcacionSalidaRecesoFiltro_ = await _EvaluacionAsync.ConsultaMarcaciones(itemCol.Identificacion, date2, turnoLabDesde.AddDays(1), codMarcacionSalidaReceso);
                    }
                    marcacionSalidaRecesoFiltro = marcacionSalidaRecesoFiltro_?.FirstOrDefault();

                    TurnoReceso turnoReceso = new()
                    {
                        //turno de receso asignado
                        Id = turnoRecesoFiltro?.Id ?? null,
                        Entrada = turnoRecesoFiltro?.Turno?.Entrada ?? null,
                        Salida = turnoRecesoFiltro?.Turno?.Salida ?? null,
                        TotalHoras = (turnoRecesoFiltro?.Turno?.TotalHoras) ?? "0",

                        //marcaciones de receso entrada
                        MarcacionEntrada = marcacionEntradaRecesoFiltro?.Time != null ? DateTime.ParseExact(marcacionEntradaRecesoFiltro.Time, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : null,
                        FechaSolicitudEntradaReceso = marcacionEntradaRecesoFiltro?.FechaSolicitud != null ? DateTime.ParseExact(marcacionEntradaRecesoFiltro.FechaSolicitud, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse("01-01-1900"),
                        UsuarioSolicitudEntradaReceso = marcacionEntradaRecesoFiltro?.UsuarioSolicitud ?? "0",
                        IdSolicitudEntradaReceso = marcacionEntradaRecesoFiltro?.IdSolicitud,
                        EstadoEntradaReceso = marcacionEntradaRecesoFiltro?.EstadoMarcacion ?? "",

                        //marcaciones de receso salida
                        MarcacionSalida = marcacionSalidaRecesoFiltro?.Time != null ? DateTime.ParseExact(marcacionSalidaRecesoFiltro.Time, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : null,
                        FechaSolicitudSalidaReceso = marcacionSalidaRecesoFiltro?.FechaSolicitud != null ? DateTime.ParseExact(marcacionSalidaRecesoFiltro.FechaSolicitud, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse("01-01-1900"),
                        UsuarioSolicitudSalidaReceso = marcacionSalidaRecesoFiltro?.UsuarioSolicitud ?? "",
                        IdSolicitudSalidaReceso = marcacionSalidaRecesoFiltro?.IdSolicitud,
                        EstadoSalidaReceso = marcacionSalidaRecesoFiltro?.EstadoMarcacion ?? "",

                    };

                    #endregion


                    #region Consulta y procesamiento de novedades
                    novedades.Clear();

                    if (turnoFiltro?.Turno?.ClaseTurno?.CodigoClaseturno == "LABORA")
                    {
                        if (!string.IsNullOrEmpty(objMarcacionColEntrada?.Novedad))
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = objMarcacionColEntrada?.Novedad,
                                MinutosNovedad = objMarcacionColEntrada?.Minutos_Novedad,
                                EstadoMarcacion = objMarcacionColEntrada?.EstadoMarcacion
                            });
                        }

                        if (!string.IsNullOrEmpty(objMarcacionColSalida?.Novedad))
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = objMarcacionColSalida?.Novedad,
                                MinutosNovedad = objMarcacionColSalida?.Minutos_Novedad,
                                EstadoMarcacion = objMarcacionColSalida?.EstadoMarcacion
                            });
                        }

                        if (!string.IsNullOrEmpty(marcacionSalidaRecesoFiltro?.Novedad))
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = marcacionSalidaRecesoFiltro?.Novedad,
                                MinutosNovedad = marcacionSalidaRecesoFiltro?.Minutos_Novedad,
                                EstadoMarcacion = marcacionSalidaRecesoFiltro?.EstadoMarcacion
                            });
                        }

                        if (turnoFiltro != null && objMarcacionColEntrada == null) // && 
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = "Falta injustificada, no tiene registro de entrada.",
                                MinutosNovedad = "",
                                EstadoMarcacion = "FI"
                            });
                        } //SI TIENE TURNO Y NO REALIZA MARCACION (FALTA) /FJ /FI

                        if (turnoFiltro == null && (objMarcacionColEntrada != null || objMarcacionColSalida == null))
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = "No tiene registro de salida",
                                MinutosNovedad = "",
                                EstadoMarcacion = "NS"
                            });
                        } //NO REGISTRA MARCACION DE SALIDA

                        if (turnoRecesoFiltro != null && marcacionEntradaRecesoFiltro != null && marcacionSalidaRecesoFiltro == null)
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = "No tiene registro de retorno del receso",
                                MinutosNovedad = "",
                                EstadoMarcacion = ""
                            });
                        }

                        if (turnoRecesoFiltro == null && (marcacionEntradaRecesoFiltro != null || marcacionSalidaRecesoFiltro != null))
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = "No ha sido asignado el turno de receso, pero registra marcacion de receso.",
                                MinutosNovedad = "",
                                EstadoMarcacion = ""
                            });
                        }
                    }
                    #endregion

                    listaEvaluacionAsistencia.Add(new EvaluacionAsistenciaResponseType()
                    {
                        Colaborador = itemCol.Empleado,
                        Identificacion = itemCol.Identificacion,
                        CodBiometrico = itemCol.CodigoBiometrico,
                        Udn = request.Udn,
                        Area = request.Area,
                        SubCentroCosto = request.Departamento,
                        Fecha = dtm,
                        Novedades = novedades,
                        TurnoLaboral = turnoLaborall,
                        TurnoReceso = turnoReceso,
                        //Solicitudes = solicitudes
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