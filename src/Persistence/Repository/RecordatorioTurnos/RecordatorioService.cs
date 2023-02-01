using ClosedXML.Excel;
using Dapper;
using EnrolApp.Application.Features.RecordatorioTurnos.Specifications;
using EvaluacionCore.Application.Common.Interfaces;
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
using Workflow.Persistence.Repository.RecordatorioTurnos;

namespace EvaluacionCore.Persistence.Repository.RecordatorioTurnos;


public class RecordatorioService : IRecordatorio
{
    private readonly ILogger<MarcacionColaborador> _log;
    private readonly IRepositoryAsync<Cliente> _repoCliente;
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoCola;
    private readonly IRepositoryAsync<Recordatorio> _repoRecordatorio;
    private readonly IRepositoryAsync<NovedadRecordatorioCab> _repoNovedadRecordatorioCab;
    private readonly IRepositoryAsync<NovedadRecordatorioDet> _repoNovedadRecordatorioDet;
    private readonly IApisConsumoAsync _repositoryApis;
    private readonly IConfiguration _config;
    private string ConnectionString_Marc { get; }
    private string ConnectionString { get; }
    private readonly string UrlBaseApiUtils = string.Empty;
    private string nombreEnpoint = string.Empty;
    private string uriEnpoint = string.Empty;



