
using Ardalis.Specification;
using Dapper;
using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EnrolApp.Application.Features.Marcacion.Specifications;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Specifications;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Marcaciones;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using EvaluacionCore.Application.Common.Interfaces;

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
    private string ConnectionString_Marc { get; }



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
        _repoTurnoCola = repoTurnoCola;
        _repoMonitorLogAsync = repoMonitorLogAsync;
        _repoCliente = repoCliente;
        _repoMonitoLogRiasemAsync = repoMonitoLogRiasemAsync;
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

            if (Request.DispositivoId == objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.DispositivoId)
            {

                AccMonitorLog accMonitorLog = new()
                {
                    //Id = objMonitorLog.Id + 1,
                    State = 0,
                    Time = marcacionColaborador,
                    Pin = objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.CodigoConvivencia,
                    Device_Id = 999,
                    Verified = 0,
                    Device_Name = "EnrolApp",
                    Status = 1
                };

                var objResultado = await _repoMonitorLogAsync.AddAsync(accMonitorLog, cancellationToken);
                if (objResultado is null)
                {
                    return new ResponseType<MarcacionResponseType>() { Message = "No se ha podido registrar su marcación", StatusCode = "101", Succeeded = true };
                }

                var objMarcacion =  _repoMonitoLogRiasemAsync.ListAsync(cancellationToken).Result.Take(1000).OrderByDescending(e => e.Time);

                var marcacionEmpl = objMarcacion.Where(e => e.Device_Id == 999 && e.Pin == accMonitorLog.Pin).OrderByDescending(e => e.Time).FirstOrDefault();

                if (marcacionEmpl is not null)
                {
                    string tipoMarcacion = EvaluaTipoMarcacion(marcacionEmpl.State);
                    string estadoMarcacion = EvaluaEstadoMarcacion(marcacionEmpl.Description);

                    objResultFinal = new()
                    {
                        //MarcacionId = Guid.Parse(marcacionid),  TEMPORAL SE COMENTA HASTA REGULARIZAR 
                        MarcacionId = marcacionEmpl.Id,
                        TipoMarcacion = tipoMarcacion,
                        EstadoMarcacion = estadoMarcacion
                    };
                }
                else
                {
                    objResultFinal = null;
                }


                return new ResponseType<MarcacionResponseType>() { Data = objResultFinal, Message = "Marcación registrada correctamente", StatusCode = "100", Succeeded = true };

                //if (!objTurno.Any())
                //{
                //    var objResultado = await _repoMonitorLogAsync.AddAsync(accMonitorLog, cancellationToken);
                //    if (objResultado is null)
                //    {
                //        return new ResponseType<MarcacionResponseType>() { Message = "No se ha podido registrar su marcación", StatusCode = "101", Succeeded = true };
                //    }

                //    return new ResponseType<MarcacionResponseType>() { Message = "Su marcación se ingreso correctamente, por el momento no tiene turno asignado por favor comunicar a su superior que le asigne", StatusCode = "100", Succeeded = true };
                //}

                #region COMENTADO KPI 1412
                //Guid? idturnovalidado = Guid.Empty;
                //var tipoMarcacion = string.Empty;
                //var codigoMarcacion = string.Empty;
                //var estadoMarcacion = string.Empty;
                //var countMarcacion = 0;
                //var countMarcacionEntrada = 0;
                //MarcacionColaborador marcacionColaboradorS = new();

                //foreach (var itemTurno in objTurno)
                //{
                //    var turnoEntrada = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.Entrada.TimeOfDay.ToString());
                //    var turnoSalida = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.Salida.TimeOfDay.ToString());

                //    var mEntradaPre = DateTime.Now.AddMinutes(-Convert.ToDouble(itemTurno.Turno.MargenEntradaPrevio));
                //    var mSalidaPos = DateTime.Now.AddMinutes(Convert.ToDouble(itemTurno.Turno.MargenSalidaPosterior));
                //    var mEntradaGra = DateTime.Now.AddDays(Convert.ToDouble(itemTurno.Turno.MargenEntradaGracia));
                //    var mSalidaGra = DateTime.Now.AddDays(-Convert.ToDouble(itemTurno.Turno.MargenSalidaGracia));

                //    //var mEntrada = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenEntradaPrevio.TimeOfDay.ToString());
                //    //var mSalida = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenSalida.TimeOfDay.ToString());
                //    //var margenEPre = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenEntradaPrevio);
                //    //var margenEPos = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenEntradaPosterior);
                //    //var margenSPre = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenSalidaPrevio);
                //    //var margenSPos = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenSalidaPosterior);


                //    if (marcacionColaborador >= mEntradaPre && marcacionColaborador <= mEntradaGra)
                //    {
                //        idturnovalidado = itemTurno.Id;
                //        tipoMarcacion = "E";
                //        codigoMarcacion = "10";
                //        estadoMarcacion = marcacionColaborador < turnoEntrada && marcacionColaborador <= mEntradaGra ? "C" : "AI";
                //        countMarcacion = await _repoMarcacionCola.CountAsync(new MarcacionByMargen(mEntradaPre, mEntradaGra, tipoMarcacion, objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Id), cancellationToken);
                //        break;

                //    }
                //    else if (marcacionColaborador >= mSalidaGra && marcacionColaborador <= mSalidaPos)
                //    {
                //        idturnovalidado = itemTurno.Id;
                //        tipoMarcacion = "S";
                //        codigoMarcacion = "11";
                //        estadoMarcacion = marcacionColaborador > turnoSalida && marcacionColaborador > mSalidaGra ? "C" : "SI";
                //        countMarcacion = await _repoMarcacionCola.CountAsync(new MarcacionByMargen(mSalidaGra, mSalidaPos, tipoMarcacion, objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Id), cancellationToken);
                //        countMarcacionEntrada = await _repoMarcacionCola.CountAsync(new MarcacionByMargen(mEntradaPre, mEntradaGra, tipoMarcacion, objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Id), cancellationToken);
                //        marcacionColaboradorS = await _repoMarcacionCola.FirstOrDefaultAsync(new MarcacionByColaborador(objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Id, marcacionColaborador), cancellationToken);
                //        break;
                //    }
                //}

                //if (idturnovalidado == Guid.Empty && tipoMarcacion == string.Empty && estadoMarcacion == string.Empty) return new ResponseType<MarcacionResponseType>() { Message = "Actualmente no tiene turno de Entrada/Salida", StatusCode = "101", Succeeded = true };

                //#region "Codigo Comentado"
                ////var userInfo = await _repoUserInfoAsync.FirstOrDefaultAsync(new UserMarcacionByCodigoSpec(Request.CodigoEmpleado), cancellationToken);
                ////if (userInfo is null)
                ////{
                ////    UserInfo objUserInfo = new()
                ////    {
                ////        Badgenumber = Request.CodigoEmpleado,
                ////        Ssn = objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Identificacion,
                ////        Name = objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Nombres,
                ////        LastName = objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Apellidos,
                ////        //DefaultDeptId = ObjClient.Cargo.DepartamentoId.ToString(),
                ////        CreateOperator = "Admin",
                ////        CreateTime = DateTime.Now
                ////    };

                ////    userInfo = await _repoUserInfoAsync.AddAsync(objUserInfo, cancellationToken);
                ////}

                ////var countMarcacionCheck = await _repoMonitorLogAsync.CountAsync(new MarcacionByUserIdSpec(objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.CodigoConvivencia), cancellationToken);

                ////CheckInOut entityCheck = new()
                ////{
                ////    UserId = userInfo.UserId,
                ////    CheckTime = DateTime.Now,
                ////    CheckType = countMarcacionCheck == 0 ? "I" : "O",
                ////    Sn = Request.DispositivoId
                ////};


                ////var objResult = await _repoCheckInOutAsync.AddAsync(entityCheck,cancellationToken);

                ////var objMonitorLog =  await _repoMonitorLogAsync.FirstOrDefaultAsync(new MarcacionByMaxIdSpec());

                //#endregion

                //if (countMarcacion >= 1)
                //{
                //    return new ResponseType<MarcacionResponseType>() { Message = "Ya tiene una marcación registrada", StatusCode = "101", Succeeded = true };
                //}
                //accMonitorLog.State = Convert.ToInt32(codigoMarcacion);
                //var objResult = await _repoMonitorLogAsync.AddAsync(accMonitorLog, cancellationToken);

                //if (objResult is not null)
                //{


                //    if (tipoMarcacion == "S")
                //    {
                //        if (countMarcacionEntrada >= 1)
                //        {
                //            marcacionColaboradorS.SalidaEntrada = tipoMarcacion == "S" ? estadoMarcacion : null;
                //            marcacionColaboradorS.UsuarioModificacion = "SYSTEM";
                //            marcacionColaboradorS.FechaModificacion = DateTime.Now;
                //            marcacionColaboradorS.MarcacionSalida = tipoMarcacion == "S" ? marcacionColaborador : null;
                //            await _repoMarcacionCola.UpdateAsync(marcacionColaboradorS);

                //            var marcacionid = marcacionColaboradorS.SalidaEntrada == "SI" ? marcacionColaboradorS.Id.ToString() : null;

                //            MarcacionResponseType objResultFinal = new();
                //            if (marcacionid != null)
                //            {
                //                objResultFinal = new()
                //                {
                //                    MarcacionId = Guid.Parse(marcacionid),
                //                    TipoMarcacion = tipoMarcacion,
                //                    EstadoMarcacion = estadoMarcacion
                //                };
                //            }
                //            else
                //            {
                //                objResultFinal = null;
                //            }

                //            return new ResponseType<MarcacionResponseType>() { Data = objResultFinal, Message = "Marcación de Salida registrada correctamente", StatusCode = "100", Succeeded = true };
                //        }
                //    }

                //    MarcacionColaborador objMarcacionColaborador = new()
                //    {
                //        IdTurnoColaborador = idturnovalidado,
                //        IdLocalidadColaborador = objLocalidad.LocalidadColaboradores.ElementAt(0).Id,
                //        MarcacionEntrada = tipoMarcacion == "E" ? marcacionColaborador : null,
                //        MarcacionSalida = tipoMarcacion == "S" ? marcacionColaborador : null,
                //        EstadoMarcacionEntrada = tipoMarcacion == "E" ? estadoMarcacion : null,
                //        SalidaEntrada = tipoMarcacion == "S" ? estadoMarcacion : null,
                //        EstadoProcesado = false,
                //        UsuarioCreacion = "Admin",
                //        FechaCreacion = DateTime.Now,
                //    };

                //    var objMarcacion = await _repoMarcacionCola.AddAsync(objMarcacionColaborador, cancellationToken);

                //    if (objMarcacion is not null)
                //    {
                //        var tipoMarcaciontexto = tipoMarcacion == "S" ? "Salida" : "Entrada";
                //        var marcacionid = objMarcacion.EstadoMarcacionEntrada == "AI" || objMarcacion.SalidaEntrada == "SI" || objMarcacion.EstadoMarcacionEntrada == null ? objMarcacion.Id.ToString() : null;
                //        MarcacionResponseType objResultFinal = new();
                //        if (marcacionid != null)
                //        {
                //            objResultFinal = new()
                //            {
                //                MarcacionId = Guid.Parse(marcacionid),
                //                TipoMarcacion = tipoMarcacion,
                //                EstadoMarcacion = estadoMarcacion
                //            };
                //        }
                //        else
                //        {
                //            objResultFinal = null;
                //        }


                //        return new ResponseType<MarcacionResponseType>() { Data = objResultFinal, Message = "Marcación de " + tipoMarcaciontexto + " registrada correctamente", StatusCode = "100", Succeeded = true };
                //    }
                //}


                //return new ResponseType<MarcacionResponseType>() { Message = "No se ha registrado la marcación", Succeeded = true, StatusCode = "101" };
                #endregion


            }
            return new ResponseType<MarcacionResponseType>() { Message = "Debe marcar desde su dispositivo movil.", Succeeded = true, StatusCode = "101" };

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
            var marcaMonitor = await _repoMonitorLogAsync.ListAsync(new MarMonitorByRangeFechaSpec(itemCliente.CodigoConvivencia, fechaDesde, fechaHasta), cancellationToken);
            TimeSpan difFechas = fechaHasta - fechaDesde;
            List<Dias> dias = new();
            for (int i = 0; i <= difFechas.Days; i++)
            {
                var fechanueva = DateTime.Parse(fechaDesde.ToString()).AddDays(i);
                var turnoAsig = objTurnoColaborador.Where(x => x.FechaAsignacion.Date == fechanueva.Date && x.Turno.ClaseTurno.CodigoClaseturno == "LABORA").FirstOrDefault();
                var turnoAsigReceso = objTurnoColaborador.Where(x => x.FechaAsignacion.Date == fechanueva.Date && x.Turno.ClaseTurno.CodigoClaseturno == "RECESO").FirstOrDefault();
                var marcacionCliente = marcacionesColaborador.Where(x => x.FechaCreacion.Date == fechanueva.Date && x.EstadoProcesado == true).FirstOrDefault();
                var marcacionMonitor = marcaMonitor.Where(x => x.Time.Date == fechanueva.Date).ToList();
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
                    var mEntrada = marcacionMonitor.Where(x => x.Time.Date == fechanueva.Date && x.State == 10).OrderBy(x => x.Time).FirstOrDefault();
                    var mSalida = marcacionMonitor.Where(x => x.Time.Date == fechanueva.Date && x.State == 11).OrderByDescending(x => x.Time).FirstOrDefault();
                    if (mEntrada != null && mSalida != null)
                    {
                        hTTrabajadas = (mSalida.Time.TimeOfDay - mEntrada.Time.TimeOfDay).TotalHours.ToString();
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

        if (desciption.Contains("EXCEDIDO"))
        {
            estadoMarcacion = "ER";
        }
        else if(desciption.Contains("ATRASO"))
        {
            estadoMarcacion = "AI";
        }
        else if(desciption.Contains("SALIDA"))
        {
            estadoMarcacion = "SI";
        }
        return estadoMarcacion;
    }
}
