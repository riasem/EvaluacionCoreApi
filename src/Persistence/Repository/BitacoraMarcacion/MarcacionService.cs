
using Ardalis.Specification;
using AutoMapper;
using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EnrolApp.Application.Features.Marcacion.Specifications;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using EvaluacionCore.Application.Features.Biometria.Specifications;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Application.Features.EvalCore.Dto;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.Locacions.Specifications;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesTxt;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateCabeceraLog;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionApp;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionOffline;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Specifications;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Common;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using EvaluacionCore.Domain.Entities.Marcaciones;
using EvaluacionCore.Domain.Entities.Organizacion;
using EvaluacionCore.Domain.Entities.Seguridad;
using Luxand;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X500;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using TurnoLaboral = EvaluacionCore.Application.Features.Marcacion.Dto.TurnoLaboral;
using TurnoReceso = EvaluacionCore.Application.Features.Marcacion.Dto.TurnoReceso;

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
    private readonly IRepositoryGRiasemAsync<DispositivoMarcacion> _repoDispMarcaAsync;

    private readonly IRepositoryGRiasemAsync<AlertasNovedadMarcacion> _repoNovedadMarcacion;
    private readonly IRepositoryAsync<TurnoColaborador> _repositoryTurnoColAsync;
    private readonly IRepositoryAsync<Localidad> _repoLocalidad;
    private readonly IRepositoryAsync<Cliente> _repoCliente;
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoCola;
    private readonly IRepositoryAsync<MarcacionColaborador> _repoMarcacionCola;
    private readonly IRepositoryAsync<LocalidadColaborador> _repoLocalColab;
    private readonly IRepositoryGRiasemAsync<MarcacionOffline> _repoMarcacionOffline;
    private readonly IRepositoryAsync<LicenciaTerceroSG> _repoLicencia;
    private readonly IRepositoryAsync<LocalidadAdministrador> _repoLocalAdministrador;


    private readonly IConfiguration _config;
    private readonly IBiometria _repoBiometriaAsync;
    private readonly IMapper _mapper;

    private readonly IRepositoryAsync<CargoEje> _repoEje;
    private readonly IEvaluacion _EvaluacionAsync;

    private readonly IMarcacionOffline _MarcacionesOffline;

    private readonly string Esquema = null;
    private string ConnectionString_Marc { get; }
    private string NombreStoreProcedure = null;
    private string fotoPerfilDefecto = string.Empty;

    // Guid de la Licencia de Luxand - SDK RECONOCIMIENTO FACIAL PAGO MENSUAL
    private string GuidLicenciaLuxand = "A199017E-3EA1-4FB0-929D-BCB10B6E2F90";

    public MarcacionService(IRepositoryGRiasemAsync<UserInfo> repoUserInfoAsync, IRepositoryGRiasemAsync<CheckInOut> repoCheckInOutAsync, IRepositoryAsync<TurnoColaborador> repositoryTurnoCol,
        IRepositoryAsync<Localidad> repoLocalidad, ILogger<MarcacionColaborador> log, IEvaluacion repository,
        IRepositoryAsync<TurnoColaborador> repoTurnoCola, IConfiguration config, IRepositoryAsync<CargoEje> repoEje,
        IRepositoryAsync<MarcacionColaborador> repoMarcacionCola, IRepositoryGRiasemAsync<AccMonitorLog> repoMonitorLogAsync,
        IRepositoryAsync<Cliente> repoCliente, IRepositoryAsync<LocalidadColaborador> repoLocalColab,
        IRepositoryGRiasemAsync<AccMonitoLogRiasem> repoMonitoLogRiasemAsync, IRepositoryGRiasemAsync<AlertasNovedadMarcacion> repoNovedadMarcacion, IBiometria repoBiometriaAsync, IRepositoryGRiasemAsync<Machines> repoMachinesAsync,IMapper mapper, IRepositoryGRiasemAsync<MonitorLogFileOffline> repoMonitorLogFileAsync,
        IRepositoryGRiasemAsync<MarcacionOffline> repoMarcacionOffline, IRepositoryGRiasemAsync<AccLogMarcacionOffline> repoAccLogMarcacionAsync, IRepositoryGRiasemAsync<DispositivoMarcacion> repoDispMarcaAsync,
        IMarcacionOffline MarcacionesOffline, IRepositoryAsync<LicenciaTerceroSG> repoLicencia, IRepositoryAsync<LocalidadAdministrador> repoLocalAdministrador)
    {
        _EvaluacionAsync = repository;
        _MarcacionesOffline = MarcacionesOffline;
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
        _repoDispMarcaAsync = repoDispMarcaAsync;
        _repoLicencia = repoLicencia;
        _repoLocalAdministrador = repoLocalAdministrador;
    }
    public async Task<ResponseType<MarcacionResponseType>> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken)
    {
        _log.LogInformation("VACIADO: CreateMarcacion-> CodigoEmpleado: " + Request.CodigoEmpleado + ", DispositivoId: " + Request.DispositivoId + ", IdentificacionSesion: " + Request.IdentificacionSesion + ", LocalidadId: " + Request.LocalidadId + ", tipoComunicacion: " + Request.TipoComunicacion + ", IdetificacionSesion: " + Request.IdentificacionSesion);
        try
        {
            string descripcion = "";
            var consultaMonitoLogRiasem = Request.ConsultaMonitoLogRiasem;
            var marcacionColaborador = DateTime.Now;
            if (Request.Time != DateTime.MinValue)
            {
                marcacionColaborador = Request.Time;
            }
            if (Request.Descripcion is not null)
            {
                descripcion = Request.Descripcion;
            }
            MarcacionResponseType objResultFinal = new();


            //var objLocalidad = await _repoLocalidad.FirstOrDefaultAsync(new GetLocalidadByIdSpec(Request.LocalidadId, Request.CodigoEmpleado), cancellationToken);

            //if (objLocalidad == null) return new ResponseType<MarcacionResponseType>() { Message = "Localidad incorrecta", Succeeded = true, StatusCode = "101" };

            //Validación de turno que corresponde
            //var objTurno = await _repoTurnoCola.ListAsync(new TurnosByIdClienteSpec(objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Id), cancellationToken);

            //if (Request.DispositivoId == objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.DispositivoId)
            //{
            var deviceId = 999;
            var deviceName = "ENROLAPP";

            var objUserSesion = await _repoEje.FirstOrDefaultAsync(new GetEjeByIdentificacionSpec(Request.IdentificacionSesion));
            if (objUserSesion != null)
            {
                var objMachines = await _repoMachinesAsync.GetByIdAsync(objUserSesion.DeviceId);
                if (objMachines != null)
                {
                    deviceId = objMachines.ID;
                    deviceName = objMachines.MachineAlias;
                }
            }

            // Por defecto asignaremos el tipo de Comunicacion "RED"
            int tipoComunicacion = 3;
            // Si se recibe el tipo de Comunicacion desde el dispositivo de marcacion
            if (Request.TipoComunicacion != null && Request.TipoComunicacion != "")
            {
                tipoComunicacion = Int32.Parse(Request.TipoComunicacion);
            }

            // Define el registro de marcacion que se almancenara en ACC_MONITOR_LOG
            AccMonitorLog accMonitorLog = new()
            {
                State = 0,
                Time = marcacionColaborador,
                Pin = Request.CodigoEmpleado, /*objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador?.CodigoConvivencia,*/
                Device_Id = deviceId, //parametrizar desde el request
                Verified = 0,
                Device_Name = deviceName, //parametrizar desde el request
                Status = 1,
                Create_Time = DateTime.Now,
                Log_Tag = tipoComunicacion,
                Description = descripcion
            };

            var objResultado = await _repoMonitorLogAsync.AddAsync(accMonitorLog, cancellationToken);
            if (objResultado is null)
            {
                return new ResponseType<MarcacionResponseType>() { Message = "No se ha podido registrar su marcación", StatusCode = "101", Succeeded = true };
            }
            if (consultaMonitoLogRiasem == true)
            {
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
            } else
            {
                objResultFinal = new()
                {
                    MarcacionId = objResultado.Id,
                    TipoMarcacion = null,
                    EstadoMarcacion = null
                };
            }

            //var colaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByCodBiometrico(Request.CodigoEmpleado), cancellationToken);
            //DateTime fechaDesde = marcacionColaborador.Date;



            //string query = "EXEC [dbo].[EAPP_SP_REPROCESA_MARCACIONES] NULL, NULL, NULL, '" + fechaDesde.ToString("yyyy/MM/dd HH:mm:ss") + "' , '" + marcacionColaborador.ToString("yyyy/MM/dd HH:mm:ss") + "',  '" + colaborador.Identificacion + "';";
            //using IDbConnection con = new SqlConnection(ConnectionString_Marc);
            //if (con.State == ConnectionState.Closed) con.Open();
            //con.Query(query);
            //if (con.State == ConnectionState.Open) con.Close();

            return new ResponseType<MarcacionResponseType>() { Data = objResultFinal, Message = "Marcación registrada correctamente", StatusCode = "100", Succeeded = true };

        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            _log.LogInformation("EXCEPTION 30: " + ex.Message);
            return new ResponseType<MarcacionResponseType>() { Message = CodeMessageResponse.GetMessageByCode("102"), StatusCode = "102", Succeeded = false };
        }


    }


    public async Task<ResponseType<CreateMarcacionResponseType>> CreateMarcacionApp(CreateMarcacionAppRequest Request, string IdentificacionSesion, CancellationToken cancellationToken)
    {
        try
        {
            _log.LogInformation("VACIADO: CreateMarcacionApp-> Extension: " + Request.Extension + ", DispositivoId: " + Request.DispositivoId + ", Identificacion: " + Request.Identificacion + ", LocalidadId: " + Request.LocalidadId + ", Nombre: " + Request.Nombre + ", tipoComunicacion: " + Request.TipoComunicacion + ", IdentificacionSesion: " + IdentificacionSesion);
            var objLocalidadColaborador = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(Request.Identificacion));
            if (!objLocalidadColaborador.Any()) return new ResponseType<CreateMarcacionResponseType>() { Message = "No tiene Localidad Asignada", StatusCode = "101", Succeeded = true };
            var localidadSesion = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(IdentificacionSesion));
            if (objLocalidadColaborador.ElementAt(0).Colaborador.FacialPersonId is null) return new ResponseType<CreateMarcacionResponseType>() { Message = "Debes registrar tu foto de perfil para poder realizar reconocimiento facial", StatusCode = "101", Succeeded = true };

            #region Validacion de localidades
            var resultLocalidad = objLocalidadColaborador.Any(x => localidadSesion.Any(ls => ls.IdLocalidad == x.IdLocalidad));
            if (!resultLocalidad) return new ResponseType<CreateMarcacionResponseType>() { Message = "No estas autorizado para registrar tu marcación en esta localidad", StatusCode = "101", Succeeded = true };

            #endregion

            AuthenticationFacialRequest requestFacial = new()
            {
                Base64 = Request.Base64,
                Extension = Request.Extension,
                FacialPersonUid = objLocalidadColaborador.ElementAt(0).Colaborador.FacialPersonId.ToString(),
                Nombre = Request.Nombre,
                Identificacion = Request.Identificacion
            };

            // Si la identificacion de la persona que marca es la misma que la del dispositivo
            var identificacionDispositivo = IdentificacionSesion;
            if (IdentificacionSesion == Request.Identificacion)
            {
                identificacionDispositivo = "0123456781"; // Identificacion que represta a la App EnrolApp (dispositivo 999)
            }

            string OnlineOffline = "ONLINE";
            ResponseType<string> resultBiometria = await _repoBiometriaAsync.AuthenticationFacialAsync(requestFacial, identificacionDispositivo, OnlineOffline);
            _log.LogInformation("RESPUESTA AuthenticationFacialAsync:resultBiometria - StatusCode: " + resultBiometria.StatusCode + ", Message: " + resultBiometria.Message + ",Succeeded " + resultBiometria.Succeeded);
            if (resultBiometria.StatusCode != "100") return new ResponseType<CreateMarcacionResponseType>() { Message = resultBiometria.Message, StatusCode = resultBiometria.StatusCode, Succeeded = resultBiometria.Succeeded };

            // Se define por defecto el tipo comunicacion por Datos Moviles
            var tipoComunicacion = "1";
            if (Request.TipoComunicacion != null && Request.TipoComunicacion != "")
            {
                tipoComunicacion = Request.TipoComunicacion;
            }

            // Define el registro de marcacion que se almacenara en ACC_MONITOR_LOG
            CreateMarcacionRequest requestMarcacion = new()
            {
                CodigoEmpleado = objLocalidadColaborador.ElementAt(0).Colaborador.CodigoConvivencia,
                DispositivoId = Request.DispositivoId,
                LocalidadId = Request.LocalidadId,
                IdentificacionSesion = identificacionDispositivo,
                TipoComunicacion = tipoComunicacion,
                ConsultaMonitoLogRiasem = false
            };


            var resultMarcacion = await CreateMarcacion(requestMarcacion, cancellationToken);
            CreateMarcacionResponseType data = new();
            data.Colaborador = objLocalidadColaborador.ElementAt(0).Colaborador.Nombres + " " + objLocalidadColaborador.ElementAt(0).Colaborador.Apellidos;
            // Esta asignacion a data.Colaborador se la utiliza para que en el Response presente la informacion tecnica de nucleos y hardware que requiere Luxand
            // data.Colaborador = resultBiometria.Data.ToString();
            data.RutaImagen = objLocalidadColaborador.ElementAt(0).Colaborador.ImagenPerfilId is null ? "" : objLocalidadColaborador.ElementAt(0).Colaborador.ImagenPerfil.RutaAcceso;
            data.Marcacion = DateTime.Now;

            return new ResponseType<CreateMarcacionResponseType>() { Message = resultMarcacion.Message, StatusCode = resultMarcacion.StatusCode, Data = data, Succeeded = resultMarcacion.Succeeded };
        } catch (Exception ex)
        {
            _log.LogInformation("EXCEPTION 31: " + ex.Message);
            throw;
        }
    }

    public async Task<ResponseType<CreateMarcacionResponseType>> CreateMarcacionAppLast(CreateMarcacionAppLastRequest Request, string IdentificacionSesion, CancellationToken cancellationToken)
    {
        _log.LogInformation("VACIADO: CreateMarcacionAppLast-> Adjunto: " + Request.Adjunto + ", DispositivoId: " + Request.DispositivoId + ", Identificacion: " + Request.Identificacion + ", TipoComunicacion: "+Request.TipoComunicacion);
        var objLocalidadColaborador = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(Request.Identificacion));
        if (!objLocalidadColaborador.Any()) return new ResponseType<CreateMarcacionResponseType>() { Message = "No tiene Localidad Asignada", StatusCode = "101", Succeeded = true };
        var localidadSesion = await _repoLocalColab.ListAsync(new GetLocationByColaboradorSpec(IdentificacionSesion));
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
        String OnlineOffline = "ONLINE";
        ResponseType<string> resultBiometria = await _repoBiometriaAsync.AuthenticationFacialLastAsync(requestFacial, IdentificacionSesion, OnlineOffline);
        _log.LogInformation("RESPUESTA AuthenticationFacialAsync:resultBiometria - StatusCode: " + resultBiometria.StatusCode + ", Message: " + resultBiometria.Message + ",Succeeded " + resultBiometria.Succeeded);

        if (resultBiometria.StatusCode != "100") return new ResponseType<CreateMarcacionResponseType>() { Message = resultBiometria.Message, StatusCode = resultBiometria.StatusCode, Succeeded = resultBiometria.Succeeded };

        // Se define por defecto el tipo comunicacion por Datos Moviles
        var tipoComunicacion = "1";
        if (Request.TipoComunicacion != null && Request.TipoComunicacion != "")
        {
            tipoComunicacion = Request.TipoComunicacion;
        }

        CreateMarcacionRequest requestMarcacion = new()
        {
            CodigoEmpleado = objLocalidadColaborador.ElementAt(0).Colaborador.CodigoConvivencia,
            DispositivoId = Request.DispositivoId,
            IdentificacionSesion = IdentificacionSesion,
            TipoComunicacion = tipoComunicacion,
            ConsultaMonitoLogRiasem = false
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
        _log.LogInformation("VACIADO: CreateMarcacionWeb-> TipoMarcacion: " + Request.TipoMarcacion + ", ExtensionArchivo: " + Request.ExtensionArchivo + ", IdentificacionColaborador: " + Request.IdentificacionColaborador + ", IdentificacionJefe: " + Request.IdentificacionJefe + ", NombreArchivo: " + Request.NombreArchivo + ", PinColaborador: " + Request.PinColaborador + ", Base64Archivo: " + Request.Base64Archivo+", TipoMarcacion: " + Request.TipoMarcacion + ", TipoComunicacion: "+Request.TipoComunicacion);
        try
        {
            string tipoMarcacion = string.IsNullOrEmpty(Request.TipoMarcacion) ? string.Empty : Request.TipoMarcacion.ToUpper();

            // Se estable que el device_id correspondiente al canal Web es el 998
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

            // Valida si el colaborador que esta marcando pertenece o no a la localidad de su jefe establecido
            //var localidades = jefeLocalidad.Where(jl => colaboradorLocalidad.Any(cl => cl.IdLocalidad == jl.IdLocalidad)).ToList();
            //if (!localidades.Any())
            //    return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = "Colaborador no corresponde a la localidad", StatusCode = "101", Succeeded = false };

            // Si el tipo de Marcacion es con Reconocimiento Facial
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
                    Identificacion = Request.IdentificacionColaborador,
                };

                string OnlineOffline = "ONLINE";
                var respAuth = await _repoBiometriaAsync.AuthenticationFacialAsync(objAuth, Request.IdentificacionJefe, OnlineOffline);

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

            // Por defecto asignaremos el tipo de Comunicacion "RED"
            int tipoComunicacion = 3;
            // Si se recibe el tipo de Comunicacion desde el dispositivo de marcacion
            if (Request.TipoComunicacion != null && Request.TipoComunicacion != "")
            {
                tipoComunicacion = Int32.Parse(Request.TipoComunicacion);
            }

            // Define el registro de marcacion que se almancenara en ACC_MONITOR_LOG
            AccMonitorLog accMonitorLog = new()
            {
                State = 0,
                Time = marcacionColaborador,
                Pin = string.IsNullOrEmpty(colaborador.CodigoConvivencia) ? string.Empty : colaborador.CodigoConvivencia,
                Device_Id = deviceId, //Device ID para en EnrolApp Web
                Verified = 0,
                Device_Name = deviceName,
                Status = 1,
                Create_Time = DateTime.Now,
                Log_Tag = tipoComunicacion
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
            _log.LogInformation("EXCEPTION 20: " + ex.Message);
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

    public async Task<ResponseType<List<NovedadMarcacionWebType>>> ConsultaAsistencia(string Identificacion, string FiltroNovedades, DateTime FDesde, DateTime FHasta, CancellationToken cancellationToken)
    {
        try
        {
            List<NovedadMarcacionWebType> listaEvaluacionAsistencia = new();
            
            string[] filtroNovedades = FiltroNovedades.Replace("-", ",").Split(",");

            var itemCol = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(Identificacion), cancellationToken);
            if (itemCol is null) return new ResponseType<List<NovedadMarcacionWebType>>() { Data = null, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };

            var colaborador = await _EvaluacionAsync.ConsultaColaborador(itemCol.Identificacion);
            List<ControlAsistenciaType> objControlAsistencia = await _EvaluacionAsync.ConsultaAsistencia(itemCol.CodigoConvivencia, FDesde, FHasta);
            foreach (var turnoColaborador in objControlAsistencia)
            {
                // Ingresado por CADCOMP S.A. 31/10/2023
                Boolean insertar = false;
                //
                List<Application.Features.Marcacion.Dto.Novedad> novedades = new();
                string fEntrada = turnoColaborador?.FechaHoraIngreso.ToString();
                DateTime? fechaEntrada = !string.IsNullOrEmpty(fEntrada) ? Convert.ToDateTime(fEntrada, CultureInfo.InvariantCulture) : null;
                string fSalida = turnoColaborador?.FechaHoraSalida.ToString();
                DateTime? fechaSalida = !string.IsNullOrEmpty(fSalida) ? Convert.ToDateTime(fSalida, CultureInfo.InvariantCulture) : null;

                string fEntradaReceso = turnoColaborador?.FechaHoraEntradaReceso.ToString();
                DateTime? fechaEntradaReceso = !string.IsNullOrEmpty(fEntradaReceso) ? Convert.ToDateTime(fEntradaReceso, CultureInfo.InvariantCulture) : null;
                string fSalidaReceso = turnoColaborador?.FechaHoraSalidaReceso.ToString();
                DateTime? fechaSalidaReceso = !string.IsNullOrEmpty(fSalidaReceso) ? Convert.ToDateTime(fSalidaReceso, CultureInfo.InvariantCulture) : null;

                // Se prepara la informacion de retorno del turno de labores
                TurnoLaboral turnoLaborall = new()
                {
                    //turno
                    Id = turnoColaborador?.IdTurno ?? null,
                    CodigoTurno = turnoColaborador?.CodigoTurno ?? null,
                    ClaseTurno = turnoColaborador?.ClaseTurno ?? null,
                    Entrada = turnoColaborador?.Entrada ?? null,
                    Salida = turnoColaborador?.Salida ?? null,
                    TotalHoras = turnoColaborador?.TotalHoras ?? "0",

                    MarcacionEntrada = fechaEntrada,
                    EstadoEntrada = turnoColaborador?.EstadoIngreso ?? "",
                    MinutosNovedadIngreso = turnoColaborador?.MinutosNovedadIngreso ?? 0,
                    NovedadIngreso = turnoColaborador?.NovedadIngreso ?? "",
                    FechaSolicitudEntrada = turnoColaborador?.FechaSolicitudIngreso ?? null,
                    UsuarioSolicitudEntrada = turnoColaborador?.UsuarioSolicitudIngreso ?? "0",
                    IdSolicitudEntrada = turnoColaborador?.IdSolicitudIngreso ?? Guid.Empty,
                    IdFeatureEntrada = turnoColaborador?.IdFeatureIngreso ?? Guid.Empty,
                    TipoSolicitudEntrada = EvaluaTipoSolicitud(turnoColaborador?.IdFeatureIngreso),

                    MarcacionSalida = fechaSalida,
                    EstadoSalida = turnoColaborador?.EstadoSalida ?? "",
                    MinutosNovedadSalida = turnoColaborador?.MinutosNovedadSalida ?? 0,
                    NovedadSalida = turnoColaborador?.NovedadSalida ?? "",
                    FechaSolicitudSalida = turnoColaborador?.FechaSolicitudSalida ?? null,
                    UsuarioSolicitudSalida = turnoColaborador?.UsuarioSolicitudSalida ?? "0",
                    IdSolicitudSalida = turnoColaborador?.IdSolicitudSalida ?? Guid.Empty,
                    IdFeatureSalida = turnoColaborador?.IdFeatureSalida ?? Guid.Empty,
                    TipoSolicitudSalida = EvaluaTipoSolicitud(turnoColaborador?.IdFeatureSalida),
                };

                // Novedad de Turno de Receso por exceso de tiempo de receso
                var EstadoSalidaReceso = "";
                if ((turnoColaborador?.NovedadSalidaReceso ?? "") != "" && (turnoColaborador?.NovedadSalidaReceso ?? "") != "OP")
                {
                    EstadoSalidaReceso = "ER";
                }

                TurnoReceso turnoReceso = new()
                {
                    //turno de receso asignado
                    Id = turnoColaborador?.IdTurno ?? null,
                    Entrada = turnoColaborador?.Entrada ?? null,
                    Salida = turnoColaborador?.Salida ?? null,
                    TotalHoras = turnoColaborador?.TotalHoras ?? "0",

                    //marcaciones de receso entrada
                    MarcacionEntrada = fechaEntradaReceso,
                    MinutosNovedadEntradaReceso = turnoColaborador?.MinutosNovedadEntradaReceso ?? 0,
                    NovedadEntradaReceso = turnoColaborador?.NovedadEntradaReceso ?? "",
                    FechaSolicitudEntradaReceso = null,
                    UsuarioSolicitudEntradaReceso = "",
                    IdSolicitudEntradaReceso = Guid.Empty,
                    EstadoEntradaReceso = turnoColaborador?.NovedadEntradaReceso,
                    IdFeatureEntradaReceso = Guid.Empty,
                    TipoSolicitudEntradaReceso = "",

                    MarcacionSalida = fechaSalidaReceso,
                    MinutosNovedadSalidaReceso = turnoColaborador?.MinutosNovedadSalidaReceso ?? 0,
                    NovedadSalidaReceso = turnoColaborador?.NovedadSalidaReceso ?? "",
                    FechaSolicitudSalidaReceso = null,
                    UsuarioSolicitudSalidaReceso = "",
                    IdSolicitudSalidaReceso = Guid.Empty,
                    EstadoSalidaReceso = EstadoSalidaReceso,
                    IdFeatureSalidaReceso = Guid.Empty,
                    TipoSolicitudSalidaReceso = ""
                };

                // Solo si el turno es un turno LABORAL se deben considerar Novedades, caso contrario no se reportan novedades
                if ((turnoColaborador?.ClaseTurno ?? "") == "LABORAL")
                {
                    // Valida si el estado de la marcacion de Ingreso consta entre los filtros de consulta
                    if (filtroNovedades.Contains(turnoColaborador?.EstadoIngreso ?? ""))
                    {
                        // Novedad en la marcacion de Entrada
                        if (fechaEntrada is not null && (turnoColaborador?.EstadoIngreso ?? "SN") != "SN")
                        {
                            // Consulta si existe alguna solicitud de permiso aprobada en la fecha de marcacion de entrada
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            if (fechaEntrada is not null)
                            {
                                solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudesAprobadasbyCodigoBiometrico(itemCol.CodigoConvivencia, fechaEntrada.Value);
                            }
                            if (solicitudesPermiso.Count > 0 && (solicitudesPermiso[0]?.NumeroSolicitud.ToString() ?? "0") != "0")
                            {
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = "ATRASO JUSTIFICADO", // AJ
                                    MinutosNovedad = turnoColaborador?.MinutosNovedadIngreso.ToString() ?? "",
                                    EstadoMarcacion = "AJ",
                                    FechaAprobacion = solicitudesPermiso[0]?.FechaAprobacion
                                });
                            }
                            else
                            {
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = turnoColaborador?.NovedadIngreso ?? "",
                                    MinutosNovedad = turnoColaborador?.MinutosNovedadIngreso.ToString() ?? "",
                                    EstadoMarcacion = turnoColaborador?.EstadoIngreso ?? "",
                                    FechaAprobacion = turnoColaborador?.FechaSolicitudIngreso
                                });
                            }
                        } else
                        {
                            insertar = true;
                        }
                        // Novedad en la marcacion de Salida
                        if (fechaSalida is not null && (turnoColaborador?.EstadoSalida ?? "SN") != "SN")
                        {
                            // Consulta si existe alguna solicitud de permiso aprobada en la fecha de marcacion de salida
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            if (fechaSalida is not null)
                            {
                                solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudesAprobadasbyCodigoBiometrico(itemCol.CodigoConvivencia, fechaSalida.Value);
                            }
                            if (solicitudesPermiso.Count > 0 && (solicitudesPermiso[0]?.NumeroSolicitud.ToString() ?? "0") != "0")
                            {
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = "SALIDA ANTICIPADA JUSTIFICADA", // SJ
                                    MinutosNovedad = turnoColaborador?.MinutosNovedadSalida.ToString() ?? "",
                                    EstadoMarcacion = "SJ",
                                    FechaAprobacion = solicitudesPermiso[0]?.FechaAprobacion
                                });
                            }
                            else
                            {
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = turnoColaborador?.NovedadSalida ?? "",
                                    MinutosNovedad = turnoColaborador?.MinutosNovedadSalida.ToString() ?? "",
                                    EstadoMarcacion = turnoColaborador?.EstadoSalida ?? "",
                                    FechaAprobacion = turnoColaborador?.FechaSolicitudSalida
                                });
                            }
                        } else
                        {
                            insertar = true;
                        }
                        // Falta a dia de labores
                        // Se debe considerar las solicitudes de permiso
                        if (fechaEntrada is null && fechaSalida is null)
                        {
                            // Consulta si existe alguna solicitud de permiso aprobada en la fecha del turno
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            string fTurno = turnoColaborador?.FechaTurno.ToString();
                            DateTime? fechaTurno = !string.IsNullOrEmpty(fTurno) ? Convert.ToDateTime(fTurno, CultureInfo.InvariantCulture) : null;
                            if (fechaTurno is not null)
                            {
                                solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudesAprobadasbyCodigoBiometrico(itemCol.CodigoConvivencia, fechaTurno.Value);
                            }
                            if (solicitudesPermiso.Count > 0 && (solicitudesPermiso[0]?.NumeroSolicitud.ToString() ?? "0") != "0")
                            {
                                // Falta justificada
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = "FALTA JUSTIFICADA", // FJ
                                    MinutosNovedad = "",
                                    EstadoMarcacion = "FJ",
                                    FechaAprobacion = null
                                });
                            }
                            else
                            {
                                // Falta injustificada
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = "FALTA INJUSTIFICADA", // FI
                                    MinutosNovedad = "",
                                    EstadoMarcacion = "FI",
                                    FechaAprobacion = null
                                });
                            }
                        }
                        // No tiene turno asigando
                        if ((turnoColaborador?.CodigoTurno ?? "") == "")
                        {
                            novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                            {
                                Descripcion = "NO TIENE TURNO ASIGNADO", // NT
                                MinutosNovedad = "",
                                EstadoMarcacion = "NT",
                                FechaAprobacion = null
                            });
                        }
                        // Sin Registro de Salida y sin turno asignado
                        // Se debe considerar las solicitudes de permiso
                        if (fechaEntrada is not null && fechaSalida is null)
                        {
                            // Consulta si existe alguna solicitud de permiso aprobada para la fecha en la que debia marcar la salida
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            string fSalidaTurno = turnoColaborador?.Salida.ToString();
                            DateTime? fechaSalidaTurno = !string.IsNullOrEmpty(fSalidaTurno) ? Convert.ToDateTime(fSalidaTurno, CultureInfo.InvariantCulture) : null;
                            if (fechaSalidaTurno is not null)
                            {
                                solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudesAprobadasbyCodigoBiometrico(itemCol.CodigoConvivencia, fechaSalidaTurno.Value);
                            }
                            if (solicitudesPermiso.Count > 0 && (solicitudesPermiso[0]?.NumeroSolicitud.ToString() ?? "0") != "0")
                            {
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = "SALIDA ANTICIPADA JUSTIFICADA", // SJ
                                    MinutosNovedad = turnoColaborador?.MinutosNovedadSalida.ToString() ?? "",
                                    EstadoMarcacion = "SJ",
                                    FechaAprobacion = solicitudesPermiso[0]?.FechaAprobacion
                                });
                            }
                            else
                            {
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = "SIN REGISTRO DE SALIDA Y SIN TURNO ASIGNADO", // NS
                                    MinutosNovedad = "",
                                    EstadoMarcacion = "NS",
                                    FechaAprobacion = null
                                });
                            }
                        }
                        // Sin Registro de retorno de receso
                        // Se debe considerar las solicitudes de permiso
                        if (fechaEntradaReceso is not null && fechaSalidaReceso is null)
                        {
                            // Consulta si existe alguna solicitud de permiso aprobada para la fecha en la que debia marcar la salida
                            List<ConsultaSolicitudPermisoType> solicitudesPermiso = new();
                            string fSalidaTurno = turnoColaborador?.Salida.ToString();
                            DateTime? fechaSalidaTurno = !string.IsNullOrEmpty(fSalidaTurno) ? Convert.ToDateTime(fSalidaTurno, CultureInfo.InvariantCulture) : null;
                            if (fechaSalidaTurno is not null)
                            {
                                solicitudesPermiso = await _EvaluacionAsync.ConsultaSolicitudesAprobadasbyCodigoBiometrico(itemCol.CodigoConvivencia, fechaSalidaTurno.Value);
                            }
                            if (solicitudesPermiso.Count > 0 && (solicitudesPermiso[0]?.NumeroSolicitud.ToString() ?? "0") != "0" && fechaSalida is null)
                            {
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = "SALIDA ANTICIPADA JUSTIFICADA", // SJ
                                    MinutosNovedad = turnoColaborador?.MinutosNovedadSalida.ToString() ?? "",
                                    EstadoMarcacion = "SJ",
                                    FechaAprobacion = solicitudesPermiso[0]?.FechaAprobacion
                                });
                            }
                            else
                            {
                                novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                                {
                                    Descripcion = "SIN REGISTRO DE RETORNO DE RECESO", // NR
                                    MinutosNovedad = "",
                                    EstadoMarcacion = "NR",
                                    FechaAprobacion = null
                                });
                            }
                        }
                        // Exceso de Receso
                        if (fechaEntradaReceso is not null && fechaSalidaReceso is not null &&
                            (turnoColaborador?.NovedadSalidaReceso ?? null) is not null && (turnoColaborador?.NovedadSalidaReceso ?? "") != "OP")
                        {
                            novedades.Add(new Application.Features.Marcacion.Dto.Novedad
                            {
                                Descripcion = turnoColaborador?.NovedadSalidaReceso ?? "",
                                MinutosNovedad = turnoColaborador?.MinutosNovedadSalidaReceso.ToString() ?? "",
                                EstadoMarcacion = "ER",
                                FechaAprobacion = null
                            });
                        }
                    }
                }

                //                    if (novedades.Count == 0) continue;
                // Modificado por CADCOMP S.A. 31/10/2023
                // Se va a filtrar solo aquellas que tienen las novedades 
                // seleccionadas
                
                foreach (var novedadAsist in novedades)
                {
                    foreach (var ListaNovedad in filtroNovedades)
                    {
                        if (novedadAsist.EstadoMarcacion.Equals(ListaNovedad))
                        {
                            insertar = true;
                        }
                    }
                }
                
                // Agregar al listado de marcaciones si la bandera está en true
                if (insertar == true)
                {
                    listaEvaluacionAsistencia.Add(new NovedadMarcacionWebType()
                    {
                        Colaborador = itemCol.Nombres + " " + itemCol.Apellidos,
                        Identificacion = itemCol.Identificacion,
                        CodBiometrico = itemCol.CodigoConvivencia,
                        Udn = colaborador[0].DesUdn,
                        Area = colaborador[0].DesArea,
                        SubCentroCosto = colaborador[0].DesSubcentroCosto,
                        Fecha = (DateTime)turnoColaborador.FechaAsginacion,
                        Novedades = novedades,
                        TurnoLaboral = turnoLaborall,
                        TurnoReceso = turnoReceso
                    });
                }
                    
            }
            return new ResponseType<List<NovedadMarcacionWebType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        }
        catch (Exception e)
        {
            _log.LogDebug("Excepcion: " + e.Message);
            return new ResponseType<List<NovedadMarcacionWebType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }
    }

    public async Task<ResponseType<List<NovedadMarcacionWebType>>> ConsultaNovedadMarcacionWeb(string Identificacion, string FiltroNovedades, DateTime FDesde, DateTime FHasta, CancellationToken cancellationToken)
    {
        try
        {
            //FHasta = FHasta.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<NovedadMarcacionWebType> listaEvaluacionAsistencia = new();
            string[] Identificaciones = Identificacion.Split(",");
            string[] filtroNovedades = FiltroNovedades.Split(",");

            foreach (var col in Identificaciones)
            {
                var novedadMarcacionWeb = await this.ConsultaAsistencia(col, FiltroNovedades, FDesde, FHasta, cancellationToken);
                if (novedadMarcacionWeb.Data != null) listaEvaluacionAsistencia.AddRange(novedadMarcacionWeb.Data);
            }

            return new ResponseType<List<NovedadMarcacionWebType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };

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
            var localidadDispositivo = await _repoLocalColab.ListAsync(new GetListadoLocalidadSpec(IdentificacionSesion), cancellationToken);
            var localidad = localidadDispositivo.FirstOrDefault().Localidad.Codigo;
            var administradores = await _repoLocalAdministrador.ListAsync(new GetListadoAdministradorByLocalidadSpec(localidad), cancellationToken);

            if (!colaboradores.Any() && !administradores.Any()) return new ResponseType<List<ColaboradorByLocalidadResponseType>>() { Message = "No existen Colaboradores y Administradores en la localidad", StatusCode = "001", Succeeded = true };

            if (colaboradores.Any()) {
                colaboradores = colaboradores.DistinctBy(x => x.Colaborador.Identificacion).ToList();
            }
            if (administradores.Any()) {
                administradores = administradores.DistinctBy(x => x.Identificacion).ToList();
            }

            var result = _mapper.Map<List<ColaboradorByLocalidadResponseType>>(colaboradores);

            // Adiciona, a la lista de colaboradores, los administradores del dispositivo de la localidad, si hubieren
            foreach (var administrador in administradores) {
                var colaboradorByLocalidadResponse = new ColaboradorByLocalidadResponseType()
                {
                    Identificacion = administrador.Identificacion,
                    Empleado = null,
                    RutaImagen = null,
                    NombreUdn = null, //administrador.Localidad.Empresa.RazonSocial,
                    CodigoUdn = null, //administrador.Localidad.Empresa.Codigo,
                    CodigoBiometrico = null,
                    Administrador = "S",
                    Clave = administrador.ClaveOffLine
                };
                if (!result.Any()) {
                    result = new();
                }
                result.Add(colaboradorByLocalidadResponse);
            }

            return new ResponseType<List<ColaboradorByLocalidadResponseType>>() { Data = result.OrderBy(x => x.Identificacion).ToList(), Message = CodeMessageResponse.GetMessageByCode("000"),Succeeded = true, StatusCode = "000"};
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<List<ColaboradorByLocalidadResponseType>>() { Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002", Succeeded = false};
        }

    }


    public async Task<ResponseType<string>> CreateMarcacionOffline(CreateMarcacionOfflineRequest Request,string IdentificacionSesion,string TipoCarga, CancellationToken cancellationToken)
    {
        _log.LogInformation("VACIADO: CreateMarcacionWeb-> Identificacion: " + Request.Identificacion + ", IdCabecera: " + Request.IdCabecera + ", CantidadSincronizada: " + Request.CantidadSincronizada + ", CodigoBiometrico: " + Request.CodigoBiometrico + ", Time: " + Request.Time + ", Extension: " + Request.Extension + ", Imagen: " + Request.Imagen+", TipoComunicacion: "+Request.TipoComunicacion);
        FSDK.CImage image;
        FSDK.CImage imageCola;
        try
        {
            MarcacionResponseType objResultFinal = new();

            // Por defecto define el device_id 999 que representa al dispositivo de marcacion celular
            var deviceId = 999;
            var deviceName = "EnrolApp";

            var objA = await _repoEje.ListAsync();

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
            if (Request.CantidadSincronizada == 1 && objUpdateCabecera.TotalMarcacion  > Request.CantidadSincronizada )
            {
                objUpdateCabecera.Estado = "IS";//Inicio de Sincronización
            }
            else if (objUpdateCabecera.TotalSincronizadas > 0 && Request.CantidadSincronizada < objUpdateCabecera.TotalSincronizadas)
            {
                objUpdateCabecera.Estado = "EM";//Enviando Marcación
            }
            else if (objUpdateCabecera.TotalMarcacion == Request.CantidadSincronizada)
            {
                objUpdateCabecera.Estado = "ET";//Envio Terminado
            }

            await _repoAccLogMarcacionAsync.UpdateAsync(objUpdateCabecera);

            #endregion

            bool estadoValid = false;
            string respMonitorId = "";
            var rutaFinal = "";
            string estadoRecono = "CORRECTO";
            float Similarity = 0.0f;
            float SimilarityDefinition = 0.85f;
            if (objUserSesion.SimilarityOffline is not null)
            {
                SimilarityDefinition = float.Parse(objUserSesion.SimilarityOffline.ToString(), CultureInfo.InvariantCulture.NumberFormat)/100;
            }
            var mensajeError = "";

            // Se debe evaluar si es dispositivo exige o no la validacion por reconocimiento facial en el procesamiento
            // de marcaciones offline, en la tabla CargoEje atributo sdkLuxandOffline (1) Si es requerido y (0) No es requerido

            if (TipoCarga is null || TipoCarga == "Txt")
            {
                try
                {
                    #region Conversion de Archivo
                    byte[] bytes = Convert.FromBase64String(Request.Imagen);
                    var rutaBase = _config.GetSection("Adjuntos:RutaBase").Get<string>();
                    var directorio = rutaBase + "marcacionOffline/";

                    if (!Directory.Exists(directorio))
                    {
                        Directory.CreateDirectory(directorio);
                    }

                    string nombreFile = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                    rutaFinal = directorio + nombreFile + "-" + Request.CodigoBiometrico.ToString() + Request.Extension;

                    await File.WriteAllBytesAsync(rutaFinal, bytes);
                    #endregion

                    if (objUserSesion.SdkLuxandOffline == true)
                    {

                        #region Validacion con el SDK
                        var Licencia = await _repoLicencia.FirstOrDefaultAsync(new LicenciaByServicioSpec(Guid.Parse(GuidLicenciaLuxand)));
                        if (Licencia is null) return new ResponseType<string> { StatusCode = "101", Succeeded = true, Message = "Licencia no se encuentra disponible o activa" };

                        // Obtener la Licencia del SDK de Luxand en la parametrizacion
                        var xxx = FSDK.ActivateLibrary(Licencia.CodigoLicencia);

                        string hardwareID;
                        var www = FSDK.GetHardware_ID(out hardwareID);
                        int num = 0;
                        var qqq = FSDK.GetNumThreads(ref num);
                        var licenseInfo = Licencia.CodigoLicencia;
                        var zzz = FSDK.GetLicenseInfo(out licenseInfo);
 
                        var yyy = FSDK.InitializeLibrary();

                        var objColaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(Request.Identificacion));
                        if (objColaborador is null) return new ResponseType<string>() { Data = null, Message = "Colaborador no tiene Imagen de Perfil", StatusCode = "101", Succeeded = true };

                        if (xxx != -2)
                        {
                            try
                            {
                                image = new FSDK.CImage(rutaFinal); // Imagen enviada
                                byte[] template = new byte[FSDK.TemplateSize];
                                FSDK.TFacePosition facePosition = new FSDK.TFacePosition();
                                facePosition = image.DetectFace();
                                template = image.GetFaceTemplateInRegion(ref facePosition);
                                imageCola = new FSDK.CImage(objColaborador.ImagenPerfil.RutaAcceso.ToString());
                                byte[] templateImgCola = new byte[FSDK.TemplateSize];
                                FSDK.TFacePosition facePositionCola = new FSDK.TFacePosition();
                                facePositionCola = imageCola.DetectFace();
                                templateImgCola = imageCola.GetFaceTemplateInRegion(ref facePositionCola);
                                FSDK.MatchFaces(ref template, ref templateImgCola, ref Similarity);
                            }
                            catch (Exception ex)
                            {
                                mensajeError = ex.Message;
                                estadoRecono = "NO VALIDO";
                                estadoValid = false;
                            }

                        } else
                        {
                            var datos = new
                            {
                                licencia = Licencia.CodigoLicencia,
                                retornaNucleos = qqq,
                                cantidadNucleos = num,
                                retornaHardwareID = www,
                                resulthardwareID = hardwareID

                            };
                            mensajeError = datos.ToString();

                        }
                        if (mensajeError == "")
                        {
                            // Se debe evaluar el valor de tolerancia de la similitud en el reconocimiento facial, establecido para el dispositivo, en el procesamiento
                            // de marcaciones offline, en la tabla CargoEje atributo similarityOffline, debiendo fluctuar entre 0.00 y 1.00
                            if (Similarity >= SimilarityDefinition)
                            {
                                estadoRecono = "CORRECTO";
                            }
                            else
                            {
                                estadoRecono = "Novedad " + Math.Round((Similarity * 100), 0) + "% de Similitud.";
                                respMonitorId = null;
                            }
                            estadoValid = true;
                            #endregion
                        }
                    }
                }
                catch (Exception ex)
                {
                    mensajeError = ex.Message;
                    estadoRecono = "NO VALIDO";
                    estadoValid = false;
                }
            }

            // Por defecto asignaremos el tipo de Comunicacion "RED"
            int tipoComunicacion = 3;
            // Si se recibe el tipo de Comunicacion desde el dispositivo de marcacion
            if (Request.TipoComunicacion != null && Request.TipoComunicacion != "")
            {
                tipoComunicacion = Int32.Parse(Request.TipoComunicacion);
            }

            #region Registro de marcacacion Offline
            // Tipo Carga es NULL cuando se realiza la carga de Marcaciones Offline mediante SINCRONIZACION DE MARCACIONES OFFLINE desde la TABLET
            // Tipo Carga es Txt cuando se realiza la carga de Marcaciones Offline mediante la IMPORTACION DEL ARCHIVO TXT, que fuere almacenado en la MICRO SD de la TABLET, desde el Portal Web EnrolApp
            // Tipo Carga es Excel cuando se realiza la carga de Marcaciones Offline mediante la IMPORTACION DEL ARCHIVO EXCEL desde el Portal Web EnrolApp
            // Para poder registrar, como VALIDA, la marcacion offline en la tabla ACC_MONITOR_LOG se debe considerar que:
            // Los Tipos de Carga NULL y TXT deben validar la IMAGEN CAPTURA al momento de la marcacion, con el RECONOCIMIENTO FACIAL del SDK de LUXAND
            // Mientras que el Tipo de Carga Excel NO requiere de validacion por RECONOCIMIENTO FACIAL
            if ((TipoCarga is null && estadoRecono == "CORRECTO") || (TipoCarga == "Txt" && estadoRecono == "CORRECTO") || (TipoCarga == "Excel"))
            {
                #region Registro de marcacion en la tabla de registro de marcaciones ACC_MONITOR_LOG


                // Define el registro de marcacion que se almacenara en ACC_MONITOR_LOG
                CreateMarcacionRequest requestMarcacion = new()
                {
                    CodigoEmpleado = Request.CodigoBiometrico,
                    DispositivoId = deviceId.ToString(),
                    IdentificacionSesion = IdentificacionSesion,
                    TipoComunicacion = Request.TipoComunicacion,
                    ConsultaMonitoLogRiasem = false,
                    Time = Request.Time,
                    Descripcion = "OS"
                };
                var resultMarcacion = await CreateMarcacion(requestMarcacion, cancellationToken);
                if (resultMarcacion is null || resultMarcacion.StatusCode != "100")
                {
                    return new ResponseType<string>() { Message = "No se ha podido registrar su marcación", StatusCode = "101", Succeeded = true };
                }
                respMonitorId = resultMarcacion.Data.MarcacionId.ToString();
                #endregion

                #region Reproceso de marcaciones del colaborador para considerar la marcacion offline recientemente registrada
                string query = "EXEC [dbo].[EAPP_SP_REPROCESA_MARCACIONES_OFFLINE] NULL, NULL, NULL, '" + Request.Time.Date.ToString("yyyy/MM/dd HH:mm:ss") + "' , '" + Request.Time.ToString("yyyy/MM/dd HH:mm:ss") + "',  '" + Request.Identificacion + "';";
                using IDbConnection con = new SqlConnection(ConnectionString_Marc);
                if (con.State == ConnectionState.Closed) con.Open();
                con.Query(query);
                if (con.State == ConnectionState.Open) con.Close();
                #endregion
            }
            #endregion

            MonitorLogFileOffline objFile = new()
            {
                Id = Guid.NewGuid(),
                MonitorId = respMonitorId == "" ? null:respMonitorId,
                RutaImagen = rutaFinal == "" ? null : rutaFinal,
                EstadoValidacion = estadoValid,
                EstadoReconocimiento = estadoRecono,
                FechaRegistro = DateTime.Now,
                UsuarioCreacion = IdentificacionSesion,
                Identificacion = Request.Identificacion,
                Time = Request.Time,
                TipoComunicacion = tipoComunicacion,
                MensajeError = mensajeError
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


    public async Task<ResponseType<List<DispositivosMarcacionResponse>>> GetDispositivoMarcacion()
    {
        try
        {
            var lstDispositivos = await _repoDispMarcaAsync.ListAsync();
            if (!lstDispositivos.Any()) return new ResponseType<List<DispositivosMarcacionResponse>> { Message = "No tenemos dispositivos disponibles", StatusCode = "001", Succeeded = true };
            
            var lstResult = _mapper.Map<List<DispositivosMarcacionResponse>>(lstDispositivos);

            return new ResponseType<List<DispositivosMarcacionResponse>>() { Data = lstResult, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);

            return new ResponseType<List<DispositivosMarcacionResponse>> { Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002", Succeeded = false };
        }
        
        
    }

    public async Task<ResponseType<string>> CargarMarcacionesExcel(List<CargaMarcacionesExcelRequest> request, string IdentificacionSesion, CancellationToken cancellationToken)
    {
        var firstMarcacion = request.OrderBy(x => x.Order).FirstOrDefault();
        var lastMarcacion = request.OrderByDescending(x => x.Order).FirstOrDefault();
        var totalMarcacion = request.Count();
        var tipoCarga = "Excel";

        CreateCabeceraLogRequest requestCabecera = new()
        {
            TotalMarcacion = totalMarcacion,
            Estado = "IS",
            FechaFin = lastMarcacion.Time,
            FechaInicio = firstMarcacion.Time,
            FechaSincronizacion = DateTime.Now,
            IdentificacionFin = lastMarcacion.Identificacion,
            IdentificacionInicio = firstMarcacion.Identificacion,
            TipoCarga = tipoCarga,
            TipoComunicacion = "3" // Tipo Comunicacion por RED
        };


        var idcabecera = await _MarcacionesOffline.CreateCabeceraLogOffline(requestCabecera,IdentificacionSesion, cancellationToken);

        if (idcabecera is null) return new ResponseType<string> { Message = "No se ha guardado cabecera correctamente", StatusCode = "101", Succeeded = true };
        var countMarcacion = 0;

        List<CargaMarcacionesExcelRequest> marcacionesSinProcesar = new();

        foreach (var marcacion in request.OrderBy(x => x.Order))
        {
            var cliente = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(marcacion.Identificacion));

            if (cliente is null)  marcacionesSinProcesar.Add(marcacion);

            countMarcacion++;   

            CreateMarcacionOfflineRequest requestMarcacion = new()
            {
                CodigoBiometrico = cliente.CodigoConvivencia,
                Imagen = null,
                IdCabecera = Guid.Parse(idcabecera.Data),
                Extension = null,
                Time = marcacion.Time,
                Identificacion = marcacion.Identificacion,
                CantidadSincronizada = countMarcacion,
                TipoComunicacion = "3" // Tipo de Comunicacion RED
                
            };
            var resultMarcacion = await CreateMarcacionOffline(requestMarcacion,marcacion.IdentificacionDispositivo, tipoCarga, cancellationToken);
            if (!resultMarcacion.Succeeded)
            {
                return new ResponseType<string> { Message = "No se ha procesado las marcaciones correctamente", Succeeded = true, StatusCode = "101" };   
            }

        }

        if (marcacionesSinProcesar.Any())
        {
            return new ResponseType<string> {Data= marcacionesSinProcesar.ToString() , Message = "Marcaciones Procesadas correctamente", StatusCode = "100", Succeeded = true };
        }
        else
        {
            return new ResponseType<string> { Message = "Marcaciones Procesadas correctamente", StatusCode = "100", Succeeded = true };
        }

        
    }

    public async Task<ResponseType<string>> CargarMarcacionesTxt(List<CargaMarcacionesTxtRequest> request, string IdentificacionSesion, CancellationToken cancellationToken)
    {
        try
        {
            //var dataProcesar = request.Where(x => x.Sincronizado == false && x.TipoMarcacion == "Offline").ToList();

            var firstMarcacion = request.OrderBy(x => x.Id).FirstOrDefault();
            var lastMarcacion = request.OrderByDescending(x => x.Id).FirstOrDefault();
            var totalMarcacion = request.Count();
            var tipoCarga = "Txt";

            CreateCabeceraLogRequest requestCabecera = new()
            {
                TotalMarcacion = totalMarcacion,
                Estado = "IS",
                FechaFin = lastMarcacion.FechaMarcacion,
                FechaInicio = firstMarcacion.FechaMarcacion,
                FechaSincronizacion = DateTime.Now,
                IdentificacionFin = lastMarcacion.Identificacion,
                IdentificacionInicio = firstMarcacion.Identificacion,
                TipoCarga = tipoCarga,
                TipoComunicacion = "3" // Tipo Comunicacion por RED
            };


            var idcabecera = await _MarcacionesOffline.CreateCabeceraLogOffline(requestCabecera, IdentificacionSesion, cancellationToken);

            if (idcabecera is null) return new ResponseType<string> { Message = "No se ha guardado cabecera correctamente", StatusCode = "101", Succeeded = true };
            var countMarcacion = 0;

            List<CargaMarcacionesTxtRequest> marcacionesSinProcesar = new();

            foreach (var marcacion in request.OrderBy(x => x.Id))
            {
                var cliente = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(marcacion.Identificacion));

                if (cliente is null) marcacionesSinProcesar.Add(marcacion);

                countMarcacion++;

                CreateMarcacionOfflineRequest requestMarcacion = new()
                {
                    CodigoBiometrico = cliente.CodigoConvivencia,
                    Imagen = marcacion.ImgBase,
                    IdCabecera = Guid.Parse(idcabecera.Data),
                    Extension = ".png",
                    Time = marcacion.FechaMarcacion,
                    Identificacion = marcacion.Identificacion,
                    CantidadSincronizada = countMarcacion,
                    TipoComunicacion = "3" // Tipo Comunicacion por RED

                };
                var resultMarcacion = await CreateMarcacionOffline(requestMarcacion, marcacion.IdentificacionDispositivo, tipoCarga, cancellationToken);
                if (!resultMarcacion.Succeeded)
                {
                    return new ResponseType<string> { Message = "No se ha procesado las marcaciones correctamente", Succeeded = true, StatusCode = "101" };
                }

            }

            if (marcacionesSinProcesar.Any())
            {
                return new ResponseType<string> { Data = marcacionesSinProcesar.ToString(), Message = "Marcaciones Procesadas correctamente", StatusCode = "100", Succeeded = true };
            }
            else
            {
                return new ResponseType<string> { Message = "Marcaciones Procesadas correctamente", StatusCode = "100", Succeeded = true };
            }

        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<string> { Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            
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
