using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacionCapacidadesEspeciales;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Common.Exceptions;

namespace EvaluacionCore.Persistence.Repository.BitacoraMarcacion
{
    public class BitacoraMarcacionService : IBitacoraMarcacion
    {
        private readonly IConfiguration _config;
        private readonly ILogger<BitacoraMarcacionService> _log;
        private readonly IRepositoryAsync<ColaboradorConvivencia> _repositoryAsyncColConv;
        private readonly IRepositoryAsync<TurnoColaborador> _repositoryAsyncTc;
        private readonly string ConnectionString = string.Empty;
        private readonly string Esquema = string.Empty;

        public BitacoraMarcacionService(ILogger<BitacoraMarcacionService> log, IConfiguration config, IRepositoryAsync<ColaboradorConvivencia> repositoryAsyncColConv, IRepositoryAsync<TurnoColaborador> repositoryAsyncTc)
        {
            _log = log;
            _config = config;
            ConnectionString = _config.GetConnectionString("DefaultConnection");
            Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
            _repositoryAsyncColConv = repositoryAsyncColConv;
            _repositoryAsyncTc = repositoryAsyncTc;
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

        public async Task<ResponseType<List<BitacoraMarcacionType>>> GetBitacoraMarcacionCapacidadesEspecialesAsync(GetBitacoraMarcacionCapacidadesEspecialesRequest request)
        {
            #region Parametros udn, area y subcentro costo capacidades especiales
            string codUdn = "01";
            string codArea = "02";
            string codScc = "ADMLAF";

            var uidTurnoLibre = Guid.Parse(_config.GetSection("TurnosUid:Libre").Get<string>());
            var uidTurnoVacacion = Guid.Parse(_config.GetSection("TurnosUid:Vacacion").Get<string>());
            var uidTurnoFeriado = Guid.Parse(_config.GetSection("TurnosUid:Feriado").Get<string>());
            #endregion

            List<BitacoraMarcacionType> lstMarcacion = new();
            List<TurnoColaborador> lstTurnoColaborador = new();

            var fechaDesde = DateTime.Parse(request.FechaDesde);
            var fechaHasta = DateTime.Parse(request.FechaHasta);

            try
            {
                var colConv = await _repositoryAsyncColConv.ListAsync(new GetColaboradorConvivenciaByUdnAreaSccSpec(codUdn, codArea, codScc, string.Empty));

                if (!colConv.Any())
                    return new ResponseType<List<BitacoraMarcacionType>>() { Data = lstMarcacion, Message = "No se encontró información de los colaboradores", StatusCode = "001", Succeeded = false };

                foreach (var col in colConv)
                {
                    var turnoColaborador = await _repositoryAsyncTc.ListAsync(new GetTurnoColaboradorByIdentificacion(col.Identificacion, fechaDesde, fechaHasta));
                    turnoColaborador = turnoColaborador.Where(x => x.IdTurno != uidTurnoLibre && x.IdTurno != uidTurnoFeriado && x.IdTurno != uidTurnoVacacion).ToList();
                    lstTurnoColaborador.AddRange(turnoColaborador);
                }

                if (!lstTurnoColaborador.Any())
                    return new ResponseType<List<BitacoraMarcacionType>>() { Data = lstMarcacion, Message = "No se encontraron turnos asignados", StatusCode = "001", Succeeded = false };

                foreach (var ltc in lstTurnoColaborador)
                {
                    var col = colConv.Where(x => x.Identificacion == ltc.Colaborador.Identificacion).FirstOrDefault();

                    string fechaString = ltc.FechaAsignacion.ToString("yyyy-MM-dd");

                    var marcacionEntrada = new BitacoraMarcacionType()
                    {
                        Udn = col.DesUdn,
                        Area = col.DesArea,
                        SubCentroCosto = col.DesSubcentroCosto,
                        Nombre = col.Empleado,
                        Cedula = col.Identificacion,
                        Codigo = col.CodigoBiometrico,
                        FechaHora = string.Concat(fechaString, " ", ltc.Turno.Entrada.ToString("HH:mm:ss")),
                        Fecha = fechaString,
                        Hora = ltc.Turno.Entrada.ToString("HH:mm:ss"),
                        CodEvento = "10",
                        Evento = "ENTRADA"
                    };

                    var marcacionSalida = new BitacoraMarcacionType()
                    {
                        Udn = col.DesUdn,
                        Area = col.DesArea,
                        SubCentroCosto = col.DesSubcentroCosto,
                        Nombre = col.Empleado,
                        Cedula = col.Identificacion,
                        Codigo = col.CodigoBiometrico,
                        FechaHora = string.Concat(fechaString, " ", ltc.Turno.Salida.ToString("HH:mm:ss")),
                        Fecha = fechaString,
                        Hora = ltc.Turno.Salida.ToString("HH:mm:ss"),
                        CodEvento = "11",
                        Evento = "SALIDA"
                    };

                    lstMarcacion.Add(marcacionEntrada);
                    lstMarcacion.Add(marcacionSalida);
                }

                lstMarcacion = lstMarcacion.OrderBy(x => x.FechaHora).ToList();

                return new ResponseType<List<BitacoraMarcacionType>>() { Data = lstMarcacion, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<List<BitacoraMarcacionType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}