    public RecordatorioService(
        IRepositoryAsync<Recordatorio> repoRecordatorio, 
        IRepositoryAsync<NovedadRecordatorioCab> repoNovedadRecordatorioCab,
        IRepositoryAsync<NovedadRecordatorioDet> repoNovedadRecordatorioDet, 
        ILogger<MarcacionColaborador> log, 
        IRepositoryAsync<TurnoColaborador> repoTurnoCola, 
        IConfiguration config, 
        IRepositoryAsync<Cliente> repoCliente,
        IApisConsumoAsync repositoryApis)
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
        UrlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
        _repositoryApis = repositoryApis;
    }

    public async Task<(string response, int success)> ProcesarRecordatorios(CancellationToken cancellationToken)
    {
        try
        {
            DateTime hoy = DateTime.Today;
            string periodo = hoy.ToString("yyyy-MM");
            var objRecordatorio = await _repoRecordatorio.FirstOrDefaultAsync(new RecordatorioByPeriodoSpec(periodo),cancellationToken);
            var objRecordatorio_ = await _repoNovedadRecordatorioDet.ListAsync(cancellationToken);
            string messageId = "";
            string[] dataVariable;
            string plantilla = "";

            string tipoRecordatorio = "";
            int diasRecodatorio = 0;


            if (objRecordatorio != null)
            {
                if (objRecordatorio.InicioRecordatorio >= hoy && objRecordatorio.FechaLimite <= hoy)
                {
                    //RECORDATORIO (RC) -- ENTRE INICIO DE RECORDATORIO Y ANTES DE FECHA LIMITE
                    tipoRecordatorio = "RC";
                    diasRecodatorio = objRecordatorio.FechaLimite.Day - hoy.Day;
                    messageId = _config.GetSection("Sms:Plantilla:RecordatorioTurnosRC").Get<string>();
                    //dataVariable[1] = diasRecodatorio.ToString();
                    //dataVariable[2] = objRecordatorio.FechaLimite.ToString("dd/MM/yyyy");
                    dataVariable = new string[] { "", diasRecodatorio.ToString(), objRecordatorio.FechaLimite.ToString("dd/MM/yyyy") };
                    plantilla = "AlertaTurnosRC";
                }
                else if (objRecordatorio.FechaLimite == hoy)
                {
                    //DIA LIMITE (LM)
                    tipoRecordatorio = "LM";
                    diasRecodatorio = 0;
                    messageId = _config.GetSection("Sms:Plantilla:RecordatorioTurnosLM").Get<string>();
                    dataVariable = new string[] { "", objRecordatorio.FechaLimite.ToString("dd/MM/yyyy") };
                    plantilla = "AlertaTurnosLM";
                }
                else if (hoy > objRecordatorio.FechaLimite && hoy < objRecordatorio.FinRecordatorio)
                {
                    //ALERTA (AL) -- PASADO EL DIA LIMITE
                    tipoRecordatorio = "AL";
                    diasRecodatorio = hoy.Day - objRecordatorio.FechaLimite.Day;
                    messageId = _config.GetSection("Sms:Plantilla:RecordatorioTurnosAL").Get<string>();
                    dataVariable = new string[] { "", objRecordatorio.FechaLimite.ToString("dd/MM/yyyy"), diasRecodatorio.ToString() };
                    plantilla = "AlertaTurnosAL";
                }
                else
                {
                    return ("", 1);
                }
            }
            else
            {
                return ("No hay nada que procesar", 1);
            }

            //inicio del mes
            DateTime inicioMes = objRecordatorio.FechaLimite.AddDays(-1 * (double.Parse(objRecordatorio.FechaLimite.Day.ToString()) - 1));
            //fin del mes
            DateTime finMes = inicioMes.AddDays((DateTime.DaysInMonth(int.Parse(DateTime.Today.Year.ToString()), int.Parse(DateTime.Today.Month.ToString()))) - 1);

            var objColaboradoresJefes = await ConsultarJefes();

            foreach (var jefe in objColaboradoresJefes)
            {
                var objColaboradores = await ConsultarColaboradores(jefe.Id);

                NovedadRecordatorioCab novedadRecordatorioCab = new()
                {
                    Id = Guid.NewGuid(),
                    IdJefe = jefe.Id,
                    FechaEvaluacion = DateTime.Now,
                    TipoRecordatorio = tipoRecordatorio,
                    DiasRecordatorio = diasRecodatorio,
                    Estado = "PR" //PROCESADO
                };
                var cab = await _repoNovedadRecordatorioCab.AddAsync(novedadRecordatorioCab, cancellationToken);

                foreach (var col in objColaboradores)
                {
                    for (DateTime dtm = inicioMes; dtm <= finMes; dtm = dtm.AddDays(1))
                        {
                        var objTurnoCol = await _repoTurnoCola.ListAsync(new TurnoColaboradorTreeSpec(col.Identificacion, dtm), cancellationToken);

                        if (objTurnoCol.Count == 0)
                        {
                            NovedadRecordatorioDet novedadRecordatorioDet = new()
                            {
                                Id = Guid.NewGuid(),
                                IdNovedadRecordatorioCab = cab.Id,
                                FechaEvaluacion = DateTime.Now,
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


            //SE REALIZA EL PROCESO DE ENVIO DE CORREOS Y MENSAJES DE RECORDATORIO
            var objNovedades = await _repoNovedadRecordatorioCab.ListAsync(new NovedadRecordatorioCabByPeriodoSpec(periodo), cancellationToken);

            foreach (var itemNov in objNovedades)
            {
                var objNovedadesDet = await _repoNovedadRecordatorioDet.ListAsync(new NovedadRecordatorioDetByCabSpec(itemNov.Id), cancellationToken);

                if (objNovedadesDet.Count > 0)
                {
                    var objJefe = await _repoCliente.GetByIdAsync(itemNov.IdJefe, cancellationToken);

                    //SE PROCESA LOS DETALLES DE LA NOVEDAD COMO XLS
                    #region Generar Xls

                    var objRecordatorioXlsx = objNovedadesDet.Select(x => new
                    {
                        x.NombreColaborador,
                        x.IdentificacionColaborador,
                        x.Udn,
                        x.Area,
                        x.SubcentroCosto,
                        x.FechaNoAsignada
                    }).OrderByDescending(x => x.NombreColaborador).ThenByDescending(x => x.FechaNoAsignada).ToList();

                    XLWorkbook workbook = new();
                    ListtoDataTableConverter converter = new();
                    DataTable dt = converter.ToDataTable(objRecordatorioXlsx);
                    workbook.Worksheets.Add(dt, "Novedades");
                    workbook.SaveAs("Novedades.xlsx");

                    byte[] docBytes = ReadFile("Novedades.xlsx");
                    String base64EncodedPDF = Convert.ToBase64String(docBytes);
                    
                    #endregion

                    //SE REALIZA ENVIO DE SMS
                    dataVariable[0] = objJefe.Alias;
                    #region Envio Sms
                    var objEnviarSms = new
                    {
                        celular = objJefe.Celular,
                        messageId,
                        dataVariable,
                        identificacion = objJefe.Identificacion
                    };
                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarSms").Get<string>();
                    uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                    var resultSms = await _repositoryApis.PostEndPoint(objEnviarSms, uriEnpoint, nombreEnpoint);

                    if (!resultSms.Success)
                    {
                        return ("Ocurrió un error al enviar el Mensaje ", 0);
                    }
                    #endregion

                    //SE REALIZA ENVIO DE CORREO
                    #region Envio Correo
                    var objEnviarMail = new
                    {
                        para = objJefe.Correo,
                        alias = objJefe.Alias,
                        plantilla,
                        archivoBase64 = base64EncodedPDF,
                        nombreArchivo = "Novedades.xlsx",
                        asunto = "Recordatorio Asignación de turnos",
                    };
                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                    uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                    var (Success, Data) = await _repositoryApis.PostEndPoint(objEnviarMail, uriEnpoint, nombreEnpoint);

                    if (!Success)
                    {
                        return ("Ocurrió un error al enviar el Correo ", 0);
                    }
                    #endregion
                }
            }

            return ("Se procesa correctamente", 1);
        }
        catch (Exception e)
        {
            return ("Ocurrió un error al procesar " + e.Message, 0);
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
    private byte[] ReadFile(string sourcePath)
    {
        FileInfo fileInfo = new FileInfo(sourcePath);
        long numBytes = fileInfo.Length;
        FileStream fileStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fileStream);
        byte[] data = br.ReadBytes((int)numBytes);
        fileStream.Close();
        return data;
    }
}
