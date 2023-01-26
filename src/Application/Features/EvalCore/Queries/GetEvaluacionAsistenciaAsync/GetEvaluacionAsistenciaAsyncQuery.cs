using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using EvaluacionCore.Application.Features.EvalCore.Dto;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.EvalCore.Queries.GetEvaluacionAsistenciaAsync;

public record GetEvaluacionAsistenciaAsyncQuery(string Suscriptor, string Periodo, string Udn, string Area, string Departamento, string FiltroNovedades) : IRequest<ResponseType<List<EvaluacionAsistenciaResponseType>>>;

public class GetEvaluacionAsistenciaAsyncHandler : IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>
{
    private readonly IEvaluacion _EvaluacionAsync;
    private readonly IApisConsumoAsync _ApiConsumoAsync;
    private readonly IBitacoraMarcacion _repoBitMarcacionAsync;
    private readonly IRepositoryAsync<ControlAsistenciaCab> _repositoryCAsisCabAsync;
    private readonly IRepositoryAsync<ControlAsistenciaDet> _repositoryCAsisDetAsync;
    private readonly IRepositoryAsync<ControlAsistenciaNovedad> _repositoryCAsisNovAsync;
    private readonly IConfiguration _config;
    private string ConnectionString { get; }



    public GetEvaluacionAsistenciaAsyncHandler(IEvaluacion repository, 
                                                //IRepositoryAsync<Cliente> repositoryCli,
                                                IConfiguration config, 
                                                IBitacoraMarcacion repoBitMarcacionAsync, 
                                                IApisConsumoAsync apisConsumoAsync)
    {
        _EvaluacionAsync = repository;
        _ApiConsumoAsync = apisConsumoAsync;
        _repoBitMarcacionAsync = repoBitMarcacionAsync;
        _config = config;
        ConnectionString = _config.GetConnectionString("ConnectionStrings:Bd_Marcaciones_GRIAMSE");
    }


    async Task<ResponseType<List<EvaluacionAsistenciaResponseType>>> IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>.Handle(GetEvaluacionAsistenciaAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<EvaluacionAsistenciaResponseType> listaEvaluacionAsistencia = new();

            List<ControlAsistenciaDet> controlAsistenciaDet = new();
            TurnoColaborador turnoRecesoFiltro = new();
            List<string> filtroNovedades = request.FiltroNovedades.Split("-").ToList();

            var cabecera = await _EvaluacionAsync.ConsultaControlAsistenciaCab(request.Udn, request.Area, request.Departamento, request.Periodo, request.Suscriptor);


            if (cabecera == null)
            {
                return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "001", Message = "La consulta no retorna datos." };
            }

