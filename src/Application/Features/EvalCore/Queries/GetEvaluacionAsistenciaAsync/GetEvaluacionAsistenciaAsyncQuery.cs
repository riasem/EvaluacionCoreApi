using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Application.Features.EvalCore.Dto;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.EvalCore.Specifications;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using EvaluacionCore.Domain.Entities.Seguridad;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.EvalCore.Queries.GetEvaluacionAsistenciaAsync;

public record GetEvaluacionAsistenciaAsyncQuery(string Suscriptor, string Periodo, string Udn, string Area, string Departamento, string FiltroNovedades,string identSession,Guid? idCanal) : IRequest<ResponseType<List<EvaluacionAsistenciaResponseType>>>;

public class GetEvaluacionAsistenciaAsyncHandler : IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>
{
    private readonly IEvaluacion _EvaluacionAsync;
    private readonly IRepositoryAsync<ColaboradorConvivencia> _repoColabConvivenciaAync;
    private readonly IRepositoryAsync<Cliente> _repoClienteAync;
    private readonly IRepositoryAsync<RolCargoSG> _repoRolCargoAync;
    private readonly IRepositoryAsync<AtributoRolSG> _repoAtributoRolAync;
    private readonly IRepositoryGRiasemAsync<ControlAsistenciaSolicitudes> _repositoryCAsisSoliAsync;

    private readonly IRepositoryGRiasemAsync<ControlAsistenciaSolicitudes_V> _repositoryCAsisSoli_VAsync;
    private readonly IRepositoryGRiasemAsync<ControlAsistenciaNovedad_V> _repositoryCAsisNov_VAsync;
    private readonly IRepositoryGRiasemAsync<ControlAsistenciaCab_V> _repositoryCAsisCab_VAsync;
    private readonly IConfiguration _config;
    private string ConnectionString { get; }



    public GetEvaluacionAsistenciaAsyncHandler(IEvaluacion repository,
                                                IRepositoryGRiasemAsync<ControlAsistenciaSolicitudes> repositorySoli,
                                                IConfiguration config,  IRepositoryAsync<ColaboradorConvivencia> repoColabConvivenciaAync,
                                                IRepositoryAsync<RolCargoSG> repoRolCargoAync,
                                                IRepositoryAsync<AtributoRolSG> repoAtributoRolAync,
                                                IRepositoryAsync<Cliente> repoClienteAync,
                                                IRepositoryGRiasemAsync<ControlAsistenciaCab_V> repoAsistenciaCabAync,
                                                IRepositoryGRiasemAsync<ControlAsistenciaNovedad_V> repoAsistenciaNovAync,
                                                IRepositoryGRiasemAsync<ControlAsistenciaSolicitudes_V> repoAsistenciaSolAync)
    {
        _EvaluacionAsync = repository;
        _repositoryCAsisSoliAsync = repositorySoli;
        _config = config;
        ConnectionString = _config.GetConnectionString("ConnectionStrings:Bd_Marcaciones_GRIAMSE");
        _repoColabConvivenciaAync = repoColabConvivenciaAync;
        _repoRolCargoAync = repoRolCargoAync;
        _repoAtributoRolAync = repoAtributoRolAync;
        _repoClienteAync = repoClienteAync;

        _repositoryCAsisCab_VAsync = repoAsistenciaCabAync;
        _repositoryCAsisNov_VAsync = repoAsistenciaNovAync;
        _repositoryCAsisSoli_VAsync = repoAsistenciaSolAync;
    }


    async Task<ResponseType<List<EvaluacionAsistenciaResponseType>>> IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>.Handle(GetEvaluacionAsistenciaAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<EvaluacionAsistenciaResponseType> listaEvaluacionAsistencia = new();
            List<ControlAsistenciaCab> controlAsistenciaCab = new();
            List<ControlAsistenciaDet> controlAsistenciaDet = new();
            List<ControlAsistenciaNovedad> controlAsistenciaNov = new();
            List<ControlAsistenciaSolicitudes> controlAsistenciaSoli = new();
            TurnoColaborador turnoLaboralFiltro = new();
            TurnoColaborador turnoRecesoFiltro = new();


            List<string> filtroNovedades = request.FiltroNovedades.Split("-").ToList();

            string depar = request.Departamento == "" ? null : request.Departamento;
            string susc = request.Suscriptor == "" ? null : request.Suscriptor;
            string area = request.Area == "" ? null : request.Area;

            var cabecera = await _repositoryCAsisCab_VAsync.ListAsync(new GetControlAsistenciaCabByFilterSpec(request.Udn, area, depar, request.Periodo, susc), cancellationToken);

            var asistenciaNov = await _repositoryCAsisNov_VAsync.ListAsync(new GetControlAsistenciaNovByFilterSpec(request.Udn, area, depar, request.Periodo, susc), cancellationToken);
            
            var solicitudes = await _repositoryCAsisSoli_VAsync.ListAsync(new GetControlAsistenciaSoliByFilterSpec(request.Udn, area, depar, request.Periodo, susc), cancellationToken);

            #region Filtro de Colaboradores que se debe presentar en la consulta

            var colaSesion = await _repoColabConvivenciaAync.FirstOrDefaultAsync(new GetColaboradorConvivenciaByUdnAreaSccSpec("", "", "", request.identSession), cancellationToken);

            var rolCargo = await _repoRolCargoAync.FirstOrDefaultAsync(new GetRolesAccesoByCargoConvivenciaSpec(colaSesion.CodCargo, colaSesion.CodSubcentroCosto, request.idCanal, ""), cancellationToken);
            
            if (rolCargo != null)
            {
                var listAttributos = await _repoAtributoRolAync.ListAsync(new GetAtributosByRolSpec(rolCargo.RolSG.Id), cancellationToken);

                var idAtributoTTHH = _config.GetSection("Atributos:ControlAsistenciaTTHH").Get<string>();

                var atributoTTHH = listAttributos.Where(x => x.Id == Guid.Parse(idAtributoTTHH)).ToList();
                if (!atributoTTHH.Any())
                {
                    var objJefe = await _repoClienteAync.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(colaSesion.Identificacion), cancellationToken);
                    var objColaboradoresJefe = await _repoClienteAync.ListAsync(new GetColaboradorByJefe(objJefe.Id), cancellationToken);
                    
                    cabecera = cabecera.Where(x => objJefe.Identificacion == x.Identificacion || objColaboradoresJefe.Any(c => c.Identificacion == x.Identificacion)).ToList();

                }
            }
            else
            {
                cabecera = cabecera.Where(x => x.Identificacion == colaSesion.Identificacion).ToList();
            }

