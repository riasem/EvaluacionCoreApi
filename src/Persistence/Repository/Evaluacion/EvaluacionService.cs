﻿using Dapper;
using EnrolApp.Application.Features.Marcacion.Specifications;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.EvalCore.Commands.Specifications;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Specifications;
using EvaluacionCore.Application.Features.Permisos.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using EvaluacionCore.Domain.Entities.Permisos;
using EvaluacionCore.Domain.Entities.Vacaciones;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace EvaluacionCore.Persistence.Repository.Employees;

public class EvaluacionService : IEvaluacion
{
    private readonly ILogger<EvaluacionService> _log;
    private readonly IConfiguration _config;
    private readonly IRepositoryAsync<MarcacionColaborador> _repoMarcaColAsync;
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoColAsync;
    private readonly IRepositoryAsync<LocalidadColaborador> _repositoryLocalidadColAsync;
    private readonly IRepositoryAsync<Turno> _repositoryTurnoAsync;
    private readonly IRepositoryAsync<SolicitudPermiso> _repositorySoliPermisoAsync;
    private readonly IRepositoryAsync<SolicitudVacacion> _repositorySoliVacacionAsync;
    private readonly IRepositoryGRiasemAsync<UserInfo> _repoUserInfoAsync;
    private readonly IRepositoryGRiasemAsync<CheckInOut> _repoCheckInOutAsync;
    private string ConnectionString_Marc { get; }
    private string ConnectionString { get; }

    public EvaluacionService(IRepositoryGRiasemAsync<UserInfo> repoUserInfoAsync, IRepositoryGRiasemAsync<CheckInOut> repoCheckInOutAsync, 
        ILogger<EvaluacionService> log, IConfiguration config, IRepositoryAsync<MarcacionColaborador> repositoryMarcaCol,  
        IRepositoryAsync<TurnoColaborador> repositoryTurnoCol, IRepositoryAsync<LocalidadColaborador> repositoryLocalidadCol,
        IRepositoryAsync<Turno> repositoryTurno, IRepositoryAsync<SolicitudPermiso> repositorySoliPermisoAsync,
        IRepositoryAsync<SolicitudVacacion> repositorySoliVacacionAsync)
    {
        _repoUserInfoAsync = repoUserInfoAsync;
        _repoCheckInOutAsync = repoCheckInOutAsync;
        _log = log;
        _config = config;
        ConnectionString = _config.GetConnectionString("Bd_Rrhh_Panacea");
        ConnectionString_Marc = _config.GetConnectionString("DefaultConnection");
        _repoMarcaColAsync = repositoryMarcaCol;
        _repoTurnoColAsync = repositoryTurnoCol;
        _repositoryLocalidadColAsync = repositoryLocalidadCol;
        _repositoryTurnoAsync = repositoryTurno;
        _repositorySoliPermisoAsync = repositorySoliPermisoAsync;
        _repositorySoliVacacionAsync = repositorySoliVacacionAsync;
    }

