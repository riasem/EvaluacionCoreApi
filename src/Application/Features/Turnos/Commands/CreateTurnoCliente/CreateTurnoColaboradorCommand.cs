using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Calendario.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Specifications;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;

public record CreateTurnoColaboradorCommand(CreateTurnoColaboradorRequest TurnoRequest) : IRequest<ResponseType<string>>;

public class CreateTurnoColaboradorCommandHandler : IRequestHandler<CreateTurnoColaboradorCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoAsync;
    private readonly IRepositoryAsync<Turno> _repoTurAsync;
    private readonly IRepositoryAsync<Cliente> _repoCliente;
    private readonly ICalendario _repoCalendario;
    private readonly IConfiguration _config;


    public CreateTurnoColaboradorCommandHandler(IRepositoryAsync<TurnoColaborador> repository, IRepositoryAsync<Turno> repositoryTur, ICalendario repoCalendario, IRepositoryAsync<Cliente> repoCliente,
                   IConfiguration config)
    {
        _repoTurnoAsync = repository;
        _repoTurAsync = repositoryTur;
        _repoCalendario = repoCalendario;
        _repoCliente = repoCliente;
        _config = config;
    }

    public async Task<ResponseType<string>> Handle(CreateTurnoColaboradorCommand request, CancellationToken cancellationToken)
    {
        List<TurnoColaborador> turnos = new();
        List<TurnoColaborador> subturnos = new();
        bool subturnoAsignado = false;
        string ConnectionString_Marc;
        List<string> cedulasColaboradores = new();
        DateTime ayer = DateTime.Now.AddDays(-1);
        DateTime fechaLimite = DateTime.Now.AddDays(-1);
        // Agregado por IC. Para ejecutar la conexión a BD GRIAMSE
        ConnectionString_Marc = _config.GetConnectionString("Bd_Marcaciones_GRIAMSE");
        string queryString = "";
        SqlCommand command;
        //
        try
        {
            DateTime fechaDesde = request.TurnoRequest.FechaAsignacionDesde;
            DateTime fechaHasta = request.TurnoRequest.FechaAsignacionHasta;
            TimeSpan difFechas = fechaHasta - fechaDesde;
            String identificacionAprobador = request.TurnoRequest.IdentificacionAprobador;

            // Procesa cada una de las fechas comprendidas entre la fecha desde y la fecha hasta
            for (int i = 0; i <= difFechas.Days; i++)
            {
                // Obtiene todos los turnos activos de todos los colaboradores en la fecha dada
                var consulta = await _repoTurnoAsync.ListAsync(new GetTurnosByFechaAsignacionSpec(fechaDesde.AddDays(i)), cancellationToken);

                // Itera cada uno de los colaboradores a los que se les esta asignando Turno en la fecha dada
                foreach (var item in request.TurnoRequest.ClienteSubturnos)
                {
                    // Obtiene todos los turnos activos que tiene el colaborador en la fecha dada
                    var filtro = consulta.Where(e => e.FechaAsignacion == fechaDesde.AddDays(i) && e.IdColaborador == item.IdCliente && e.Estado == "A").ToList();

                    // Si el colaborador tiene turnos activos en la fecha dada
                    if (filtro.Count > 0)
                    {
                        // Actualiza con estado Inactivo todos los turnos activos que tiene el colaborador, dejando de estar vigentes
                        foreach (var item2 in filtro)
                        {
                            item2.Estado = "I";
                            item2.UsuarioModificacion = identificacionAprobador;
                            item2.FechaModificacion = DateTime.Now;
                            await _repoTurnoAsync.UpdateAsync(item2);
                        }
                    }

                    // Obtiene los subturnos relacionados al Turno de labores Principal (Padre)
                    var subturno = await _repoTurAsync.GetBySpecAsync(new TurnoByIdPadreSpec(request.TurnoRequest.IdTurno), cancellationToken);

                    // Si el Turno de labores Principal tiene subturnos relacionados
                    if (subturno is not null)
                    {
                        // Crea el Subturno para el colaborador en la fecha dada
                        subturnoAsignado = true;
                        TurnoColaborador objClient3 = new()
                        {
                            Id = Guid.NewGuid(),
                            Estado = "A",
                            UsuarioCreacion = identificacionAprobador,
                            FechaCreacion = DateTime.Now,
                            IdTurno = subturno.Id,
                            IdColaborador = item.IdCliente,
                            FechaAsignacion = fechaDesde.AddDays(i),
                            HorasExtraordinariasAprobadas = 0,
                            ComentariosAprobacion = null
                        };
                        subturnos.Add(objClient3);
                    }

                    // Obtiene los datos del colaborador y establece las horas de sobretiempo aprobadas con su comentario respectivo si tuviere
                    var objCliente = await _repoCliente.GetByIdAsync(item.IdCliente);
                    // Agregado por IC. 4/04/2024 
                    // Alimenta una lista de las cedulas de los colaboradores a los que se les está
                    // asignando un turno. Esta lista de valores será usada más adelante para 
                    // mandar a reprocesar en el rango de fechas de asignación del turno,
                    // siempre y cuando no sean fechas a futuro.
                    if (!cedulasColaboradores.Contains(objCliente.Identificacion))
                    {
                        cedulasColaboradores.Add(objCliente.Identificacion);
                    }
                    //
                    var horasExtraordinariasAprobadas = item.HorasSobretiempoAprobadas;
                    var comentariosAprobacion = item.ComentariosAprobacion;

                    // Consulta si en la Localidad Principal de marcacion del colaborador, la fecha dada es un Día Feriado Nacional o Feriado Local
                    var diasferiados = await _repoCalendario.GetDiasFeriadosByIdentificacion(objCliente.Identificacion, fechaDesde.AddDays(i));

                    // Si no hay diferencia de dias entre la fecha desde y fecha hasta, es decir fecha desde y fecha hasta son iguales (1 solo dia de asignación del Turno)
                    if (difFechas.Days == 0)
                    {
                        // Crea el Turno para el colaborador en la fecha dada
                        TurnoColaborador objClient = new()
                        {
                            Id = Guid.NewGuid(),
                            Estado = "A",
                            UsuarioCreacion = identificacionAprobador,
                            FechaCreacion = DateTime.Now,
                            IdTurno = request.TurnoRequest.IdTurno,
                            IdColaborador = item.IdCliente,
                            FechaAsignacion = fechaDesde.AddDays(i),
                            HorasExtraordinariasAprobadas = horasExtraordinariasAprobadas,
                            ComentariosAprobacion = comentariosAprobacion
                        };
                        turnos.Add(objClient);

                        // Si tiene subturnos asignados por el usuario aprobador y no se han asignado aun subturnos
                        if (item.Subturnos.Count > 0 && subturnoAsignado == false)
                        {
                            // Por cada Subturno asignado al colaborador en la fecha dada
                            foreach (var item2 in item.Subturnos)
                            {
                                // Crea el Subturno para el colaborador en la fecha dada
                                TurnoColaborador objClient2 = new()
                                {
                                    Id = Guid.NewGuid(),
                                    Estado = "A",
                                    UsuarioCreacion = identificacionAprobador,
                                    FechaCreacion = DateTime.Now,
                                    IdTurno = item2.IdSubturno,
                                    IdColaborador = item.IdCliente,
                                    FechaAsignacion = fechaDesde.AddDays(i),
                                    HorasExtraordinariasAprobadas = 0,
                                    ComentariosAprobacion = null
                                };
                                subturnos.Add(objClient2);
                            }
                        }
                    }

                    // Si hay diferencia de dias entre la fecha desde y fecha hasta, es decir hay varios dias entre fecha desde y fecha hasta (varios dias de asignación del Turno)
                    else
                    {
                        // Si la fecha dada es un dia Feriado Nacional o Feriado Local
                        if (diasferiados.Data != null)
                        {
                            // Obtiene el GUID del Turno de Feriado
                            var uidTurnoFeriado = _config.GetSection("TurnosUid:Feriado").Get<string>();

                            // Crea el Turno Feriado para el colaborador en la fecha dada
                            TurnoColaborador objClient = new()
                            {
                                Id = Guid.NewGuid(),
                                Estado = "A",
                                UsuarioCreacion = identificacionAprobador,
                                FechaCreacion = DateTime.Now,
                                IdTurno = Guid.Parse(uidTurnoFeriado),
                                IdColaborador = item.IdCliente,
                                FechaAsignacion = fechaDesde.AddDays(i),
                                HorasExtraordinariasAprobadas = 0,
                                ComentariosAprobacion = null
                            };
                            turnos.Add(objClient);

                        }

                        // Si la fecha dada NO es un dia Feriado Nacional o Feriado Local, es decir es un dia normal de trabajo o de descanso
                        else
                        {
                            // Obtiene el dia de la semana de la fecha dada
                            var diaSemana = (int)fechaDesde.AddDays(i).DayOfWeek;

                            // Valida si la fecha dada no es ni Sabado ni Domingo, es decir es un dia entre Lunes y Viernes
                            if (diaSemana != 6 && diaSemana != 0)
                            {
                                // Crea el Turno asignado para el colaborador en la fecha dada
                                TurnoColaborador objClient = new()
                                {
                                    Id = Guid.NewGuid(),
                                    Estado = "A",
                                    UsuarioCreacion = identificacionAprobador,
                                    FechaCreacion = DateTime.Now,
                                    IdTurno = request.TurnoRequest.IdTurno,
                                    IdColaborador = item.IdCliente,
                                    FechaAsignacion = fechaDesde.AddDays(i),
                                    HorasExtraordinariasAprobadas = horasExtraordinariasAprobadas,
                                    ComentariosAprobacion = comentariosAprobacion
                                };
                                turnos.Add(objClient);

                                // Si tiene subturnos asignados por el usuario aprobador y no se han asignado aun subturnos
                                if (item.Subturnos.Count > 0 && subturnoAsignado == false)
                                {
                                    // Por cada Subturno asignado al colaborador en la fecha dada
                                    foreach (var item2 in item.Subturnos)
                                    {
                                        // Crea el Subturno para el colaborador en la fecha dada
                                        TurnoColaborador objClient2 = new()
                                        {
                                            Id = Guid.NewGuid(),
                                            Estado = "A",
                                            UsuarioCreacion = identificacionAprobador,
                                            FechaCreacion = DateTime.Now,
                                            IdTurno = item2.IdSubturno,
                                            IdColaborador = item.IdCliente,
                                            FechaAsignacion = fechaDesde.AddDays(i),
                                            HorasExtraordinariasAprobadas = 0,
                                            ComentariosAprobacion = null
                                        };
                                        subturnos.Add(objClient2);
                                    }
                                }
                            }

                            // Si la fecha dada es un fin de semana, es decir un dia de descanso, Sabado o Domingo
                            else
                            {
                                #region Asignacion de turnos fines de semana

                                // Obtiene el GUID del Turno de Dia Libre o Dia de Descanso
                                var uidLibre = _config.GetSection("TurnosUid:Libre").Get<string>();

                                // Crea el Turno Libre o de Descanso para el colaborador en la fecha dada
                                TurnoColaborador objClient = new()
                                {
                                    Id = Guid.NewGuid(),
                                    Estado = "A",
                                    UsuarioCreacion = identificacionAprobador,
                                    FechaCreacion = DateTime.Now,
                                    IdTurno = Guid.Parse(uidLibre),
                                    IdColaborador = item.IdCliente,
                                    FechaAsignacion = fechaDesde.AddDays(i),
                                    HorasExtraordinariasAprobadas = 0,
                                    ComentariosAprobacion = null
                                };
                                turnos.Add(objClient);

                                #endregion

                            }
                        }
                    }
                    

                }
            }

            // Registra todos los turnos asignados a los colaboradores
            var resTurnos = await _repoTurnoAsync.AddRangeAsync(turnos, cancellationToken);

            if (!resTurnos.Any())
                return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación del turno Laboral", StatusCode = "101", Succeeded = false };

            // Si tiene subturnos asignados a los colaboradores
            if (subturnos.Any())
            {
                // Registra todos los Subturnos asignados a los colaboradores
                var resSubturnos = await _repoTurnoAsync.AddRangeAsync(subturnos, cancellationToken);

                if (!resSubturnos.Any())
                    return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación del (los) turno(s) de receso", StatusCode = "101", Succeeded = false };
            }


            #region Reprocesamiento de marcaciones
            // Evalúa si la fecha desde es menor a la fecha de hoy,
            if (fechaDesde < DateTime.Now.Date)
            {
                // Ajusta la fechaHasta como máximo hasta el día de ayer.
                if (fechaHasta <= ayer) {
                    fechaLimite = fechaHasta;
                } else
                {
                    fechaLimite = ayer;
                }
                // Recorre la lista de cedulas de colaboradores a los que se les asignó el turno
                foreach (var colaborador in cedulasColaboradores)
                {
                    queryString = "EXEC [dbo].[EAPP_SP_REPROCESA_MARCACIONES] NULL, NULL, NULL, '" + fechaDesde.ToString("yyyy/MM/dd HH:mm:ss") + "' , '" + fechaLimite.ToString("yyyy/MM/dd HH:mm:ss") + "',  '" + colaborador + "';";
                    using (SqlConnection connection = new SqlConnection(ConnectionString_Marc))
                    {
                        command = new SqlCommand(queryString, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                    }
                }
                //
            }
            //
            #endregion
            
            return new ResponseType<string>() { Data = null, Message = "Turnos asignados correctamente", StatusCode = "100", Succeeded = true };
        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "102", Succeeded = false };
        }
    }
}
