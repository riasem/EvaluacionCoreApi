using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion;

namespace EvaluacionCore.Persistence.Repository.BitacoraMarcacion
{
    public class BitacoraMarcacionService : IBitacoraMarcacion
    {
        private readonly IConfiguration _config;
        private readonly ILogger<BitacoraMarcacionService> _log;
        private readonly string ConnectionString = string.Empty;
        private readonly string Esquema = string.Empty;

        public BitacoraMarcacionService(ILogger<BitacoraMarcacionService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            ConnectionString = _config.GetConnectionString("DefaultConnection");
            Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
        }

        public async Task<List<BitacoraMarcacionType>> GetBitacoraMarcacionAsync(GetBitacoraMarcacionRequest request)
        {
            List<BitacoraMarcacionType> bitacoraMarcacion = new();

            try
            {
                string BitacoraMarcacion = _config.GetSection("StoredProcedure:Marcacion:BitacoraMarcaciones").Get<string>();

                using IDbConnection con = new SqlConnection(ConnectionString);
                if (con.State == ConnectionState.Closed) con.Open();

                bitacoraMarcacion = (await con.QueryAsync<BitacoraMarcacionType>(
                    sql: string.Concat(Esquema, BitacoraMarcacion),
                    param: new
                    {
                        suscriptor = request.Suscriptor,
                        unidadNegocio = request.CodUdn,
                        area = request.CodArea,
                        subcentro = request.CodSubcentro,
                        codMarcacion = request.CodMarcacion,
                        fechaDesde = ConvertDateYYYYMMDD(request.FechaDesde),
                        fechaHasta = ConvertDateYYYYMMDD(request.FechaHasta)
                    },
                    commandType: CommandType.StoredProcedure)).ToList();

                if (con.State == ConnectionState.Open) con.Close();
            }
            catch (Exception e)
            {
                _log.LogError(e, string.Empty);
            }

            return bitacoraMarcacion;
        }

        public async Task<List<ComboBitacoraMarcacionType>> GetComboBitacoraMarcacionAsync(GetComboBitacoraMarcacionRequest request)
        {
            List<ComboBitacoraMarcacionType> lstCombo = new();

            try
            {
                string BitacoraMarcacion = _config.GetSection("StoredProcedure:Marcacion:ComboBitacoraMarcaciones").Get<string>();

                using IDbConnection con = new SqlConnection(ConnectionString);
                if (con.State == ConnectionState.Closed) con.Open();

                lstCombo = (await con.QueryAsync<ComboBitacoraMarcacionType>(
                    sql: string.Concat(Esquema, BitacoraMarcacion),
                    param: new
                    {
                        tipo = request.Tipo,
                        udn = request.Udn,
                        area = request.Area,
                        identificacion = string.Empty,
                    },
                    commandType: CommandType.StoredProcedure)).ToList();

                if (con.State == ConnectionState.Open) con.Close();
            }
            catch (Exception e)
            {
                _log.LogError(e, string.Empty);
            }

            return lstCombo;
        }

        private static string ConvertDateYYYYMMDD(string fechaDDMMYYYY)
        {
            string fechaYYYYMMDD;

            var fs = fechaDDMMYYYY.Split("/");
            var date = new DateTime(Convert.ToInt32(fs[2]), Convert.ToInt32(fs[1]), Convert.ToInt32(fs[0]));
            fechaYYYYMMDD = date.ToString("yyyy-MM-dd");

            return fechaYYYYMMDD;
        }

    }
}
