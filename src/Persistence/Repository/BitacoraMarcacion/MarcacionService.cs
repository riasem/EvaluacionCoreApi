
using Dapper;
using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EnrolApp.Application.Features.Marcacion.Specifications;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Workflow.Persistence.Repository.BitacoraMarcacion;


public class MarcacionService : IMarcacion
{
    private readonly ILogger<MarcacionColaborador> _log;
    private readonly IRepositoryGRiasemAsync<UserInfo> _repoUserInfoAsync;
    private readonly IRepositoryGRiasemAsync<CheckInOut> _repoCheckInOutAsync;
    private readonly IRepositoryAsync<Localidad> _repoLocalidad;
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoCola;
    private readonly IRepositoryAsync<MarcacionColaborador> _repoMarcacionCola;
    private readonly IConfiguration _config;
    private string ConnectionString_Marc { get; }



    public MarcacionService(IRepositoryGRiasemAsync<UserInfo> repoUserInfoAsync, IRepositoryGRiasemAsync<CheckInOut> repoCheckInOutAsync,
        IRepositoryAsync<Localidad> repoLocalidad,ILogger<MarcacionColaborador> log,IRepositoryAsync<TurnoColaborador> repoTurnoCola, IConfiguration config, IRepositoryAsync<MarcacionColaborador> repoMarcacionCola)
    {
        _repoUserInfoAsync = repoUserInfoAsync;
        _repoCheckInOutAsync = repoCheckInOutAsync;
        _config = config;
        _repoLocalidad = repoLocalidad;
        _log = log;
        _repoMarcacionCola = repoMarcacionCola;
        ConnectionString_Marc = _config.GetConnectionString("Bd_Marcaciones_GRIAMSE");
        _repoTurnoCola = repoTurnoCola;
    }

