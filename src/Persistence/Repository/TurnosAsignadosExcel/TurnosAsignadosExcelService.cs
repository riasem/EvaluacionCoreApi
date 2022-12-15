using EvaluacionCore.Application.Features.Turnos.Commands.GetTurnosAsignadosExcel;
using EvaluacionCore.Application.Features.Turnos.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using System.Text.Json;

namespace Workflow.Persistence.Repository.TurnosAsignadosExcel
{
    public class TurnosAsignadosExcelService : ITurnosAsignadosExcel
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TurnosAsignadosExcelService> _log;
        private readonly string ConnectionString = string.Empty;
        private readonly string Esquema = string.Empty;

        public TurnosAsignadosExcelService(ILogger<TurnosAsignadosExcelService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            ConnectionString = _config.GetConnectionString("DefaultConnection");
            Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
        }

        public async Task<string> GetTurnosAsignadosExcelAsync(GetTurnosAsignadosExcelRequest request)
        {
            string responseList = string.Empty;

            try
            {
                string TurnosExcel = _config.GetSection("StoredProcedure:TurnosAsignadosExcel:InfoExcel").Get<string>();

                using IDbConnection con = new SqlConnection(ConnectionString);
                if (con.State == ConnectionState.Closed) con.Open();

                var turnosExcel =  (await con.QueryAsync<dynamic>(
                                   sql: string.Concat(Esquema, TurnosExcel),
                                   param: new
                                   {
                                       udn = request.CodUdn,
                                       area = request.CodArea,
                                       scc = request.CodScc,
                                       fechaDesde = request.FechaDesde,
                                       fechaHasta = request.FechaHasta
                                   },
                                   commandType: CommandType.StoredProcedure)).ToList();

                responseList = JsonSerializer.Serialize(turnosExcel);
                
                if (con.State == ConnectionState.Open) con.Close();
            }
            catch (Exception e)
            {
                _log.LogError(e, string.Empty);
            }

            return responseList;
        }
    }
}
