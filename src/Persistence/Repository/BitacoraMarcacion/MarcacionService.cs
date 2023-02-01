﻿
using Ardalis.Specification;
using Dapper;
using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EnrolApp.Application.Features.Marcacion.Specifications;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Marcaciones;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace EvaluacionCore.Persistence.Repository.BitacoraMarcacion;


public class MarcacionService : IMarcacion
{
    private readonly ILogger<MarcacionColaborador> _log;
    private readonly IRepositoryGRiasemAsync<UserInfo> _repoUserInfoAsync;
    private readonly IRepositoryGRiasemAsync<CheckInOut> _repoCheckInOutAsync;
    private readonly IRepositoryGRiasemAsync<AccMonitorLog> _repoMonitorLogAsync;
    private readonly IRepositoryGRiasemAsync<AccMonitoLogRiasem> _repoMonitoLogRiasemAsync;
    private readonly IRepositoryAsync<Localidad> _repoLocalidad;
    private readonly IRepositoryAsync<Cliente> _repoCliente;
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoCola;
    private readonly IRepositoryAsync<MarcacionColaborador> _repoMarcacionCola;
    private readonly IConfiguration _config;
    private readonly string Esquema = null;
    private string ConnectionString_Marc { get; }
    private string NombreStoreProcedure = null;