    public async Task<ResponseType<string>> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken)
    {
        try
        {
            var marcacionColaborador = DateTime.Now;
            var objLocalidad = await _repoLocalidad.FirstOrDefaultAsync(new GetLocalidadByIdSpec(Request.LocalidadId, Request.CodigoEmpleado), cancellationToken);
            //var objLocalidad = await _repoLocalidadCola.FirstOrDefaultAsync(new GetLocalidadByIdSpec(Request.LocalidadId, Request.CodigoEmpleado), cancellationToken);
            //LocalidadColaborador objLocalidad = new();

            if (objLocalidad == null) return new ResponseType<string>(){ Message = "Localidad incorrecta", Succeeded = true, StatusCode = "101" };

            //Validación de turno que corresponde
            var objTurno = await _repoTurnoCola.ListAsync(new TurnosByIdClienteSpec(objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Id), cancellationToken);

            if (!objTurno.Any()) return new ResponseType<string>(){ Message = "No tiene turnos asignados", StatusCode = "101", Succeeded = true };
            var idturnovalidado = Guid.Empty;
            var tipoMarcacion = string.Empty;
            var estadoMarcacion = string.Empty;
            var countMarcacion = 0;

            foreach (var itemTurno in objTurno)
            {
                var turnoEntrada = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.Entrada.TimeOfDay.ToString());
                var mEntrada = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenEntrada.TimeOfDay.ToString());
                var mSalida = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenSalida.TimeOfDay.ToString());
                var margenEPre = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenEntradaPrevio);
                var margenEPos = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenEntradaPosterior);
                var margenSPre = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenSalidaPrevio);
                var margenSPos = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.MargenSalidaPosterior);
                var turnoSalida = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + itemTurno.Turno.Salida.TimeOfDay.ToString());

                if (marcacionColaborador >= margenEPre && marcacionColaborador <= margenEPos)
                {
                    idturnovalidado = itemTurno.Id;
                    tipoMarcacion = "E";

                    estadoMarcacion = marcacionColaborador < turnoEntrada && marcacionColaborador <= mEntrada ? "C" : "A";
                    countMarcacion = await _repoMarcacionCola.CountAsync(new MarcacionByMargen(margenEPre, margenEPos, tipoMarcacion), cancellationToken);
                    break;

                }
                else if (marcacionColaborador >= margenSPre && marcacionColaborador <= margenSPos)
                {
                    idturnovalidado = itemTurno.Id;
                    tipoMarcacion = "S";

                    estadoMarcacion = marcacionColaborador > turnoSalida && marcacionColaborador <= mSalida ? "C" : "";
                    countMarcacion = await _repoMarcacionCola.CountAsync(new MarcacionByMargen(margenSPre, margenSPos, tipoMarcacion), cancellationToken);

                    break;
                }
            }

            if (idturnovalidado == Guid.Empty && tipoMarcacion == string.Empty && estadoMarcacion == string.Empty) return new ResponseType<string>() { Message = "Actualmente no tiene turno de Entrada/Salida", StatusCode = "101", Succeeded = true };

            var userInfo = await _repoUserInfoAsync.FirstOrDefaultAsync(new UserMarcacionByCodigoSpec(Request.CodigoEmpleado), cancellationToken);
            if (userInfo is null)
            {
                UserInfo objUserInfo = new()
                {
                    Badgenumber = Request.CodigoEmpleado,
                    Ssn = objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Identificacion,
                    Name = objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Nombres,
                    LastName = objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.Apellidos,
                    //DefaultDeptId = ObjClient.Cargo.DepartamentoId.ToString(),
                    CreateOperator = "Admin",
                    CreateTime = DateTime.Now
                };

                userInfo = await _repoUserInfoAsync.AddAsync(objUserInfo, cancellationToken);
            }
            if (Request.DispositivoId == objLocalidad.LocalidadColaboradores.ElementAt(0).Colaborador.DispositivoId)
            {
                var countMarcacionCheck = await _repoCheckInOutAsync.CountAsync(new MarcacionByUserIdSpec(userInfo.UserId), cancellationToken);


                CheckInOut entityCheck = new()
                {
                    UserId = userInfo.UserId,
                    CheckTime = DateTime.Now,
                    CheckType = countMarcacionCheck == 0 ? "I" : "O",
                    Sn = Request.DispositivoId
                };


                var objResult = await _repoCheckInOutAsync.AddAsync(entityCheck,cancellationToken);

                //using IDbConnection con = new SqlConnection(ConnectionString_Marc);
                //if (con.State == ConnectionState.Closed) con.Open();
                //var objResult = await con.ExecuteAsync(sql: (" INSERT INTO [GRIAMSE].[dbo].[CHECKINOUT] (USERID,CHECKTYPE) VALUES (" + entityCheck.UserId + ",'" + entityCheck.CheckType + "')"), commandType: CommandType.Text);
                //con.Close();

                if (objResult is not null)
                {
                    if (countMarcacion >= 1)
                    {
                        return new ResponseType<string>() { Message = "Ya tiene una marcación registrada", StatusCode = "101", Succeeded = true };
                    }
                    MarcacionColaborador objMarcacionColaborador = new()
                    {
                        IdTurnoColaborador = idturnovalidado,
                        IdLocalidadColaborador = objLocalidad.LocalidadColaboradores.ElementAt(0).Id,
                        MarcacionEntrada = tipoMarcacion == "E" ? marcacionColaborador : null,
                        MarcacionSalida = tipoMarcacion == "S" ? marcacionColaborador : null,
                        EstadoMarcacionEntrada = tipoMarcacion == "E" ? estadoMarcacion : null,
                        SalidaEntrada = tipoMarcacion == "S" ? estadoMarcacion : null,
                        EstadoProcesado = false,
                        UsuarioCreacion = "Admin",
                        FechaCreacion = DateTime.Now,
                    };

                    var objMarcacion = await _repoMarcacionCola.AddAsync(objMarcacionColaborador, cancellationToken);

                    if (objMarcacion is not null)
                    {
                        return new ResponseType<string>() { Message = "Marcación registrada correctamente", StatusCode = "100", Succeeded = true };
                    }
                }

                return new ResponseType<string>() { Message = "No se ha registrado la marcación", Succeeded = true, StatusCode = "101" };
            }
            return new ResponseType<string>() { Message = "Debe marcar desde su dispositivo movil.", Succeeded = true, StatusCode = "101" };

        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<string>(){ Message = CodeMessageResponse.GetMessageByCode("102"), StatusCode = "102", Succeeded = false };
            
        }


    }
}
