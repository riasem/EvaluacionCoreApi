
using Ardalis.Specification;
using AutoMapper;
using Dapper;
using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EnrolApp.Application.Features.Marcacion.Specifications;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.Locacions.Specifications;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionApp;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionOffline;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Specifications;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Marcaciones;
using Luxand;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Globalization;

namespace EvaluacionCore.Persistence.Repository.BitacoraMarcacion;


public class MarcacionService : IMarcacion
{
    private readonly ILogger<MarcacionColaborador> _log;
    private readonly IRepositoryGRiasemAsync<UserInfo> _repoUserInfoAsync;
    private readonly IRepositoryGRiasemAsync<Machines> _repoMachinesAsync;
    private readonly IRepositoryGRiasemAsync<CheckInOut> _repoCheckInOutAsync;
    private readonly IRepositoryGRiasemAsync<AccMonitorLog> _repoMonitorLogAsync;
    private readonly IRepositoryGRiasemAsync<MonitorLogFileOffline> _repoMonitorLogFileAsync;
    private readonly IRepositoryGRiasemAsync<AccMonitoLogRiasem> _repoMonitoLogRiasemAsync;
    private readonly IRepositoryGRiasemAsync<AccLogMarcacionOffline> _repoAccLogMarcacionAsync;

    private readonly IRepositoryGRiasemAsync<AlertasNovedadMarcacion> _repoNovedadMarcacion;
    private readonly IRepositoryAsync<TurnoColaborador> _repositoryTurnoColAsync;
    private readonly IRepositoryAsync<Localidad> _repoLocalidad;
    private readonly IRepositoryAsync<Cliente> _repoCliente;
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoCola;
    private readonly IRepositoryAsync<MarcacionColaborador> _repoMarcacionCola;
    private readonly IRepositoryAsync<LocalidadColaborador> _repoLocalColab;
    private readonly IRepositoryGRiasemAsync<MarcacionOffline> _repoMarcacionOffline;
    
    private readonly IConfiguration _config;
    private readonly IBiometria _repoBiometriaAsync;
    private readonly IMapper _mapper;

    private readonly IRepositoryAsync<CargoEje> _repoEje;
    private readonly IEvaluacion _EvaluacionAsync;

    private readonly string Esquema = null;
    private string ConnectionString_Marc { get; }
    private string NombreStoreProcedure = null;
    private string fotoPerfilDefecto = string.Empty;

