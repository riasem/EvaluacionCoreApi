using Dapper;
using EnrolApp.Application.Features.RecordatorioTurnos.Specifications;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Organizacion;
using EvaluacionCore.Domain.Entities.Permisos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace EvaluacionCore.Persistence.Repository.RecordatorioTurnos;


public class RecordatorioService : IRecordatorio
{
    private readonly ILogger<MarcacionColaborador> _log;
    private readonly IRepositoryAsync<Cliente> _repoCliente;
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoCola;
    private readonly IRepositoryAsync<Recordatorio> _repoRecordatorio;
    private readonly IRepositoryAsync<NovedadRecordatorioCab> _repoNovedadRecordatorioCab;
    private readonly IRepositoryAsync<NovedadRecordatorioDet> _repoNovedadRecordatorioDet;
    private readonly IConfiguration _config;
    private string ConnectionString_Marc { get; }
    private string ConnectionString { get; }



    public RecordatorioService(
        IRepositoryAsync<Recordatorio> repoRecordatorio, 
        IRepositoryAsync<NovedadRecordatorioCab> repoNovedadRecordatorioCab,
        IRepositoryAsync<NovedadRecordatorioDet> repoNovedadRecordatorioDet, 
        ILogger<MarcacionColaborador> log, 
        IRepositoryAsync<TurnoColaborador> repoTurnoCola, 
        IConfiguration config, 
        IRepositoryAsync<Cliente> repoCliente)
    {
        _config = config;
        _log = log;
        ConnectionString_Marc = _config.GetConnectionString("Bd_Marcaciones_GRIAMSE");
        ConnectionString = _config.GetConnectionString("DefaultConnection");
        _repoTurnoCola = repoTurnoCola;
        _repoCliente = repoCliente;
        _repoRecordatorio = repoRecordatorio;
        _repoNovedadRecordatorioCab = repoNovedadRecordatorioCab;
        _repoNovedadRecordatorioDet = repoNovedadRecordatorioDet;
    }

    public async Task<(string response, int success)> ProcesarRecordatorios(CancellationToken cancellationToken)
    {
        try
        {
            DateTime hoy = DateTime.Today;
            string periodo = hoy.ToString("yyyy-MM"); 
            var objRecordatorio = await _repoRecordatorio.FirstOrDefaultAsync(new RecordatorioByPeriodoSpec(periodo),cancellationToken);

            string estado = "";
            string tipoRecordatorio = "";
            int diasRecodatorio = 0;

            if (objRecordatorio != null)
            {
                if (objRecordatorio.InicioRecordatorio >= hoy && objRecordatorio.FechaLimite <= hoy)
                {
                    //RECORDATORIO (RC) -- ENTRE INICIO DE RECORDATORIO Y ANTES DE FECHA LIMITE
                    tipoRecordatorio = "RC";
                    diasRecodatorio = objRecordatorio.FechaLimite.Day - hoy.Day;
                }
                else if (objRecordatorio.FechaLimite == hoy)
                {
                    //DIA LIMITE (LM)
                    tipoRecordatorio = "LM";
                    diasRecodatorio = 0;
                }
                else if (hoy > objRecordatorio.FechaLimite && hoy < objRecordatorio.FinRecordatorio)
                {
                    //ALERTA (AL) -- PASADO EL DIA LIMITE
                    tipoRecordatorio = "AL";
                    diasRecodatorio = hoy.Day - objRecordatorio.FechaLimite.Day;
                }
                else
                {
                    return ("", 1);
                }
            }
            else
            {
                return ("", 1);
            }


            //inicio del mes
            DateTime inicioMes = objRecordatorio.FechaLimite.AddDays(-1 * (double.Parse(objRecordatorio.FechaLimite.Day.ToString()) - 1));
            //fin del mes
            DateTime finMes = inicioMes.AddDays((DateTime.DaysInMonth(int.Parse(DateTime.Today.Year.ToString()), int.Parse(DateTime.Today.Month.ToString()))) - 1);

            var objColaboradoresJefes = await ConsultarJefes();
            //List<ColaboradoresConvivencia> colaboradores = await ConsultarUdn();

            //NovedadRecordatorioCab novedadRecordatorioCab = new();
            //NovedadRecordatorioDet novedadRecordatorioDet = new();

            foreach (var jefe in objColaboradoresJefes)
            {
                var objColaboradores = await ConsultarColaboradores(jefe.Id);

                NovedadRecordatorioCab novedadRecordatorioCab = new()
                {
                    Id = Guid.NewGuid(),
                    IdJefe = jefe.Id,
                    FechaEvaluacion = hoy,
                    TipoRecordatorio = tipoRecordatorio,
                    DiasRecordatorio = diasRecodatorio,
                    Estado = "PR" //PROCESADO
                };
                await _repoNovedadRecordatorioCab.AddAsync(novedadRecordatorioCab, cancellationToken);

                for (DateTime dtm = inicioMes; dtm <= finMes; dtm.AddDays(1))
                {
                    foreach (var col in objColaboradores)
                    {
                        var objTurnoCol = await _repoTurnoCola.ListAsync(new TurnoColaboradorTreeSpec(col.Identificacion, dtm), cancellationToken);

                        if (objTurnoCol == null)
                        {
                            NovedadRecordatorioDet novedadRecordatorioDet = new()
                            {
                                Id = Guid.NewGuid(),
                                IdNovedadRecordatorioCab = novedadRecordatorioCab.Id,
                                FechaEvaluacion = hoy,
                                CodBiometricoColaborador = col.CodigoBiometrico,
                                NombreColaborador = col.Empleado,
                                IdentificacionColaborador = col.Identificacion,
                                Udn = col.DesUdn,
                                Area = col.DesArea,
                                SubcentroCosto = col.DesSubcentroCosto,
                                FechaNoAsignada = dtm
                                
                            };
                            await _repoNovedadRecordatorioDet.AddAsync(novedadRecordatorioDet, cancellationToken);
                        }
                    }

                }
            }

            return ("", 1);
        }
        catch (Exception e)
        {

            throw;
        }
    }


    private async Task<List<Cliente>> ConsultarJefes()
    {
        List<Cliente> colaboradores = new();

        try
        {
            //del  turno se saca hora entrada, salida. Margen posterior y previo y los codigos de marcacion
            string query = "select * from CL_Cliente where id in (select distinct clientePadreId from CL_Cliente);";

            using IDbConnection con = new SqlConnection(ConnectionString);
            if (con.State == ConnectionState.Closed) con.Open();

            colaboradores = (await con.QueryAsync<Cliente>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return colaboradores;
    }

    private async Task<List<ColaboradoresConvivencia>> ConsultarColaboradores(Guid IdJefe)
    {
        List<ColaboradoresConvivencia> colaboradores = new();

        try
        {
            //del  turno se saca hora entrada, salida. Margen posterior y previo y los codigos de marcacion
            string query = "select * from V_COLABORADORES_CONVIVENCIA_JEFE where idClientePadre = " + "'" + IdJefe.ToString() + "';";

            using IDbConnection con = new SqlConnection(ConnectionString);
            if (con.State == ConnectionState.Closed) con.Open();

            colaboradores = (await con.QueryAsync<ColaboradoresConvivencia>(
                query)).ToList();

            if (con.State == ConnectionState.Open) con.Close();
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
        }

        return colaboradores;
    }
}