    public async Task<(string response, int success)> EvaluateAsistencias(string identificacion, DateTime? fechaDesde, DateTime? fechaHasta)
    {
        try
        {
            if (!string.IsNullOrEmpty(identificacion) && fechaDesde != null && fechaHasta != null)
            {
                var (resp1, sucess1) = await EvaluaConParams(identificacion, fechaDesde, fechaHasta);
                return (resp1, sucess1);
            }

            if (fechaDesde != null && fechaHasta != null)
            {
                var resp2 = EvaluaSinParams();
                return (resp2, 1);
            }

            return ("",1);
        }

        catch (Exception e)
        {
            return (e.Message, 0);
        }
    }
    public async Task<(string resp, int succ)> EvaluaConParams(string identificacion, DateTime? fechaDesde, DateTime? fechaHasta)
    {
        try
        {
            DateTime primeraMarcacion;
            DateTime ultimaMarcacion;

            var objLocalidad = await _repositoryLocalidadColAsync.FirstOrDefaultAsync(new GetLocalidadColaByIdentificacionSpec(identificacion));
            if (objLocalidad == null) return ("No tiene Localidad asignada", 0);

            //var objTurno = await _repoTurnoColAsync.ListAsync(new TurnosByIdClienteSpec(objLocalidad.Colaborador.Id));
            //if (!objTurno.Any()) return ("No tiene turnos asignados", 0);

            var codigoConviviencia = objLocalidad.Colaborador.CodigoConvivencia;

            var userInfo = await _repoUserInfoAsync.FirstOrDefaultAsync(new UserMarcacionByCodigoSpec(codigoConviviencia));

            var objMarcacionBase = await _repoCheckInOutAsync.ListAsync(new GetMarcacionesByRangeDateSpec(userInfo.UserId,fechaDesde,fechaHasta));

            
            //if (!objMarcacion.Any()) return ("No posee marcaciones el colaborador", 0);
            TimeSpan difFechas = DateTime.Parse(fechaHasta.ToString()) - DateTime.Parse(fechaDesde.ToString());

            for (int i = 0; i <= difFechas.Days; i++)
            {
                var fechanueva = DateTime.Parse(fechaDesde.ToString()).AddDays(i);

                var objMarcacion = await _repoMarcaColAsync.FirstOrDefaultAsync(new MarcacionByColaborador(objLocalidad.Colaborador.Id, fechanueva));

                if (objMarcacion is not null)
                {
                    var objPermiso = await _repositorySoliPermisoAsync.ListAsync(new GetPermisoRangeDateSpec(Convert.ToInt32(codigoConviviencia), fechanueva));






                    if (objMarcacion.EstadoMarcacionEntrada != "C") // Correcto
                    {
                        


                    }
                        
                    if (objMarcacion.SalidaEntrada != "C")
                    {

                    }
                }
                else
                {
                    var objVacacion = _repositorySoliVacacionAsync.FirstOrDefaultAsync(new GetVacacionRangeDateSpec(Convert.ToInt32(codigoConviviencia), fechanueva));

                }


                //}


            }













            return ("",1);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public string EvaluaSinParams()
    {
        try
        {
            return "";

        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<List<BitacoraMarcacionType>> ConsultaMarcaciones(string Identificacion, DateTime fechaDesde, DateTime fechaHasta, string codigoMarcacion)
    {
        List<BitacoraMarcacionType> bitacoraMarcacion = new();

        try
        {
            //del  turno se saca hora entrada, salida. Margen posterior y previo y los codigos de marcacion
            string query = "SELECT top 1 * FROM V_BIOMETRICO WHERE CEDULA = '" + Identificacion + "' AND time BETWEEN '" + fechaDesde.ToString("yyyy/MM/dd HH:mm:ss") + "' AND '" + fechaHasta.ToString("yyyy/MM/dd HH:mm:ss") + "' And state = '" + codigoMarcacion + "' Order by time desc";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            bitacoraMarcacion = (await con.QueryAsync<BitacoraMarcacionType>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return bitacoraMarcacion;
    }

    public async Task<List<ControlAsistenciaType>> ConsultaAsistencia(string CodigoBiometrico, DateTime fechaDesde, DateTime fechaHasta)
    {
        List<ControlAsistenciaType> controlAsistencia = new();

        try
        {
            // Vista que permite consultar en un rango de fechas todos los turnos que le han sido asignados un colaborador y las marcaciones que ha realizado en estos turnos
            // asi como las novedades que existieren en las marcaciones y las solicitudes que hubiesen sido gestionadas por estas novedades
            string query = "SELECT * FROM GRIAMSE.dbo.VT_CASISTENCIA WHERE CODIGOBIOMETRICO = '" + CodigoBiometrico + "' AND FECHAASGINACION BETWEEN '" + fechaDesde.ToString("yyyy/MM/dd HH:mm:ss") + "' AND '" + fechaHasta.ToString("yyyy/MM/dd HH:mm:ss") + "' ORDER BY FECHAASGINACION ASC";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            controlAsistencia = (await con.QueryAsync<ControlAsistenciaType>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return controlAsistencia;
    }

    public async Task<List<ConsultaSolicitudPermisoType>> ConsultaSolicitudesAprobadasbyCodigoBiometrico(string CodigoBiometrico, DateTime fecha)
    {
        List<ConsultaSolicitudPermisoType> consultaSolicitudPermiso = new();

        try
        {
            // Vista que permite consultar en un rango de fechas todos los turnos que le han sido asignados un colaborador y las marcaciones que ha realizado en estos turnos
            // asi como las novedades que existieren en las marcaciones y las solicitudes que hubiesen sido gestionadas por estas novedades
            string query = "SELECT * FROM Riasem.dbo.V_SOLICITUD_PERMISO WHERE CODIGOBIOMETRICOBENEFICIARIO = '" + CodigoBiometrico + "' AND '" + fecha.ToString("yyyy/MM/dd HH:mm:ss") + "' BETWEEN FECHAPERMISODESDE AND FECHAPERMISOHASTA AND CODIGOESTADO = 'APROBADA' ORDER BY FECHAPERMISODESDE ASC";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            consultaSolicitudPermiso = (await con.QueryAsync<ConsultaSolicitudPermisoType>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return consultaSolicitudPermiso;
    }

    public async Task<List<ConsultaSolicitudPermisoType>> ConsultaSolicitudbyIdSolicitud(string IdSolicitud, string EstadoSolicitud)
    {
        List<ConsultaSolicitudPermisoType> consultaSolicitudPermiso = new();

        try
        {
            // Vista que permite consultar en un rango de fechas todos los turnos que le han sido asignados un colaborador y las marcaciones que ha realizado en estos turnos
            // asi como las novedades que existieren en las marcaciones y las solicitudes que hubiesen sido gestionadas por estas novedades
            string query = "SELECT * FROM Riasem.dbo.V_SOLICITUD_PERMISO WHERE IDSOLICITUDPERMISO = '" + IdSolicitud.ToString() + "' AND CODIGOESTADO = '" + EstadoSolicitud + "'";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            consultaSolicitudPermiso = (await con.QueryAsync<ConsultaSolicitudPermisoType>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return consultaSolicitudPermiso;
    }

    public async Task<List<ColaboradorConvivenciaType>> ConsultaColaboradores(string codUdn, string codArea, string codScosto, string suscriptor)
    {
        List<ColaboradorConvivenciaType> bitacoraMarcacion = new();
        string Udn    = !string.IsNullOrEmpty(codUdn) ? codUdn  : "codUdn";
        string Area   = !string.IsNullOrEmpty(codArea) ? codArea : "codArea";
        string Scosto = !string.IsNullOrEmpty(codScosto) ? codScosto : "codSubcentroCosto";
        //string Susc   = !string.IsNullOrEmpty(suscriptor) ? suscriptor : "%";
        try
        {
            string query = "SELECT identificacion, Empleado, codigoBiometrico, desUdn, desArea, desSubcentroCosto FROM V_COLABORADORES_CONVIVENCIA WHERE codUdn = '" + Udn + "' AND codArea= '" + Area + "' AND codSubcentroCosto =  '" + Scosto + "' ";
            if (!string.IsNullOrEmpty(suscriptor))
            {
                query += " and (Empleado LIKE CONCAT('%', '" + suscriptor + "', '%') OR codigoBiometrico = '" + suscriptor + "' OR identificacion = '" + suscriptor + "') ";
            }
            query += " order by desUdn, desArea, desCentroCosto, desSubcentroCosto, Empleado";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            bitacoraMarcacion = (await con.QueryAsync<ColaboradorConvivenciaType>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return bitacoraMarcacion;
    }

    public async Task<List<ColaboradorConvivenciaType>> ConsultaColaborador(string suscriptor)
    {
        List<ColaboradorConvivenciaType> bitacoraMarcacion = new();
        //string Susc   = !string.IsNullOrEmpty(suscriptor) ? suscriptor : "%";
        try
        {
            string query = "SELECT identificacion, Empleado, codigoBiometrico, desUdn, desArea, desSubcentroCosto FROM V_COLABORADORES_CONVIVENCIA WHERE ";
            query += " identificacion = '" + suscriptor + "' ";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            bitacoraMarcacion = (await con.QueryAsync<ColaboradorConvivenciaType>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return bitacoraMarcacion;
    }
    

    public async Task<List<ControlAsistenciaCab>> ConsultaControlAsistenciaCab(string codUdn, string codArea, string codScosto, string periodo, string suscriptor)
    {
        List<ControlAsistenciaCab> bitacoraMarcacion = new();
        //string Udn = !string.IsNullOrEmpty(codUdn) ? codUdn : "udn";
        //string Area = !string.IsNullOrEmpty(codArea) ? codArea : "area";
        //string Scosto = !string.IsNullOrEmpty(codScosto) ? codScosto : "subcentroCosto";
        string query = "";
        //string Susc   = !string.IsNullOrEmpty(suscriptor) ? suscriptor : "%";
        try
        {
            if (!string.IsNullOrEmpty(suscriptor))
            {
                query += "SELECT top 1 * FROM GRIAMSE.dbo.controlAsistenciaCab WHERE udn = '"
                            + codUdn + "' AND area= '" + codArea + "' AND periodo= '" + periodo + "' ";
                query += " and identificacion =  '" + suscriptor + "' ";
            }
            else
            {
                query += "select id, periodo, fechaDesde, fechaHasta, identificacion, idColaborador, udn, area, subcentroCosto, fechaRegistro, usuarioRegistro " +
                         "from (SELECT *, ROW_NUMBER() OVER(PARTITION BY identificacion ORDER BY fechaRegistro DESC) rn " +
                         "FROM GRIAMSE.dbo.controlAsistenciaCab) a WHERE rn > 1 and udn = '"
                            + codUdn + "' AND area= '" + codArea +  "'  AND periodo= '" + periodo + "' ";
            }
            if (!string.IsNullOrEmpty(codScosto))
            {
                query += " AND subcentroCosto =  '" + codScosto + "' ";
            }
            query += " order by fechaRegistro desc";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            bitacoraMarcacion = (await con.QueryAsync<ControlAsistenciaCab>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return bitacoraMarcacion;
    }
    public async Task<List<ControlAsistenciaDet>> ConsultaControlAsistenciaDet(int idControlAsistenciaCab)
    {
        List<ControlAsistenciaDet> bitacoraMarcacion = new();
        try
        {
            string query = "SELECT * FROM GRIAMSE.dbo.controlAsistenciaDet WHERE idControlAsistenciaCab = " + idControlAsistenciaCab + " order by fecha asc";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            bitacoraMarcacion = (await con.QueryAsync<ControlAsistenciaDet>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return bitacoraMarcacion;
    }    
    public async Task<List<ControlAsistenciaNovedad>> ConsultaControlAsistenciaNovedad(int idControlAsistenciaDet)
    {
        List<ControlAsistenciaNovedad> bitacoraMarcacion = new();
        try
        {
            string query = "SELECT * FROM GRIAMSE.dbo.controlAsistenciaNovedad WHERE idControlAsistenciaDet = " + idControlAsistenciaDet + " order by fecha desc";

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();

            bitacoraMarcacion = (await con.QueryAsync<ControlAsistenciaNovedad>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return bitacoraMarcacion;
    }
}
