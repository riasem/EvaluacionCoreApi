using EnrolApp.Application.Features.Marcacion.Specifications;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;

namespace EvaluacionCore.Persistence.Repository.Employees;

public class EvaluacionService : IEvaluacion
{
    private readonly ILogger<EvaluacionService> _log;
    private readonly IConfiguration _config;
    private readonly IRepositoryAsync<MarcacionColaborador> _repoMarcaColAsync;
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoColAsync;
    private readonly IRepositoryAsync<LocalidadColaborador> _repositoryLocalidadColAsync;
    private readonly IRepositoryAsync<Turno> _repositoryTurnoAsync;
    private readonly IRepositoryGRiasemAsync<UserInfo> _repoUserInfoAsync;
    private readonly IRepositoryGRiasemAsync<CheckInOut> _repoCheckInOutAsync;
    private string ConnectionString_Marc { get; }
    private string ConnectionString { get; }

    public EvaluacionService(IRepositoryGRiasemAsync<UserInfo> repoUserInfoAsync, IRepositoryGRiasemAsync<CheckInOut> repoCheckInOutAsync, 
        ILogger<EvaluacionService> log, IConfiguration config, IRepositoryAsync<MarcacionColaborador> repositoryMarcaCol,  
        IRepositoryAsync<TurnoColaborador> repositoryTurnoCol, IRepositoryAsync<LocalidadColaborador> repositoryLocalidadCol,
        IRepositoryAsync<Turno> repositoryTurno)
    {
        _repoUserInfoAsync = repoUserInfoAsync;
        _repoCheckInOutAsync = repoCheckInOutAsync;
        _log = log;
        _config = config;
        ConnectionString = _config.GetConnectionString("Bd_Rrhh_Panacea");
        ConnectionString_Marc = _config.GetConnectionString("Bd_Marcaciones_GRIAMSE");
        _repoMarcaColAsync = repositoryMarcaCol;
        _repoTurnoColAsync = repositoryTurnoCol;
        _repositoryLocalidadColAsync = repositoryLocalidadCol;
        _repositoryTurnoAsync = repositoryTurno;
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

            var objTurno = await _repoTurnoColAsync.ListAsync(new TurnosByIdClienteSpec(objLocalidad.Colaborador.Id));
            if (!objTurno.Any()) return ("No tiene turnos asignados", 0);

            var codigoConviviencia = objLocalidad.Colaborador.CodigoConvivencia;

            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();
            var objMarcacionBase = await con.ExecuteAsync(sql: (" Select * from [GRIAMSE].[dbo].[CHECKINOUT] where USERID = "+ codigoConviviencia +" and CHECKTIME BETWEEN " + fechaDesde +  " AND "+ fechaHasta +" ORDER BY CHECKTIME;"), commandType: CommandType.Text);
            con.Close();


            var objMarcacion = await _repoMarcaColAsync.ListAsync(new MarcacionByColaborador(objLocalidad.Colaborador.Id));
            if (!objMarcacion.Any()) return ("No posee marcaciones el colaborador", 0);


            return ("",1);
        }
        catch (Exception)
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

}