            #endregion
            
            //var cabecera = await _EvaluacionAsync.ConsultaControlAsistenciaCab(request.Udn, request.Area, request.Departamento, request.Periodo, request.Suscriptor);
                       

            if (cabecera.Count == 0)
            {
                return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "001", Message = "La consulta no retorna datos." };
            }

            foreach (var item in cabecera)
            {
                
                    var itemCol = await _EvaluacionAsync.ConsultaColaborador(item.Colaborador);
                    List<Novedad> novedades = new();
                    List<ControlAsistenciaSolicitudes> solicitudes1 = new();
                    //var solicitudes = await _repositoryCAsisSoliAsync.FirstOrDefaultAsync(new ControlAsistenciaSolicitudByIdDetalle(item.Id), cancellationToken);
                        
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
                        TipoSolicitudEntrada = EvaluaTipoSolicitud(item.IdFeatureEntradaLaboral),

                        MarcacionSalida = item.MarcacionSalidaLaboral,
                        EstadoSalida = item.EstadoSalidaLaboral,
                        FechaSolicitudSalida = item.FechaSolicitudSalidaLaboral,
                        UsuarioSolicitudSalida = item.UsuarioSolicitudSalidaLaboral,
                        IdSolicitudSalida = item.IdSolicitudSalidaLaboral,
                        IdFeatureSalida = item.IdFeatureSalidaLaboral,
                        TipoSolicitudSalida = EvaluaTipoSolicitud(item.IdFeatureSalidaLaboral)
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

                #region Consulta y procesamiento de solicitudes

                var solicitudesFilter = solicitudes.Where(x => x.IdControlAsistenciaDet == item.IdDet).ToList();

                if (solicitudes.Count > 0)
                {
                    foreach (var soli in solicitudes)
                    {
                        solicitudes1.Add(new ControlAsistenciaSolicitudes
                        {
                            Id = soli.Id,
                            IdControlAsistenciaDet = soli.IdControlAsistenciaDet,
                            Colaborador = soli.Colaborador,
                            Comentarios = soli.Comentarios,
                            IdFeature = soli.IdFeature,
                            IdSolicitud = soli.IdSolicitud
                        });
                    }
                }
                #endregion

                #region Consulta y procesamiento de novedades

                var listaNovedades = asistenciaNov.Where(x => x.IdControlAsistenciaDet == item.IdDet).ToList();
                bool poseeNovedades = false;
                foreach (var listaNovedad in listaNovedades)
                {
                    if (filtroNovedades.Contains(listaNovedad.EstadoMarcacion))
                    {
                        poseeNovedades = true;
                        novedades.Add(new Novedad
                        {
                            Descripcion = listaNovedad.Descripcion,
                            MinutosNovedad = listaNovedad.MinutosNovedad,
                            EstadoMarcacion = listaNovedad.EstadoMarcacion
                        });
                    }
                }

                #endregion

                if (poseeNovedades ||
                    filtroNovedades.Contains(turnoLaborall.EstadoEntrada) ||
                    filtroNovedades.Contains(turnoLaborall.EstadoSalida) ||
                    filtroNovedades.Contains(turnoReceso.EstadoSalidaReceso) ||
                    filtroNovedades.Contains(turnoReceso.EstadoEntradaReceso))
                {
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
                        TurnoReceso = turnoReceso,
                        Solicitudes = solicitudes1
                    });
                }
            }
            List<EvaluacionAsistenciaResponseType> listaTmp2;

            
            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
            
        }
       catch (Exception e)
        {
            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }

    }


    private static string EvaluaTipoSolicitud(Guid? idFeature)
    {
        Guid permiso = Guid.Parse("DE4D17BD-9F03-4CCB-A3C0-3F37629CEA6A");
        Guid justificacion = Guid.Parse("16D8E575-51A2-442D-889C-1E93E9F786B2");
        Guid vacacion = Guid.Parse("26A08EC8-40FE-435C-8655-3F570278879E");
        Guid hextra = Guid.Parse("B0BE0865-5C82-40FE-A48A-491154CA6368");
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
            else if (idFeature == hextra)
            {
                return "HEX";
            }
            else
            {
                return "";
            }
        }

        return "";
    }
    
   
}