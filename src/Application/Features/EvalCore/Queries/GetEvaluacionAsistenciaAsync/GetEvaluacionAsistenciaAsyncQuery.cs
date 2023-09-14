﻿using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Application.Features.EvalCore.Dto;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.EvalCore.Specifications;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using EvaluacionCore.Domain.Entities.Seguridad;
using MediatR;
using Microsoft.Extensions.Configuration;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using System.Globalization;

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

    private readonly IMarcacion _repoMarcacionAsync;
    private readonly IRepositoryGRiasemAsync<PeriodosLaborales> _repositoryPeriodoAsync;


    public GetEvaluacionAsistenciaAsyncHandler(IEvaluacion repository,
                                                IRepositoryGRiasemAsync<ControlAsistenciaSolicitudes> repositorySoli,
                                                IConfiguration config,  IRepositoryAsync<ColaboradorConvivencia> repoColabConvivenciaAync,
                                                IRepositoryAsync<RolCargoSG> repoRolCargoAync,
                                                IRepositoryAsync<AtributoRolSG> repoAtributoRolAync,
                                                IRepositoryAsync<Cliente> repoClienteAync,
                                                IRepositoryGRiasemAsync<ControlAsistenciaCab_V> repoAsistenciaCabAync,
                                                IRepositoryGRiasemAsync<ControlAsistenciaNovedad_V> repoAsistenciaNovAync,
                                                IRepositoryGRiasemAsync<ControlAsistenciaSolicitudes_V> repoAsistenciaSolAync,
                                                IMarcacion repoMarcacionAsync, IRepositoryGRiasemAsync<PeriodosLaborales> repositoryPeriodoAsync)
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
        _repoMarcacionAsync = repoMarcacionAsync;
        _repositoryPeriodoAsync = repositoryPeriodoAsync;
    }


    async Task<ResponseType<List<EvaluacionAsistenciaResponseType>>> IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>.Handle(GetEvaluacionAsistenciaAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<EvaluacionAsistenciaResponseType> listaEvaluacionAsistencia = new();
            List<NovedadMarcacionWebType> listaNovedadMarcacionWeb = new();

            List<string> filtroNovedades = request.FiltroNovedades.Split("-").ToList();

            string depar = request.Departamento == "" ? null : request.Departamento;
            string susc = request.Suscriptor == "" ? null : request.Suscriptor;
            string area = request.Area == "" ? null : request.Area;

            var periodolaboral = await _repositoryPeriodoAsync.FirstOrDefaultAsync(new GetPeriodoLaboralByDescSpec(request.Udn, request.Periodo));

            if (periodolaboral is null) return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "001", Message = "El Periodo no se encuentra definido para la UDN." };

            #region Filtro de Colaboradores que se debe presentar en la consulta
            var colaboradores = await _repoColabConvivenciaAync.ListAsync(new GetColaboradorConvivenciaByUdnAreaSccSpec(request.Udn, area, depar, susc), cancellationToken);
            // Si la lista tiene menos de 1 registro, entonces lanzar una excepción
            // "No hay colaboradores activos para ese filtro"
            if (colaboradores is null || colaboradores.Count == 0)
            {
                return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "001", Message = "No hay colaboradores activos para las condiciones seleccionadas" };
            }
            // 
             var colaSesion = await _repoColabConvivenciaAync.FirstOrDefaultAsync(new GetColaboradorConvivenciaByUdnAreaSccSpec("", "", "", request.identSession), cancellationToken);

            // Cambiar a un ListAsync
            var rolesCargos = await _repoRolCargoAync.ListAsync(new GetRolesAccesoByCargoConvivenciaSpec(colaSesion.CodCargo, colaSesion.CodSubcentroCosto, request.idCanal, ""), cancellationToken);
            bool banderaTtth = false;
            var idAtributoTTHH = _config.GetSection("Atributos:ControlAsistenciaTTHH").Get<string>();
            // Recorrer los RolesCargo, en busqueda del atributo de TalentoHumano
            foreach (var rolCargo in rolesCargos)
            {
                var listAttributos = await _repoAtributoRolAync.ListAsync(new GetAtributosByRolSpec(rolCargo.RolSG.Id), cancellationToken);
                var atributoTTHH = listAttributos.Where(x => x.Id == Guid.Parse(idAtributoTTHH)).ToList();
                if (atributoTTHH.Any())
                {
                    banderaTtth = true;
                }
            }
            // Si no tiene el atributo de Talento Humano, procede a buscar los colaboradores a su cargo
            if (!banderaTtth)
            {
                var objJefe = await _repoClienteAync.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(colaSesion.Identificacion), cancellationToken);
                var objColaboradoresJefe = await _repoClienteAync.ListAsync(new GetColaboradorByJefe(objJefe.Id), cancellationToken);

                if (!objColaboradoresJefe.Any())
                {
                    return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "001", Message = "No tiene colaboradores asignados a su cargo" };
                }

                // SI no tiene colaboradores, lanzar una excepcion
                colaboradores = colaboradores.Where(x => objJefe.Identificacion == x.Identificacion || objColaboradoresJefe.Any(c => c.Identificacion == x.Identificacion)).ToList();
            }
            if (!colaboradores.Any())
            {
                return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "001", Message = "No tiene colaboradores por consultar" };
            }
            #endregion

            #region Recorre cada uno de los Colaboradores que se deben presentar en la consulta
            foreach (var col in colaboradores)
            {
                var novedadMarcacionWeb = await _repoMarcacionAsync.ConsultaAsistencia(col.Identificacion, request.FiltroNovedades, periodolaboral.FechaDesdeCorte, periodolaboral.FechaHastaCorte, cancellationToken);
                if (novedadMarcacionWeb.Data != null) listaNovedadMarcacionWeb.AddRange(novedadMarcacionWeb.Data);

                var asistenciasColaborador = novedadMarcacionWeb.Data;
                foreach (var asistenciaColaborador in asistenciasColaborador)
                {
                    List<Dto.Novedad> novedades = new();
                    List<ControlAsistenciaSolicitudes> solicitudes1 = new();
                    int iteracion = 0;

                    //SE PREPARA LA INFORMACION DE RETORNO
                    Dto.TurnoLaboral turnoLaborall = new()
                    {
                        //turno
                        Id = asistenciaColaborador.TurnoLaboral.Id,
                        CodigoTurno = asistenciaColaborador?.TurnoLaboral?.CodigoTurno ?? null,
                        ClaseTurno = asistenciaColaborador?.TurnoLaboral?.ClaseTurno ?? null,
                        Entrada = asistenciaColaborador.TurnoLaboral.Entrada,
                        Salida = asistenciaColaborador.TurnoLaboral.Salida,
                        TotalHoras = asistenciaColaborador.TurnoLaboral.TotalHoras,

                        MinutosNovedadIngreso = asistenciaColaborador?.TurnoLaboral?.MinutosNovedadIngreso ?? 0,
                        NovedadIngreso = asistenciaColaborador?.TurnoLaboral?.NovedadIngreso ?? "",
                        MarcacionEntrada = asistenciaColaborador.TurnoLaboral.MarcacionEntrada,
                        EstadoEntrada = asistenciaColaborador.TurnoLaboral.EstadoEntrada,
                        FechaSolicitudEntrada = asistenciaColaborador.TurnoLaboral.FechaSolicitudEntrada,
                        UsuarioSolicitudEntrada = asistenciaColaborador.TurnoLaboral.UsuarioSolicitudEntrada,
                        IdSolicitudEntrada = asistenciaColaborador.TurnoLaboral.IdSolicitudEntrada,
                        IdFeatureEntrada = asistenciaColaborador.TurnoLaboral.IdFeatureEntrada,
                        TipoSolicitudEntrada = EvaluaTipoSolicitud(asistenciaColaborador.TurnoLaboral.IdFeatureEntrada),

                        MinutosNovedadSalida = asistenciaColaborador?.TurnoLaboral?.MinutosNovedadSalida ?? 0,
                        NovedadSalida = asistenciaColaborador?.TurnoLaboral?.NovedadSalida ?? "",
                        MarcacionSalida = asistenciaColaborador.TurnoLaboral.MarcacionSalida,
                        EstadoSalida = asistenciaColaborador.TurnoLaboral.EstadoSalida,
                        FechaSolicitudSalida = asistenciaColaborador.TurnoLaboral.FechaSolicitudSalida,
                        UsuarioSolicitudSalida = asistenciaColaborador.TurnoLaboral.UsuarioSolicitudSalida,
                        IdSolicitudSalida = asistenciaColaborador.TurnoLaboral.IdSolicitudSalida,
                        IdFeatureSalida = asistenciaColaborador.TurnoLaboral.IdFeatureSalida,
                        TipoSolicitudSalida = EvaluaTipoSolicitud(asistenciaColaborador.TurnoLaboral.IdFeatureSalida)
                    };

                    Dto.TurnoReceso turnoReceso = new()
                    {
                        //turno de receso asignado
                        Id = asistenciaColaborador.TurnoReceso.Id,
                        Entrada = asistenciaColaborador.TurnoReceso.Entrada,
                        Salida = asistenciaColaborador.TurnoReceso.Salida,
                        TotalHoras = asistenciaColaborador.TurnoReceso.TotalHoras,

                        //marcaciones de receso entrada
                        MinutosNovedadEntradaReceso = asistenciaColaborador?.TurnoReceso?.MinutosNovedadEntradaReceso ?? 0,
                        NovedadEntradaReceso = asistenciaColaborador?.TurnoReceso?.NovedadEntradaReceso ?? "",
                        MarcacionEntrada = asistenciaColaborador.TurnoReceso.MarcacionEntrada,
                        FechaSolicitudEntradaReceso = asistenciaColaborador.TurnoReceso.FechaSolicitudEntradaReceso,
                        UsuarioSolicitudEntradaReceso = asistenciaColaborador.TurnoReceso.UsuarioSolicitudEntradaReceso,
                        IdSolicitudEntradaReceso = asistenciaColaborador.TurnoReceso.IdSolicitudEntradaReceso,
                        EstadoEntradaReceso = asistenciaColaborador.TurnoReceso.EstadoEntradaReceso,
                        IdFeatureEntradaReceso = asistenciaColaborador.TurnoReceso.IdFeatureEntradaReceso,
                        TipoSolicitudEntradaReceso = asistenciaColaborador.TurnoReceso.TipoSolicitudEntradaReceso,

                        MinutosNovedadSalidaReceso = asistenciaColaborador?.TurnoReceso?.MinutosNovedadSalidaReceso ?? 0,
                        NovedadSalidaReceso = asistenciaColaborador?.TurnoReceso?.NovedadSalidaReceso ?? "",
                        MarcacionSalida = asistenciaColaborador.TurnoReceso.MarcacionSalida,
                        FechaSolicitudSalidaReceso = asistenciaColaborador.TurnoReceso.FechaSolicitudSalidaReceso,
                        UsuarioSolicitudSalidaReceso = asistenciaColaborador.TurnoReceso.UsuarioSolicitudSalidaReceso,
                        IdSolicitudSalidaReceso = asistenciaColaborador.TurnoReceso.IdSolicitudSalidaReceso,
                        EstadoSalidaReceso = asistenciaColaborador.TurnoReceso.EstadoSalidaReceso,
                        IdFeatureSalidaReceso = asistenciaColaborador.TurnoReceso.IdFeatureSalidaReceso,
                        TipoSolicitudSalidaReceso = asistenciaColaborador.TurnoReceso.TipoSolicitudSalidaReceso
                    };

                    foreach (var novedad in asistenciaColaborador.Novedades)
                    {
                        novedades.Add(new Dto.Novedad
                        {
                            Descripcion = novedad.Descripcion,
                            MinutosNovedad = novedad.MinutosNovedad,
                            EstadoMarcacion = novedad.EstadoMarcacion
                        });
                    }

                    #region Consulta y procesamiento de solicitudes
                    if ((asistenciaColaborador.TurnoLaboral?.ClaseTurno ?? "") == "LABORAL")
                    {
                        // Consulta si existe alguna solicitud de permiso aprobada para la fecha en que marca la entrada
                        if (asistenciaColaborador.TurnoLaboral?.IdSolicitudEntrada != Guid.Empty)
                        {
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudbyIdSolicitud(asistenciaColaborador.TurnoLaboral?.IdSolicitudEntrada.ToString(), "APROBADA");

                            iteracion = iteracion + 1;
                            solicitudes1.Add(new ControlAsistenciaSolicitudes
                            {
                                Id = solicitudesPermiso[0].NumeroSolicitud,
                                IdControlAsistenciaDet = iteracion,
                                Colaborador = asistenciaColaborador.Colaborador,
                                Comentarios = solicitudesPermiso[0].Comentarios,
                                IdFeature = Guid.Parse(asistenciaColaborador.TurnoLaboral?.IdFeatureEntrada.ToString()),
                                IdSolicitud = Guid.Parse(asistenciaColaborador.TurnoLaboral?.IdSolicitudEntrada.ToString())
                            });
                        }
                        // Consulta si existe alguna solicitud de permiso aprobada para la fecha en que marca la salida
                        if (asistenciaColaborador.TurnoLaboral?.IdSolicitudSalida != Guid.Empty)
                        {
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudbyIdSolicitud(asistenciaColaborador.TurnoLaboral?.IdSolicitudSalida.ToString(), "APROBADA");

                            iteracion = iteracion + 1;
                            solicitudes1.Add(new ControlAsistenciaSolicitudes
                            {
                                Id = solicitudesPermiso[0].NumeroSolicitud,
                                IdControlAsistenciaDet = iteracion,
                                Colaborador = asistenciaColaborador.Colaborador,
                                Comentarios = solicitudesPermiso[0].Comentarios,
                                IdFeature = Guid.Parse(asistenciaColaborador.TurnoLaboral?.IdFeatureSalida.ToString()),
                                IdSolicitud = Guid.Parse(asistenciaColaborador.TurnoLaboral?.IdSolicitudSalida.ToString())
                            });
                        }
                        // Falta a dia de labores
                        // Se debe considerar las solicitudes de permiso
                        if (asistenciaColaborador.TurnoLaboral.MarcacionEntrada is null && asistenciaColaborador.TurnoLaboral.MarcacionSalida is null)
                        {
                            // Consulta si existe alguna solicitud de permiso aprobada en la fecha del turno
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            string fTurno = asistenciaColaborador.TurnoLaboral?.Entrada.ToString();
                            DateTime? fechaTurno = !string.IsNullOrEmpty(fTurno) ? Convert.ToDateTime(fTurno, CultureInfo.InvariantCulture) : null;
                            if (fechaTurno is not null)
                            {
                                solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudesAprobadasbyCodigoBiometrico(asistenciaColaborador.CodBiometrico, fechaTurno.Value);
                            }
                            if (solicitudesPermiso.Count > 0 && (solicitudesPermiso[0]?.NumeroSolicitud.ToString() ?? "0") != "0")
                            {
                                iteracion = iteracion + 1;
                                solicitudes1.Add(new ControlAsistenciaSolicitudes
                                {
                                    Id = solicitudesPermiso[0].NumeroSolicitud,
                                    IdControlAsistenciaDet = iteracion,
                                    Colaborador = asistenciaColaborador.Colaborador,
                                    Comentarios = solicitudesPermiso[0].Comentarios,
                                    IdFeature = Guid.Parse(solicitudesPermiso[0].IdFeaturePermiso),
                                    IdSolicitud = Guid.Parse(solicitudesPermiso[0].IdSolicitudPermiso.ToString())
                                });
                            }
                        }
                        // Marca entrada pero NO marca salida laboral
                        // Se debe considerar las solicitudes de permiso
                        if (asistenciaColaborador.TurnoLaboral.MarcacionEntrada is not null && asistenciaColaborador.TurnoLaboral.MarcacionSalida is null)
                        {
                            // Consulta si existe alguna solicitud de permiso aprobada en la fecha del turno
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            string fTurno = asistenciaColaborador.TurnoLaboral?.Salida.ToString();
                            DateTime? fechaTurno = !string.IsNullOrEmpty(fTurno) ? Convert.ToDateTime(fTurno, CultureInfo.InvariantCulture) : null;
                            if (fechaTurno is not null)
                            {
                                solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudesAprobadasbyCodigoBiometrico(asistenciaColaborador.CodBiometrico, fechaTurno.Value);
                            }
                            if (solicitudesPermiso.Count > 0 && (solicitudesPermiso[0]?.NumeroSolicitud.ToString() ?? "0") != "0")
                            {
                                iteracion = iteracion + 1;
                                solicitudes1.Add(new ControlAsistenciaSolicitudes
                                {
                                    Id = solicitudesPermiso[0].NumeroSolicitud,
                                    IdControlAsistenciaDet = iteracion,
                                    Colaborador = asistenciaColaborador.Colaborador,
                                    Comentarios = solicitudesPermiso[0].Comentarios,
                                    IdFeature = Guid.Parse(solicitudesPermiso[0].IdFeaturePermiso),
                                    IdSolicitud = Guid.Parse(solicitudesPermiso[0].IdSolicitudPermiso.ToString())
                                });
                            }
                        }
                    }
                    #endregion

                    listaEvaluacionAsistencia.Add(new EvaluacionAsistenciaResponseType()
                    {
                        Colaborador = asistenciaColaborador.Colaborador,
                        Identificacion = asistenciaColaborador.Identificacion,
                        CodBiometrico = asistenciaColaborador.CodBiometrico,
                        Udn = asistenciaColaborador.Udn,
                        Area = asistenciaColaborador.Area,
                        SubCentroCosto = asistenciaColaborador.SubCentroCosto,
                        Fecha = asistenciaColaborador.Fecha,
                        Novedades = novedades,
                        TurnoLaboral = turnoLaborall,
                        TurnoReceso = turnoReceso,
                        Solicitudes = solicitudes1
                    });
                }
            }
            #endregion

            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
/*
            if (cabecera.Count == 0)
            {
                return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "001", Message = "La consulta no retorna datos." };
            }
            foreach (var item in cabecera)
            {
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

*/
/*
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
*/            
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