            foreach (var itemCab in cabecera)
            {
                controlAsistenciaDet = await _EvaluacionAsync.ConsultaControlAsistenciaDet(itemCab.id);

                if (controlAsistenciaDet.Any())
                {
                    foreach (var item in controlAsistenciaDet)
                    {
                        var itemCol = await _EvaluacionAsync.ConsultaColaborador(item.Colaborador);
                        List<Novedad> novedades = new();

                        #region consulta y procesamiento de turno laboral

                        //SE PREPARA LA INFORMACION DE RETORNO
                        TurnoLaboral turnoLaborall = new()
                        {
                            //turno
                            Id = item.IdTurnoLaboral,
                            Entrada = item.EntradaLaboral,
                            Salida = item.SalidaLaboral,
                            TotalHoras = item.TotalHorasLaboral,

                            MarcacionEntrada = item.MarcacionEntradaLaboral,
                            EstadoEntrada = item.EstadoEntradaLaboral,
                            FechaSolicitudEntrada = item.FechaSolicitudEntradaLaboral,
                            UsuarioSolicitudEntrada = item.UsuarioSolicitudEntradaLaboral,
                            IdSolicitudEntrada = item.IdSolicitudEntradaLaboral,
                            IdFeatureEntrada = item.IdFeatureEntradaLaboral,
                            TipoSolicitudEntrada = item.TipoSolicitudEntradaLaboral,

                            MarcacionSalida = item.MarcacionSalidaLaboral,
                            EstadoSalida = item.EstadoSalidaLaboral,
                            FechaSolicitudSalida = item.FechaSolicitudSalidaLaboral,
                            UsuarioSolicitudSalida = item.UsuarioSolicitudSalidaLaboral,
                            IdSolicitudSalida = item.IdSolicitudSalidaLaboral,
                            IdFeatureSalida = item.IdFeatureSalidaLaboral,
                            TipoSolicitudSalida = item.TipoSolicitudSalidaLaboral
                        };


                        #endregion

                        #region consulta y procesamiento de turno de receso


                        TurnoReceso turnoReceso = new()
                        {
                            //turno de receso asignado
                            Id = item.IdTurnoReceso,
                            Entrada = item.EntradaReceso,
                            Salida = item.SalidaReceso,
                            TotalHoras = item.TotalHorasReceso,

                            //marcaciones de receso entrada
                            MarcacionEntrada = item.MarcacionEntradaReceso,
                            FechaSolicitudEntradaReceso = DateTime.Parse("1900-01-01"),
                            UsuarioSolicitudEntradaReceso = "",
                            IdSolicitudEntradaReceso = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                            EstadoEntradaReceso = item.EstadoEntradaReceso,
                            IdFeatureEntradaReceso = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                            TipoSolicitudEntradaReceso = "",

                            MarcacionSalida = item.MarcacionSalidaReceso,
                            FechaSolicitudSalidaReceso = DateTime.Parse("1900-01-01"),
                            UsuarioSolicitudSalidaReceso = "",
                            IdSolicitudSalidaReceso = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                            EstadoSalidaReceso = item.EstadoSalidaReceso,
                            IdFeatureSalidaReceso = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                            TipoSolicitudSalidaReceso = ""

                        };

                        #endregion

                        #region Consulta y procesamiento de novedades

                        var listaNovedades = await _EvaluacionAsync.ConsultaControlAsistenciaNovedad(item.Id);

                        foreach (var listaNovedad in listaNovedades)
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = listaNovedad.Descripcion,
                                MinutosNovedad = listaNovedad.MinutosNovedad,
                                EstadoMarcacion = listaNovedad.EstadoMarcacion
                            });
                        }

                        #endregion

                        listaEvaluacionAsistencia.Add(new EvaluacionAsistenciaResponseType()
                        {
                            Colaborador = itemCol?.FirstOrDefault()?.Empleado,
                            Identificacion = itemCol?.FirstOrDefault()?.Identificacion,
                            CodBiometrico = itemCol?.FirstOrDefault()?.CodigoBiometrico,
                            Udn = itemCol?.FirstOrDefault()?.DesUdn,
                            Area = itemCol?.FirstOrDefault()?.DesArea,
                            SubCentroCosto = itemCol?.FirstOrDefault()?.DesSubcentroCosto,
                            Fecha = item.Fecha,
                            Novedades = novedades,
                            TurnoLaboral = turnoLaborall,
                            TurnoReceso = turnoReceso
                        });
                    }
                }
            }


            var lista = listaEvaluacionAsistencia.Where(e => filtroNovedades.Contains(e.TurnoLaboral.EstadoEntrada) || filtroNovedades.Contains(e.TurnoLaboral.EstadoSalida) ||
                                                 filtroNovedades.Contains(e.TurnoReceso.EstadoEntradaReceso) || filtroNovedades.Contains(e.TurnoReceso.EstadoSalidaReceso) ||
                                                 filtroNovedades.Contains(e.Novedades.Select(e => e.EstadoMarcacion).ToString())).ToList();

            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = lista, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
            //return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };

        }
       catch (Exception e)
        {
            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }

    }


    private string EvaluaTipoSolicitud(Guid? idFeature)
    {
        Guid permiso = Guid.Parse("DE4D17BD-9F03-4CCB-A3C0-3F37629CEA6A");
        Guid justificacion = Guid.Parse("16D8E575-51A2-442D-889C-1E93E9F786B2");
        Guid vacacion = Guid.Parse("26A08EC8-40FE-435C-8655-3F570278879E");
        if (idFeature != null)
        {
            if (idFeature == permiso)
            {
                return "PER";
            }
            else if(idFeature == justificacion)
            {
                return "JUS";
            }
            else if (idFeature == vacacion)
            {
                return "VAC";
            }
            else
            {
                return "";
            }
        }

        return "";
    }
    
   
}