    public MarcacionService(IRepositoryGRiasemAsync<UserInfo> repoUserInfoAsync, IRepositoryGRiasemAsync<CheckInOut> repoCheckInOutAsync,
        IRepositoryAsync<Localidad> repoLocalidad,ILogger<MarcacionColaborador> log, 
        IRepositoryAsync<TurnoColaborador> repoTurnoCola, IConfiguration config, 
        IRepositoryAsync<MarcacionColaborador> repoMarcacionCola, IRepositoryGRiasemAsync<AccMonitorLog> repoMonitorLogAsync,
        IRepositoryAsync<Cliente> repoCliente,
        IRepositoryGRiasemAsync<AccMonitoLogRiasem> repoMonitoLogRiasemAsync)
    {
        _repoUserInfoAsync = repoUserInfoAsync;
        _repoCheckInOutAsync = repoCheckInOutAsync;
        _config = config;
        _repoLocalidad = repoLocalidad;
        _log = log;
        _repoMarcacionCola = repoMarcacionCola;
        ConnectionString_Marc = _config.GetConnectionString("Bd_Marcaciones_GRIAMSE");
        Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
        _repoTurnoCola = repoTurnoCola;
        _repoMonitorLogAsync = repoMonitorLogAsync;
        _repoCliente = repoCliente;
        _repoMonitoLogRiasemAsync = repoMonitoLogRiasemAsync;
        NombreStoreProcedure = _config.GetSection("StoredProcedure:Marcacion:ReprocesaMarcaciones").Get<string>();
    }
    public async Task<ResponseType<MarcacionResponseType>> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken)
    {
        try
        {
            var marcacionColaborador = DateTime.Now;
            MarcacionResponseType objResultFinal = new();
            var objLocalidad = await _repoLocalidad.FirstOrDefaultAsync(new GetLocalidadByIdSpec(Request.LocalidadId, Request.CodigoEmpleado), cancellationToken);

            if (objLocalidad == null) return new ResponseType<MarcacionResponseType>() { Message = "Localidad incorrecta", Succeeded = true, StatusCode = "101" };

            //Validación de turno que corresponde
            //var objTurno = await _repoTurnoCola.ListAsync(new TurnosByIdClienteSpec(objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Id), cancellationToken);

            //if (Request.DispositivoId == objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.DispositivoId)
            //{

                AccMonitorLog accMonitorLog = new()
                {
                    State = 0,
                    Time = marcacionColaborador,
                    Pin = objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador?.CodigoConvivencia,
                    Device_Id = 999, //parametrizar desde el request
                    Verified = 0,
                    Device_Name = "EnrolApp", //parametrizar desde el request
                    Status = 1,
                    Create_Time = DateTime.Now
                };

                var objResultado = await _repoMonitorLogAsync.AddAsync(accMonitorLog, cancellationToken);
                if (objResultado is null)
                {
                    return new ResponseType<MarcacionResponseType>() { Message = "No se ha podido registrar su marcación", StatusCode = "101", Succeeded = true };
                }
                await Task.Delay(1500, cancellationToken);
                var marcacionEmpl = await _repoMonitoLogRiasemAsync.FirstOrDefaultAsync(new MarcacionByColaboradorAndTime(objResultado.Pin, objResultado.Time), cancellationToken);
            
                if (marcacionEmpl is not null)
                {
                //string tipoMarcacion = EvaluaTipoMarcacion(marcacionEmpl.Result.State);
                //string estadoMarcacion = EvaluaEstadoMarcacion(marcacionEmpl.Result.Description);

                    objResultFinal = new()
                    {
                        //MarcacionId = Guid.Parse(marcacionid),  TEMPORAL SE COMENTA HASTA REGULARIZAR 
                        MarcacionId = marcacionEmpl.Id,
                        TipoMarcacion = marcacionEmpl.Estado,
                        EstadoMarcacion = marcacionEmpl.Description
                    };
                }
                else
                {
                    objResultFinal = null;
                }

                var colaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByCodBiometrico(Request.CodigoEmpleado), cancellationToken);
                DateTime fechaDesde = marcacionColaborador.Date;



                string query = "EXEC [dbo].[EAPP_SP_REPROCESA_MARCACIONES] NULL, NULL, NULL, '" + fechaDesde.ToString("yyyy/MM/dd HH:mm:ss") + "' , '" + marcacionColaborador.ToString("yyyy/MM/dd HH:mm:ss") +  "',  '" + colaborador.Identificacion + "';";
                using IDbConnection con = new SqlConnection(ConnectionString_Marc);
                if (con.State == ConnectionState.Closed) con.Open();
                con.Query(query);
                if (con.State == ConnectionState.Open) con.Close();
                
                return new ResponseType<MarcacionResponseType>() { Data = objResultFinal, Message = "Marcación registrada correctamente", StatusCode = "100", Succeeded = true };
            


            //}
            //return new ResponseType<MarcacionResponseType>() { Message = "Debe marcar desde su dispositivo movil.", Succeeded = true, StatusCode = "101" };

        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<MarcacionResponseType>() { Message = CodeMessageResponse.GetMessageByCode("102"), StatusCode = "102", Succeeded = false };
        }


    }

    public  async Task<ResponseType<List<ConsultaRecursoType>>> ConsultarRecursos(Guid Identificacion, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken)
    {
        var objClienteCargo = await _repoCliente.ListAsync(new ClientePadreById(Identificacion), cancellationToken);

        if (objClienteCargo == null) return new ResponseType<List<ConsultaRecursoType>>() { Message = "No tiene personal a cargo", StatusCode = "001", Succeeded = true};

        List<ConsultaRecursoType> listRecursos = new();

        foreach (var itemCliente in objClienteCargo)
        {
            var objTurnoColaborador = await _repoTurnoCola.ListAsync(new TurnoColaboradorByIdentificacionSpec(itemCliente.Identificacion, fechaDesde, fechaHasta), cancellationToken);
            var marcacionesColaborador = await _repoMarcacionCola.ListAsync(new MarcacionByRangeFechaSpec(itemCliente.Identificacion, fechaDesde, fechaHasta), cancellationToken);
            var marcaMonitor = await _repoMonitoLogRiasemAsync.ListAsync(new MarMonitorByRangeFechaSpec(itemCliente.CodigoConvivencia, fechaDesde, fechaHasta), cancellationToken);
            TimeSpan difFechas = fechaHasta - fechaDesde;
            List<Dias> dias = new();
            for (int i = 0; i <= difFechas.Days; i++)
            {
                var fechanueva = DateTime.Parse(fechaDesde.ToString()).AddDays(i);
                var turnoAsig = objTurnoColaborador.Where(x => x.FechaAsignacion.Date == fechanueva.Date && x.Turno.ClaseTurno.CodigoClaseturno == "LABORA").FirstOrDefault();
                var turnoAsigReceso = objTurnoColaborador.Where(x => x.FechaAsignacion.Date == fechanueva.Date && x.Turno.ClaseTurno.CodigoClaseturno == "RECESO").FirstOrDefault();
                var marcacionCliente = marcacionesColaborador.Where(x => x.FechaCreacion.Date == fechanueva.Date && x.EstadoProcesado == true).FirstOrDefault();
                var marcacionMonitor = marcaMonitor.Where(x => x.Time.Value.Date == fechanueva.Date).ToList();
                var localidadDescripcion = string.Empty;
                var tHAsignada = string.Empty;
                var tHTrabajadas = string.Empty;
                if (turnoAsig == null) { tHAsignada = "0"; } else { tHAsignada = turnoAsig.Turno.TotalHoras; };
                if (marcacionCliente != null)
                {
                    if (marcacionCliente.MarcacionSalida != null && marcacionCliente.MarcacionEntrada != null)
                    {
                        //var hReceso = turnoAsigReceso.Turno.TotalHoras;
                        tHTrabajadas = ((marcacionCliente.MarcacionSalida.Value.TimeOfDay - marcacionCliente.MarcacionEntrada.Value.TimeOfDay).TotalHours - Convert.ToDouble(turnoAsigReceso.Turno.TotalHoras)).ToString();
                    }else /*if (marcacionCliente.MarcacionEntrada != null && marcacionCliente.MarcacionSalida == null)*/
                    {
                        tHTrabajadas = "0";
                    }

                    Dias newDias = new()
                    {
                        Fecha = fechanueva,
                        HorasAsignadas = Math.Round(Convert.ToDouble(tHAsignada),2).ToString(),
                        HorasPendiente = marcacionCliente.TotalAtraso == null ? TimeSpan.Zero.TotalHours.ToString() : Math.Round(marcacionCliente.TotalAtraso.Value.TimeOfDay.TotalHours,2).ToString(),
                        HorasTrabajada = Math.Round(Convert.ToDouble(tHTrabajadas),2) >= 8 ? "8" : Math.Round(Convert.ToDouble(tHTrabajadas), 2).ToString(),
                        LocalidadDescripcion = marcacionCliente.LocalidadColaborador.Localidad.Descripcion,
                        

                    };

                    dias.Add(newDias);
                    continue;
                }
                // no tiene marcacion en tabla principal
                var hTTrabajadas = string.Empty;

                if (marcacionMonitor.Any())
                {
                    var mEntrada = marcacionMonitor.Where(x => x.Time.Value.Date == fechanueva.Date && x.State == 10).OrderBy(x => x.Time).FirstOrDefault();
                    var mSalida = marcacionMonitor.Where(x => x.Time.Value.Date == fechanueva.Date && x.State == 11).OrderByDescending(x => x.Time).FirstOrDefault();
                    if (mEntrada != null && mSalida != null)
                    {
                        hTTrabajadas = (mSalida.Time.Value.TimeOfDay - mEntrada.Time.Value.TimeOfDay).TotalHours.ToString();
                    }
                    else
                    {
                        hTTrabajadas = "0";
                    }
                    

                }
                else
                {
                    hTTrabajadas = "0";
                }

                Dias newDiasSturno = new()
                {
                    Fecha = fechanueva,
                    HorasAsignadas = Math.Round(Convert.ToDouble(tHAsignada), 2).ToString(),
                    HorasTrabajada = Math.Round(Convert.ToDouble(hTTrabajadas), 2) >= 8 ? "8": Math.Round(Convert.ToDouble(hTTrabajadas), 2).ToString()

                };

                dias.Add(newDiasSturno);

                continue;

            }

            listRecursos.Add(new ConsultaRecursoType()
            {
                Colaborador = itemCliente.Nombres + " " + itemCliente.Apellidos,
                Identificacion = itemCliente.Identificacion,
                HTotalAsignadas = Math.Round(dias.Sum(x => Convert.ToDouble(x.HorasAsignadas)),2).ToString(),
                HTotalPendiente = Math.Round(dias.Sum(x => Convert.ToDouble(x.HorasPendiente)),2).ToString(),
                HTotalTrabajadas = Math.Round(dias.Sum(x => Convert.ToDouble(x.HorasTrabajada)),2).ToString(),
                Dias = dias
            });

        }
        return await Task.FromResult(new ResponseType<List<ConsultaRecursoType>>() { Data = listRecursos, Message = "Consulta Correcta", StatusCode = "000", Succeeded = true});
    }

    public static string EvaluaTipoMarcacion(int? state)
    {
        string tipoMarcacion = "";

        switch (state)
        {
            case 10:
                tipoMarcacion = "ENTRADA";
                break;
            case 11:
                tipoMarcacion = "SALIDA";
                break;
            case 14:
                tipoMarcacion = "ENTRADA RECESO";
                break;
            case 15:
                tipoMarcacion = "RETORNO DEL RECESO";
                break;
            default:
                break;
        }
        return tipoMarcacion;
    }


    public static string EvaluaEstadoMarcacion(string desciption)
    {
        string estadoMarcacion = "";

        if (desciption.Contains("HA EXCEDIDO"))
        {
            estadoMarcacion = "ER";
        }
        if(desciption.Contains("ATRASO DE"))
        {
            estadoMarcacion = "AI";
        }
        if(desciption.Contains("SALIDA ANTICIPADA"))
        {
            estadoMarcacion = "SI";
        }
        return estadoMarcacion;
    }
}