    public MarcacionService(IRepositoryGRiasemAsync<UserInfo> repoUserInfoAsync, IRepositoryGRiasemAsync<CheckInOut> repoCheckInOutAsync, IRepositoryAsync<TurnoColaborador> repositoryTurnoCol,
        IRepositoryAsync<Localidad> repoLocalidad, ILogger<MarcacionColaborador> log, IEvaluacion repository,
        IRepositoryAsync<TurnoColaborador> repoTurnoCola, IConfiguration config, IRepositoryAsync<CargoEje> repoEje,
        IRepositoryAsync<MarcacionColaborador> repoMarcacionCola, IRepositoryGRiasemAsync<AccMonitorLog> repoMonitorLogAsync,
        IRepositoryAsync<Cliente> repoCliente, IRepositoryAsync<LocalidadColaborador> repoLocalColab,
        IRepositoryGRiasemAsync<AccMonitoLogRiasem> repoMonitoLogRiasemAsync, IRepositoryGRiasemAsync<AlertasNovedadMarcacion> repoNovedadMarcacion, IBiometria repoBiometriaAsync, IRepositoryGRiasemAsync<Machines> repoMachinesAsync,IMapper mapper, IRepositoryGRiasemAsync<MonitorLogFileOffline> repoMonitorLogFileAsync,
        IRepositoryGRiasemAsync<MarcacionOffline> repoMarcacionOffline, IRepositoryGRiasemAsync<AccLogMarcacionOffline> repoAccLogMarcacionAsync)
    {
        _EvaluacionAsync = repository;

        _repoNovedadMarcacion = repoNovedadMarcacion;
        _repoUserInfoAsync = repoUserInfoAsync;
        _repoCheckInOutAsync = repoCheckInOutAsync;
        _config = config;
        _repoLocalidad = repoLocalidad;
        _log = log;
        _repoAccLogMarcacionAsync = repoAccLogMarcacionAsync;
        _repoMarcacionOffline = repoMarcacionOffline;
        _repoMarcacionCola = repoMarcacionCola;
        ConnectionString_Marc = _config.GetConnectionString("Bd_Marcaciones_GRIAMSE");
        Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
        _repoTurnoCola = repoTurnoCola;
        _repoMonitorLogAsync = repoMonitorLogAsync;
        _repoCliente = repoCliente;
        _repoMonitoLogRiasemAsync = repoMonitoLogRiasemAsync;
        NombreStoreProcedure = _config.GetSection("StoredProcedure:Marcacion:ReprocesaMarcaciones").Get<string>();
        _repoLocalColab = repoLocalColab;
        fotoPerfilDefecto = _config.GetSection("Imagenes:FotoPerfilDefecto").Get<string>();
        _repoBiometriaAsync = repoBiometriaAsync;
        _repositoryTurnoColAsync = repositoryTurnoCol;
        _repoEje = repoEje;
        _repoMachinesAsync = repoMachinesAsync;
        _mapper = mapper;
        _repoMonitorLogFileAsync = repoMonitorLogFileAsync;
    }
    public async Task<ResponseType<MarcacionResponseType>> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken)
    {
        try
        {
            var marcacionColaborador = DateTime.Now;
            MarcacionResponseType objResultFinal = new();

            //var objLocalidad = await _repoLocalidad.FirstOrDefaultAsync(new GetLocalidadByIdSpec(Request.LocalidadId, Request.CodigoEmpleado), cancellationToken);

            //if (objLocalidad == null) return new ResponseType<MarcacionResponseType>() { Message = "Localidad incorrecta", Succeeded = true, StatusCode = "101" };

            //Validación de turno que corresponde
            //var objTurno = await _repoTurnoCola.ListAsync(new TurnosByIdClienteSpec(objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Id), cancellationToken);

            //if (Request.DispositivoId == objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.DispositivoId)
            //{
            var deviceId = 999;
            var deviceName = "EnrolApp";

            var objUserSesion = await _repoEje.FirstOrDefaultAsync(new GetEjeByIdentificacionSpec(Request.IdentificacionSesion));
            if (objUserSesion != null)
            {
                var objMachines = await _repoMachinesAsync.GetByIdAsync(objUserSesion.DeviceId);
                deviceId = objMachines.ID;
                deviceName = objMachines.MachineAlias;
            }


            AccMonitorLog accMonitorLog = new()
            {
                State = 0,
                Time = marcacionColaborador,
                Pin = Request.CodigoEmpleado, /*objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador?.CodigoConvivencia,*/
                Device_Id = deviceId, //parametrizar desde el request
                Verified = 0,
                Device_Name = deviceName, //parametrizar desde el request
                Status = 1,
                Create_Time = DateTime.Now
            };

            var objResultado = await _repoMonitorLogAsync.AddAsync(accMonitorLog, cancellationToken);
            if (objResultado is null)
            {
                return new ResponseType<MarcacionResponseType>() { Message = "No se ha podido registrar su marcación", StatusCode = "101", Succeeded = true };
            }
            //await Task.Delay(1500, cancellationToken);
            var marcacionEmpl = await _repoMonitoLogRiasemAsync.FirstOrDefaultAsync(new MarcacionByColaboradorAndTime(objResultado.Pin, objResultado.Time, deviceId), cancellationToken);

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

            //var colaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByCodBiometrico(Request.CodigoEmpleado), cancellationToken);
            //DateTime fechaDesde = marcacionColaborador.Date;



            //string query = "EXEC [dbo].[EAPP_SP_REPROCESA_MARCACIONES] NULL, NULL, NULL, '" + fechaDesde.ToString("yyyy/MM/dd HH:mm:ss") + "' , '" + marcacionColaborador.ToString("yyyy/MM/dd HH:mm:ss") + "',  '" + colaborador.Identificacion + "';";
            //using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            //if (con.State == ConnectionState.Closed) con.Open();
            //con.Query(query);
            //if (con.State == ConnectionState.Open) con.Close();

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


    public async Task<ResponseType<CreateMarcacionResponseType>> CreateMarcacionApp(CreateMarcacionAppRequest Request, string IdentificacionSesion, CancellationToken cancellationToken)
    {
        var objLocalidadColaborador = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(Request.Identificacion));
        if (!objLocalidadColaborador.Any()) return new ResponseType<CreateMarcacionResponseType>() { Message = "No tiene Localidad Asignada", StatusCode = "101", Succeeded = true };
        var localidadSesion = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(IdentificacionSesion));
        //if (!objLocalidadColaborador.Any()) return new ResponseType<CreateMarcacionResponseType>() { Message = "No existe el colaborador", StatusCode = "101", Succeeded = true };
        if (objLocalidadColaborador.ElementAt(0).Colaborador.FacialPersonId is null) return new ResponseType<CreateMarcacionResponseType>() { Message = "Debes registrar tus datos biométricos", StatusCode = "101", Succeeded = true };

        #region Validacion de localidades
        var resultLocalidad = objLocalidadColaborador.Any(x => localidadSesion.Any(ls => ls.IdLocalidad == x.IdLocalidad));
        if (!resultLocalidad) return new ResponseType<CreateMarcacionResponseType>() { Message = "No puede registrar marcación en esta localidad", StatusCode = "101", Succeeded = true };

        #endregion

        AuthenticationFacialRequest requestFacial = new()
        {
            Base64 = Request.Base64,
            Extension = Request.Extension,
            FacialPersonUid = objLocalidadColaborador.ElementAt(0).Colaborador.FacialPersonId.ToString(),
            Nombre = Request.Nombre,
            Identificacion = Request.Identificacion
        };
        ResponseType<string> resultBiometria = await _repoBiometriaAsync.AuthenticationFacialAsync(requestFacial, IdentificacionSesion);

        if (resultBiometria.StatusCode != "100") return new ResponseType<CreateMarcacionResponseType>() { Message = resultBiometria.Message, StatusCode = resultBiometria.StatusCode, Succeeded = resultBiometria.Succeeded };



        CreateMarcacionRequest requestMarcacion = new()
        {
            CodigoEmpleado = objLocalidadColaborador.ElementAt(0).Colaborador.CodigoConvivencia,
            DispositivoId = Request.DispositivoId,
            LocalidadId = Request.LocalidadId,
            IdentificacionSesion = IdentificacionSesion
        };


        var resultMarcacion = await CreateMarcacion(requestMarcacion, cancellationToken);
        CreateMarcacionResponseType data = new();
        data.Colaborador = objLocalidadColaborador.ElementAt(0).Colaborador.Nombres + " " + objLocalidadColaborador.ElementAt(0).Colaborador.Apellidos;
        data.RutaImagen = objLocalidadColaborador.ElementAt(0).Colaborador.ImagenPerfilId is null ? "" : objLocalidadColaborador.ElementAt(0).Colaborador.ImagenPerfil.RutaAcceso;
        data.Marcacion = DateTime.Now;

        return new ResponseType<CreateMarcacionResponseType>() { Message = resultMarcacion.Message ,StatusCode = resultMarcacion.StatusCode, Data = data,  Succeeded = resultMarcacion.Succeeded };


    }

    public async Task<ResponseType<CreateMarcacionResponseType>> CreateMarcacionAppLast(CreateMarcacionAppLastRequest Request, string IdentificacionSesion, CancellationToken cancellationToken)
    {
        var objLocalidadColaborador = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(Request.Identificacion));
        if (!objLocalidadColaborador.Any()) return new ResponseType<CreateMarcacionResponseType>() { Message = "No tiene Localidad Asignada", StatusCode = "101", Succeeded = true };
        var localidadSesion = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(IdentificacionSesion));
        //if (!objLocalidadColaborador.Any()) return new ResponseType<CreateMarcacionResponseType>() { Message = "No existe el colaborador", StatusCode = "101", Succeeded = true };
        if (objLocalidadColaborador.ElementAt(0).Colaborador.FacialPersonId is null) return new ResponseType<CreateMarcacionResponseType>() { Message = "Debes registrar tus datos biométricos", StatusCode = "101", Succeeded = true };

        #region Validacion de localidades
        var resultLocalidad = objLocalidadColaborador.Any(x => localidadSesion.Any(ls => ls.IdLocalidad == x.IdLocalidad));
        if (!resultLocalidad) return new ResponseType<CreateMarcacionResponseType>() { Message = "No puede registrar marcación en esta localidad", StatusCode = "101", Succeeded = true };

        #endregion

        AuthenticationFacialLastRequest requestFacial = new()
        {

            FacialPersonUid = objLocalidadColaborador.ElementAt(0).Colaborador.FacialPersonId.ToString(),
            AdjuntoFiles = Request.Adjunto,
            Identificacion = Request.Identificacion

        };
        ResponseType<string> resultBiometria = await _repoBiometriaAsync.AuthenticationFacialLastAsync(requestFacial);

        if (resultBiometria.StatusCode != "100") return new ResponseType<CreateMarcacionResponseType>() { Message = resultBiometria.Message, StatusCode = resultBiometria.StatusCode, Succeeded = resultBiometria.Succeeded };



        CreateMarcacionRequest requestMarcacion = new()
        {
            CodigoEmpleado = objLocalidadColaborador.ElementAt(0).Colaborador.CodigoConvivencia,
            DispositivoId = Request.DispositivoId,
            //LocalidadId = Request.LocalidadId,
            IdentificacionSesion = IdentificacionSesion
        };


        var resultMarcacion = await CreateMarcacion(requestMarcacion, cancellationToken);
        CreateMarcacionResponseType data = new();
        data.Colaborador = objLocalidadColaborador.ElementAt(0).Colaborador.Nombres + " " + objLocalidadColaborador.ElementAt(0).Colaborador.Apellidos;
        data.RutaImagen = objLocalidadColaborador.ElementAt(0).Colaborador.ImagenPerfilId is null ? "" : objLocalidadColaborador.ElementAt(0).Colaborador.ImagenPerfil.RutaAcceso;
        data.Marcacion = DateTime.Now;

        return new ResponseType<CreateMarcacionResponseType>() { Message = resultMarcacion.Message, StatusCode = resultMarcacion.StatusCode, Data = data, Succeeded = resultMarcacion.Succeeded };
    }

    public async Task<ResponseType<List<ConsultaRecursoType>>> ConsultarRecursos(Guid Identificacion, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken)
    {
        var objClienteCargo = await _repoCliente.ListAsync(new ClientePadreById(Identificacion), cancellationToken);

        if (objClienteCargo == null) return new ResponseType<List<ConsultaRecursoType>>() { Message = "No tiene personal a cargo", StatusCode = "001", Succeeded = true };

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
                    }
                    else /*if (marcacionCliente.MarcacionEntrada != null && marcacionCliente.MarcacionSalida == null)*/
                    {
                        tHTrabajadas = "0";
                    }

                    Dias newDias = new()
                    {
                        Fecha = fechanueva,
                        HorasAsignadas = Math.Round(Convert.ToDouble(tHAsignada), 2).ToString(),
                        HorasPendiente = marcacionCliente.TotalAtraso == null ? TimeSpan.Zero.TotalHours.ToString() : Math.Round(marcacionCliente.TotalAtraso.Value.TimeOfDay.TotalHours, 2).ToString(),
                        HorasTrabajada = Math.Round(Convert.ToDouble(tHTrabajadas), 2) >= 8 ? "8" : Math.Round(Convert.ToDouble(tHTrabajadas), 2).ToString(),
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
                    HorasTrabajada = Math.Round(Convert.ToDouble(hTTrabajadas), 2) >= 8 ? "8" : Math.Round(Convert.ToDouble(hTTrabajadas), 2).ToString()

                };

                dias.Add(newDiasSturno);

                continue;

            }

            listRecursos.Add(new ConsultaRecursoType()
            {
                Colaborador = itemCliente.Nombres + " " + itemCliente.Apellidos,
                Identificacion = itemCliente.Identificacion,
                HTotalAsignadas = Math.Round(dias.Sum(x => Convert.ToDouble(x.HorasAsignadas)), 2).ToString(),
                HTotalPendiente = Math.Round(dias.Sum(x => Convert.ToDouble(x.HorasPendiente)), 2).ToString(),
                HTotalTrabajadas = Math.Round(dias.Sum(x => Convert.ToDouble(x.HorasTrabajada)), 2).ToString(),
                Dias = dias
            });

        }
        return await Task.FromResult(new ResponseType<List<ConsultaRecursoType>>() { Data = listRecursos, Message = "Consulta Correcta", StatusCode = "000", Succeeded = true });
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
        if (desciption.Contains("ATRASO DE"))
        {
            estadoMarcacion = "AI";
        }
        if (desciption.Contains("SALIDA ANTICIPADA"))
        {
            estadoMarcacion = "SI";
        }
        return estadoMarcacion;
    }

    public async Task<ResponseType<MarcacionWebResponseType>> CreateMarcacionWeb(CreateMarcacionWebRequest Request, CancellationToken cancellationToken)
    {
        try
        {
            string tipoMarcacion = string.IsNullOrEmpty(Request.TipoMarcacion) ? string.Empty : Request.TipoMarcacion.ToUpper();
            int deviceId = 998;
            string deviceName = "ENROLAPP WEB";

            var jefe = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(Request.IdentificacionJefe), cancellationToken);

            if (jefe == null)
                return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "No se pudo consultar localidad principal", StatusCode = "101", Succeeded = false };

            var infoBiometrico = await _repoEje.FirstOrDefaultAsync(new GetEjeByIdentificacionSpec(jefe.Identificacion), cancellationToken);

            if (infoBiometrico != null)
            {
                if(infoBiometrico.DeviceId != null)
                {
                    deviceId = (int)infoBiometrico.DeviceId;
                    deviceName = infoBiometrico.DeviceName;
                }
            }

            var colaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(Request.IdentificacionColaborador), cancellationToken);

            if (colaborador == null)
                return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "No se pudo encontrar información del colaborador", StatusCode = "101", Succeeded = false };

            var jefeLocalidad = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(Request.IdentificacionJefe), cancellationToken);

            if (!jefeLocalidad.Any())
                return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "No se pudo encontrar localidad principal asignada", StatusCode = "101", Succeeded = false };

            var colaboradorLocalidad = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(Request.IdentificacionColaborador), cancellationToken);

            if (!colaboradorLocalidad.Any())
                return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "Colaborador no tiene localidades asignadas", StatusCode = "101", Succeeded = false };

            //var localidades = jefeLocalidad.Where(jl => colaboradorLocalidad.Any(cl => cl.IdLocalidad == jl.IdLocalidad)).ToList();

            //if (!localidades.Any())
            //    return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "Colaborador no corresponde a la localidad", StatusCode = "101", Succeeded = false };

            if (tipoMarcacion == "F")
            {
                if (colaborador.FacialPersonId == null)
                    return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "Colaborador no tiene registro facial", StatusCode = "101", Succeeded = false };

                AuthenticationFacialRequest objAuth = new()
                {
                    Base64 = Request.Base64Archivo,
                    Nombre = Request.NombreArchivo,
                    Extension = Request.ExtensionArchivo,
                    FacialPersonUid = colaborador.FacialPersonId.ToString(),
                };

                var respAuth = await _repoBiometriaAsync.AuthenticationFacialAsync(objAuth, Request.IdentificacionJefe);

                if (!respAuth.Succeeded)
                    return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = respAuth.Message, StatusCode = "101", Succeeded = false };
            }
            else
            {
                if (colaborador.CodigoConvivencia != Request.PinColaborador)
                    return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "Credenciales incorrectas", StatusCode = "101", Succeeded = false };
            }

            #region Registro de la marcación 
            var marcacionColaborador = DateTime.Now;

            AccMonitorLog accMonitorLog = new()
            {
                State = 0,
                Time = marcacionColaborador,
                Pin = string.IsNullOrEmpty(colaborador.CodigoConvivencia) ? string.Empty : colaborador.CodigoConvivencia,
                Device_Id = deviceId, //Device ID para en EnrolApp Web
                Verified = 0,
                Device_Name = deviceName,
                Status = 1,
                Create_Time = DateTime.Now
            };

            var objResultado = await _repoMonitorLogAsync.AddAsync(accMonitorLog, cancellationToken);

            if (objResultado == null)
                return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "No se ha podido registrar su marcación", StatusCode = "101", Succeeded = false };

            DateTime fechaDesde = marcacionColaborador.Date;

            string query = "EXEC [dbo].[EAPP_SP_REPROCESA_MARCACIONES] NULL, NULL, NULL, '" + fechaDesde.ToString("yyyy/MM/dd HH:mm:ss") + "' , '" + marcacionColaborador.ToString("yyyy/MM/dd HH:mm:ss") + "',  '" + colaborador.Identificacion + "';";
            using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            if (con.State == ConnectionState.Closed) con.Open();
            con.Query(query);
            if (con.State == ConnectionState.Open) con.Close();
            #endregion

            var reponse = new MarcacionWebResponseType()
            {
                Colaborador = string.Concat(colaborador.Apellidos, " ", colaborador.Nombres),
                Identificacion = colaborador.Identificacion,
                Mensaje = string.Concat("Has registrado exitosamente una marcación el ", fechaDesde.ToString("dd/MM/yyyy"), " a las ", marcacionColaborador.ToString("HH:mm:ss"), "."),
                FotoPerfil = colaborador.ImagenPerfil is not null ? colaborador.ImagenPerfil.RutaAcceso : fotoPerfilDefecto
            };

            return new ResponseType<MarcacionWebResponseType>() { Data = reponse, Message = CodeMessageResponse.GetMessageByCode("100"), StatusCode = "100", Succeeded = true };
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
        }
    }

    public async Task<ResponseType<List<NovedadMarcacionType>>> ConsultaNovedadMarcacion(string Identificacion, string FiltroNovedades, DateTime FDesde, DateTime FHasta, CancellationToken cancellationToken)
    {
        try
        {
            FHasta = FHasta.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<NovedadMarcacionType> ListaNovedadMarcacion = new();

            string[] Identificaciones = Identificacion.Split(",");
            string[] Novedades = FiltroNovedades.Split(",");

            foreach (var col in Identificaciones)
            {
                var objCliente = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(col), cancellationToken);

                if (objCliente is null) continue;

                var novedadesMarcacion = await _repoNovedadMarcacion.ListAsync(new NovedadMarcacionByColaboradorSpec(int.Parse(objCliente.CodigoConvivencia), Novedades, FDesde, FHasta), cancellationToken);

                //var novedadesMarcacion = await _repoNovedadMarcacion.ListAsync(cancellationToken);


                foreach (var item in novedadesMarcacion)
                {
                    DateTime fechaTurno = new(item.FechaMarcacion.Year, item.FechaMarcacion.Month, item.FechaMarcacion.Day, 0, 0, 0);
                    var objColaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByCodBiometrico(item.UsuarioMarcacion.ToString()), cancellationToken);

                    var objTurnoCol = await _repoTurnoCola.FirstOrDefaultAsync(new TurnoLabColaboradorByIdentificacionSpec(objColaborador.Identificacion, fechaTurno, fechaTurno), cancellationToken);

                    if (objTurnoCol is null) continue;

                    DateTime entrada = fechaTurno.AddHours(objTurnoCol.Turno.Entrada.Hour).AddMinutes(objTurnoCol.Turno.Entrada.Minute);
                    DateTime salida = fechaTurno.AddHours(objTurnoCol.Turno.Salida.Hour).AddMinutes(objTurnoCol.Turno.Salida.Minute);

                    ListaNovedadMarcacion.Add(new NovedadMarcacionType()
                    {
                        IdMarcacion = item.IdMarcacion,
                        FechaMarcacion = item.FechaMarcacion,
                        TipoNovedad = item.TipoNovedad,
                        DescripcionMensaje = item.DescripcionMensaje,
                        Canal = item.Canal,
                        Dispositivo = item.Dispositivo,
                        Colaborador = objColaborador.Nombres + ' ' + objColaborador.Apellidos,
                        TurnoEntrada = entrada,
                        TurnoSalida = salida
                    });
                }
            }
           

            return new ResponseType<List<NovedadMarcacionType>>()
            {
                Data = ListaNovedadMarcacion,
                Message = "Consulta generada exitosamente.",
                StatusCode = "000",
                Succeeded = true
            };
        }
        catch (Exception e)
        {
            return new ResponseType<List<NovedadMarcacionType>>() { Message = "Ocurrió un error", StatusCode = "001", Succeeded = false };

        }
    }

    public async Task<ResponseType<List<NovedadMarcacionWebType>>> ConsultaNovedadMarcacionWeb(string Identificacion, string FiltroNovedades, DateTime FDesde, DateTime FHasta, CancellationToken cancellationToken)
    {
        try
        {
            FHasta = FHasta.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<NovedadMarcacionWebType> listaEvaluacionAsistencia = new();
            TurnoColaborador turnoRecesoFiltro = new();
            string[] Identificaciones = Identificacion.Split(",");
            bool poseeTurnoReceso = false;
            //var listaColaboradores = await _EvaluacionAsync.ConsultaColaboradores(request.Udn, request.Area, request.Departamento, request.Suscriptor);

            string[] filtroNovedades = FiltroNovedades.Split(",");


            foreach (var col in Identificaciones)
            {

                var itemCol = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(col), cancellationToken);

                if (itemCol is null) continue;
                
                for (DateTime dtm = FDesde; dtm <= FHasta; dtm = dtm.AddDays(1))
                {
                    List<Novedad> novedades = new();
                    //Se obtiene el turno laboral asignado al colaborador de la fecha en proceso
                    var turnoFiltro = await _repositoryTurnoColAsync.FirstOrDefaultAsync(new TurnoColaboradorTreeSpec(itemCol.Identificacion, dtm), cancellationToken);


                    #region consulta y procesamiento de turno laboral

                    DateTime turnoLabDesde = dtm;
                    DateTime turnoLabHasta = dtm.AddHours(23).AddMinutes(59);

                    if (turnoFiltro != null)
                    {
                        turnoLabDesde = dtm.AddHours(turnoFiltro?.Turno?.Entrada.Hour ?? 0).AddMinutes(turnoFiltro?.Turno?.Entrada.Minute ?? 0);
                        turnoLabHasta = dtm.AddHours(turnoFiltro?.Turno?.Salida.Hour ?? 0).AddMinutes(turnoFiltro?.Turno?.Salida.Minute ?? 0);
                        turnoRecesoFiltro = await _repositoryTurnoColAsync.FirstOrDefaultAsync(new TurnoRecesoColaboradorTreeSpec(itemCol.Identificacion, dtm, turnoFiltro?.Turno.Id), cancellationToken);
                        if (turnoRecesoFiltro != null) poseeTurnoReceso = true;
                    }
                    if (turnoLabDesde > turnoLabHasta) turnoLabHasta.AddDays(1);

                    //SE OBTIENE MARCACIONES DE LA FECHA EN PROCESO

                    string codMarcacionEntrada = (turnoFiltro?.Turno?.CodigoEntrada?.ToString()) ?? "10";
                    string codMarcacionSalida = (turnoFiltro?.Turno?.CodigoSalida?.ToString()) ?? "11";


                    List<BitacoraMarcacionType> objMarcacionColEntrada_ = await _EvaluacionAsync.ConsultaMarcaciones(itemCol.Identificacion, dtm, dtm.AddHours(23).AddMinutes(59), codMarcacionEntrada);
                    BitacoraMarcacionType objMarcacionColEntrada = objMarcacionColEntrada_.FirstOrDefault();
                    List<BitacoraMarcacionType> objMarcacionColSalida_ = new();
                    BitacoraMarcacionType objMarcacionColSalida;

                    if (objMarcacionColEntrada != null)
                    {
                        string timeString = objMarcacionColEntrada?.Time;
                        DateTime date3 = DateTime.ParseExact(timeString, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        objMarcacionColSalida_ = await _EvaluacionAsync.ConsultaMarcaciones(itemCol.Identificacion, date3, turnoLabDesde.AddDays(1), codMarcacionSalida);
                    }

                    objMarcacionColSalida = objMarcacionColSalida_.FirstOrDefault();


                    string fEntrada = objMarcacionColEntrada?.Time.ToString();
                    DateTime? fechaEntrada = !string.IsNullOrEmpty(fEntrada) ? Convert.ToDateTime(fEntrada, CultureInfo.InvariantCulture) : null;


                    string fSalida = objMarcacionColSalida?.Time.ToString();
                    DateTime? fechaSalida = !string.IsNullOrEmpty(fSalida) ? Convert.ToDateTime(fSalida, CultureInfo.InvariantCulture) : null;

                    //SE PREPARA LA INFORMACION DE RETORNO
                    TurnoLaboral turnoLaborall = new()
                    {
                        //turno
                        Id = turnoFiltro?.Id,
                        Entrada = turnoFiltro?.Turno?.Entrada ?? null,
                        Salida = turnoFiltro?.Turno?.Salida ?? null,
                        TotalHoras = (turnoFiltro?.Turno?.TotalHoras) ?? "0",

                        MarcacionEntrada = fechaEntrada,
                        EstadoEntrada = objMarcacionColEntrada?.EstadoMarcacion ?? "",
                        FechaSolicitudEntrada = objMarcacionColEntrada?.FechaSolicitud != null ? DateTime.ParseExact(objMarcacionColEntrada.FechaSolicitud, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse("01-01-1900"),
                        UsuarioSolicitudEntrada = objMarcacionColEntrada?.UsuarioSolicitud ?? "0",
                        IdSolicitudEntrada = objMarcacionColEntrada?.IdSolicitud ?? Guid.Empty,
                        IdFeatureEntrada = objMarcacionColEntrada?.IdFeature ?? Guid.Empty,
                        TipoSolicitudEntrada = EvaluaTipoSolicitud(objMarcacionColEntrada?.IdFeature),

                        MarcacionSalida = fechaSalida,
                        EstadoSalida = objMarcacionColSalida?.EstadoMarcacion ?? "",
                        FechaSolicitudSalida = objMarcacionColSalida?.FechaSolicitud != null ? DateTime.ParseExact(objMarcacionColSalida.FechaSolicitud, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse("01-01-1900"),
                        UsuarioSolicitudSalida = objMarcacionColSalida?.UsuarioSolicitud ?? "0",
                        IdSolicitudSalida = objMarcacionColSalida?.IdSolicitud ?? Guid.Empty,
                        IdFeatureSalida = objMarcacionColSalida?.IdFeature ?? Guid.Empty,
                        TipoSolicitudSalida = EvaluaTipoSolicitud(objMarcacionColSalida?.IdFeature)

                    };
                    

                    #endregion


                    #region consulta y procesamiento de turno de receso


                    DateTime turnoRecesoDesde = turnoLaborall.MarcacionEntrada ?? dtm;
                    DateTime turnoRecesoHasta = turnoLaborall.MarcacionSalida ?? dtm.AddHours(23).AddMinutes(59);

                    string codMarcacionEntradaReceso = (turnoRecesoFiltro?.Turno?.CodigoEntrada?.ToString()) ?? "14";
                    string codMarcacionSalidaReceso = (turnoRecesoFiltro?.Turno?.CodigoSalida?.ToString()) ?? "15";

                    List<BitacoraMarcacionType> marcacionEntradaRecesoFiltro_ = await _EvaluacionAsync.ConsultaMarcaciones(itemCol.Identificacion, turnoRecesoDesde, turnoRecesoHasta, codMarcacionEntradaReceso);
                    BitacoraMarcacionType marcacionEntradaRecesoFiltro = marcacionEntradaRecesoFiltro_.FirstOrDefault();
                    List<BitacoraMarcacionType> marcacionSalidaRecesoFiltro_ = new();
                    BitacoraMarcacionType marcacionSalidaRecesoFiltro;
                    if (marcacionEntradaRecesoFiltro != null)
                    {
                        string timeString = marcacionEntradaRecesoFiltro?.Time;
                        DateTime date2 = DateTime.ParseExact(timeString, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        marcacionSalidaRecesoFiltro_ = await _EvaluacionAsync.ConsultaMarcaciones(itemCol.Identificacion, date2, turnoLabDesde.AddDays(1), codMarcacionSalidaReceso);
                    }
                    marcacionSalidaRecesoFiltro = marcacionSalidaRecesoFiltro_?.FirstOrDefault();

                    TurnoReceso turnoReceso = new()
                    {
                        //turno de receso asignado
                        Id = turnoRecesoFiltro?.Id ?? null,
                        Entrada = turnoRecesoFiltro?.Turno?.Entrada ?? null,
                        Salida = turnoRecesoFiltro?.Turno?.Salida ?? null,
                        TotalHoras = (turnoRecesoFiltro?.Turno?.TotalHoras) ?? "0",

                        //marcaciones de receso entrada
                        MarcacionEntrada = marcacionEntradaRecesoFiltro?.Time != null ? DateTime.ParseExact(marcacionEntradaRecesoFiltro.Time, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : null,
                        FechaSolicitudEntradaReceso = marcacionEntradaRecesoFiltro?.FechaSolicitud != null ? DateTime.ParseExact(marcacionEntradaRecesoFiltro.FechaSolicitud, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse("01-01-1900"),
                        UsuarioSolicitudEntradaReceso = marcacionEntradaRecesoFiltro?.UsuarioSolicitud ?? "0",
                        IdSolicitudEntradaReceso = marcacionEntradaRecesoFiltro?.IdSolicitud ?? Guid.Empty,
                        EstadoEntradaReceso = marcacionEntradaRecesoFiltro?.EstadoMarcacion ?? "",
                        IdFeatureEntradaReceso = marcacionEntradaRecesoFiltro?.IdFeature ?? Guid.Empty,
                        TipoSolicitudEntradaReceso = EvaluaTipoSolicitud(marcacionEntradaRecesoFiltro?.IdFeature),

                        MarcacionSalida = marcacionSalidaRecesoFiltro?.Time != null ? DateTime.ParseExact(marcacionSalidaRecesoFiltro.Time, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : null,
                        FechaSolicitudSalidaReceso = marcacionSalidaRecesoFiltro?.FechaSolicitud != null ? DateTime.ParseExact(marcacionSalidaRecesoFiltro.FechaSolicitud, @"MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse("01-01-1900"),
                        UsuarioSolicitudSalidaReceso = marcacionSalidaRecesoFiltro?.UsuarioSolicitud ?? "",
                        IdSolicitudSalidaReceso = marcacionSalidaRecesoFiltro?.IdSolicitud ?? Guid.Empty,
                        EstadoSalidaReceso = marcacionSalidaRecesoFiltro?.EstadoMarcacion ?? "",
                        IdFeatureSalidaReceso = marcacionSalidaRecesoFiltro?.IdFeature ?? Guid.Empty,
                        TipoSolicitudSalidaReceso = EvaluaTipoSolicitud(marcacionSalidaRecesoFiltro?.IdFeature)

                    };

                    #endregion


                    #region Consulta y procesamiento de novedades
                    DateTime novedadDesde = dtm;
                    DateTime novedadHasta = dtm.AddHours(23).AddMinutes(59).AddSeconds(59);


                    var novedadesMarcacion = await _repoNovedadMarcacion.ListAsync(new NovedadMarcacionByColaboradorSpec(int.Parse(itemCol.CodigoConvivencia), filtroNovedades, novedadDesde, novedadHasta), cancellationToken);

                    foreach (var item in novedadesMarcacion)
                    {
                        if (filtroNovedades.Contains(item.TipoNovedad))
                        {
                            novedades.Add(new Novedad
                            {
                                Descripcion = item.DescripcionMensaje,
                                MinutosNovedad = "",
                                EstadoMarcacion = item.TipoNovedad,
                                FechaAprobacion = item.FechaMarcacion
                            });

                        }
                    }

                    #endregion

                    var colaborador = await _EvaluacionAsync.ConsultaColaborador(itemCol.Identificacion);

                    if (novedades.Count == 0) continue;

                    listaEvaluacionAsistencia.Add(new NovedadMarcacionWebType()
                    {
                        Colaborador = itemCol.Nombres + " " + itemCol.Apellidos,
                        Identificacion = itemCol.Identificacion,
                        CodBiometrico = itemCol.CodigoConvivencia,
                        Udn = colaborador[0].DesUdn,
                        Area = colaborador[0].DesArea,
                        SubCentroCosto = colaborador[0].DesSubcentroCosto,
                        Fecha = dtm,
                        Novedades = novedades,
                        TurnoLaboral = turnoLaborall,
                        TurnoReceso = turnoReceso
                    });

                }

            }

            //var lista = listaEvaluacionAsistencia.Where(e => filtroNovedades.Contains(e.TurnoLaboral.EstadoEntrada) || filtroNovedades.Contains(e.TurnoLaboral.EstadoSalida) ||
            //                                     filtroNovedades.Contains(e.TurnoReceso.EstadoEntradaReceso) || filtroNovedades.Contains(e.TurnoReceso.EstadoSalidaReceso) ||
            //                                     filtroNovedades.Contains(e.Novedades.Select(e => e.EstadoMarcacion).ToString())).ToList();

            return new ResponseType<List<NovedadMarcacionWebType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
            //return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };

        }
        catch (Exception e)
        {
            return new ResponseType<List<NovedadMarcacionWebType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }
    }


    public async Task<ResponseType<List<ColaboradorByLocalidadResponseType>>> ListadoColaboradorByLocalidad(string IdentificacionSesion, CancellationToken cancellationToken)
    {
        try
        {
            var colaboradores = await _repoLocalColab.ListAsync(new GetListadoPersonalByLocalidadSpec(IdentificacionSesion), cancellationToken);

            if (!colaboradores.Any()) return new ResponseType<List<ColaboradorByLocalidadResponseType>>() { Message = "No existen Colaboradores en la localidad", StatusCode = "001", Succeeded = true };
            colaboradores = colaboradores.DistinctBy(x => x.Colaborador.Identificacion).ToList();

            var result = _mapper.Map<List<ColaboradorByLocalidadResponseType>>(colaboradores);

            return new ResponseType<List<ColaboradorByLocalidadResponseType>>() { Data = result.OrderBy(x => x.Identificacion).ToList(), Message = CodeMessageResponse.GetMessageByCode("000"),Succeeded = true, StatusCode = "000"};
        }
        catch (Exception ex)
        {

            _log.LogError(ex, string.Empty);

            return new ResponseType<List<ColaboradorByLocalidadResponseType>>() { Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002", Succeeded = false};
        }

    }


    public async Task<ResponseType<string>> CreateMarcacionOffline(CreateMarcacionOfflineRequest Request,string IdentificacionSesion, CancellationToken cancellationToken)
    {
        try
        {
            MarcacionResponseType objResultFinal = new();

            var deviceId = 999;
            var deviceName = "EnrolApp";

            var objUserSesion = await _repoEje.FirstOrDefaultAsync(new GetEjeByIdentificacionSpec(IdentificacionSesion));
            if (objUserSesion != null)
            {
                var objMachines = await _repoMachinesAsync.GetByIdAsync(objUserSesion.DeviceId);
                deviceId = objMachines.ID;
                deviceName = objMachines.MachineAlias;
            }

            #region Actualizar Sincronizadas

            var objUpdateCabecera = await _repoAccLogMarcacionAsync.GetByIdAsync(Request.IdCabecera);
            if (objUpdateCabecera is null)
            {
                return new ResponseType<string> { Message = "No se ha actualizado la cabecera", StatusCode = "101", Succeeded = true };
            }
            objUpdateCabecera.TotalSincronizadas = Request.CantidadSincronizada;
            objUpdateCabecera.UsuarioModificacion = IdentificacionSesion;
            objUpdateCabecera.FechaModificacion = DateTime.Now;
            if (Request.CantidadSincronizada == 1)
            {
                objUpdateCabecera.Estado = "IS";//Inicio de Sincronización
            }
            else if (objUpdateCabecera.TotalSincronizadas > 0)
            {
                objUpdateCabecera.Estado = "EM";//Enviando Marcación
            }
            else if (objUpdateCabecera.TotalMarcacion == Request.CantidadSincronizada)
            {
                objUpdateCabecera.Estado = "ET";//Envio Terminado
            }

            await _repoAccLogMarcacionAsync.UpdateAsync(objUpdateCabecera);


            #endregion

            #region Conversion de Archivo

            byte[] bytes = Convert.FromBase64String(Request.Imagen);
            var rutaBase = _config.GetSection("Adjuntos:RutaBase").Get<string>();
            var directorio = rutaBase + "marcacionOffline/";

            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
            
            
            string nombreFile = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

            var rutaFinal = directorio +  nombreFile+"-"+ Request.CodigoBiometrico.ToString() + Request.Extension;

            await File.WriteAllBytesAsync(rutaFinal, bytes);
            //using (var stream = System.IO.File.Create(rutaFinal))
            //{
            //    await Request.Imagen.CopyToAsync(stream);
            //}

            #endregion

            bool estadoValid = false;

            #region Validacion con el SDK
            float Similarity = 0.0f;
            try
            {
                FSDK.ActivateLibrary("d40mAWrxDQyp5GkdVLHRqaaQvQ2ahhA/qXCASGeKO7O59UwUe7T/rzGs2LF5Efb5SFxItfJkkHbeBUksIC2wjrvR5ViXvpoa5fKvQkbPrauvpVSjQaQJZvjgU4daglMfk6gNJNgeYeW9t4wcV4hCQHEDqPu5Kyt0B3tAvOhLBAA=");
                FSDK.InitializeLibrary();
                var objColaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(Request.Identificacion));
                if (objColaborador is null) return new ResponseType<string>() { Data = null, Message = "Colaborador no tiene Imagen de Perfil", StatusCode = "101", Succeeded = true };

                FSDK.CImage imageCola = new FSDK.CImage(objColaborador.ImagenPerfil.RutaAcceso.ToString());
                FSDK.CImage image = new FSDK.CImage(rutaFinal); // Imagen enviada

                //File.Delete(rutaFinal);

                byte[] template = new byte[FSDK.TemplateSize];
                byte[] templateImgCola = new byte[FSDK.TemplateSize];
                FSDK.TFacePosition facePosition = new FSDK.TFacePosition();
                FSDK.TFacePosition facePositionCola = new FSDK.TFacePosition();
                facePosition = image.DetectFace();
                facePositionCola = imageCola.DetectFace();
                template = image.GetFaceTemplateInRegion(ref facePosition);
                templateImgCola = imageCola.GetFaceTemplateInRegion(ref facePositionCola);

                FSDK.MatchFaces(ref template, ref templateImgCola, ref Similarity);
                estadoValid = true;
            }
            catch (Exception)
            {
                estadoValid = false;
                
            }

            string respMonitorId;
            
            string estadoRecono = "PENDIENTE";

            if (Similarity >= 0.85)
            {
                estadoRecono = "CORRECTO";
                AccMonitorLog accMonitorLog = new()
                {
                    State = 0,
                    Time = Request.Time,
                    Pin = Request.CodigoBiometrico,
                    Device_Id = deviceId,
                    Verified = 0,
                    Device_Name = deviceName,
                    Status = 1,
                    Description = "OS",
                    Create_Time = DateTime.Now
                };

                var objResultado = await _repoMonitorLogAsync.AddAsync(accMonitorLog, cancellationToken);
                
                if (objResultado is null)
                {
                    return new ResponseType<string>() { Message = "No se ha podido registrar su marcación", StatusCode = "101", Succeeded = true };
                }
                respMonitorId = objResultado.Id.ToString();

                #region Inicio de Reproceso de marcaciones offline

                string query = "EXEC [dbo].[EAPP_SP_REPROCESA_MARCACIONES_OFFLINE] NULL, NULL, NULL, '" + Request.Time.Date.ToString("yyyy/MM/dd HH:mm:ss") + "' , '" + Request.Time.ToString("yyyy/MM/dd HH:mm:ss") + "',  '" + Request.Identificacion + "';";
                using IDbConnection con = new SqlConnection(ConnectionString_Marc);
                if (con.State == ConnectionState.Closed) con.Open();
                con.Query(query);
                if (con.State == ConnectionState.Open) con.Close();

                #endregion


            }
            else
            {
                estadoRecono = "Novedad " + Math.Round((Similarity * 100),0) + "% de Similitud." ;
                respMonitorId = null;
            }

            #endregion

            MonitorLogFileOffline objFile = new()
            {
                Id = Guid.NewGuid(),
                MonitorId = respMonitorId,
                RutaImagen = rutaFinal,
                EstadoValidacion = estadoValid,
                EstadoReconocimiento = estadoRecono,
                FechaRegistro = DateTime.Now,
                UsuarioCreacion = IdentificacionSesion,
                Identificacion = Request.Identificacion,
                Time = Request.Time
            };





            var result = await _repoMonitorLogFileAsync.AddAsync(objFile);



            return new ResponseType<string>() { Message = "Marcación Offline registrada correctamente", StatusCode = "100", Succeeded = true };
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<string>() { Message = CodeMessageResponse.GetMessageByCode("102"), StatusCode = "102", Succeeded = false };

        }
    }

    public async Task<ResponseType<List<NovedadesMarcacionOfflineResponse>>> NovedadesMaracionOffline(string CodUdn,string Identificacion, DateTime? FechaDesde, DateTime? FechaHasta,int? DevideId ,string IdentificacionSesion, CancellationToken cancellationToken)
    {
        try
        {

            var listMarcacionOff = await _repoMarcacionOffline.ListAsync(new NovedadesMarcacionOfflineSpec(Identificacion, FechaDesde, FechaHasta, DevideId, CodUdn));


            var result = _mapper.Map<List<NovedadesMarcacionOfflineResponse>>(listMarcacionOff);


            return new ResponseType<List<NovedadesMarcacionOfflineResponse>>() { Data = result.OrderByDescending(x => x.Time).ToList(), Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<List<NovedadesMarcacionOfflineResponse>>() { Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002", Succeeded = false };

        }
        



    }


    private string EvaluaTipoSolicitud(Guid? idFeature)
    {
        Guid permiso = Guid.Parse("DE4D17BD-9F03-4CCB-A3C0-3F37629CEA6A");
        Guid justificacion = Guid.Parse("16D8E575-51A2-442D-889C-1E93E9F786B2");
        Guid vacacion = Guid.Parse("26A08EC8-40FE-435C-8655-3F570278879E");
        if (idFeature != null)
        {
            if (idFeature == permiso)
            {
                return "PER";
            }
            else if (idFeature == justificacion)
            {
                return "JUS";
            }
            else if (idFeature == vacacion)
            {
                return "VAC";
            }
            else
            {
                return "";
            }
        }

        return "";
    }

}
