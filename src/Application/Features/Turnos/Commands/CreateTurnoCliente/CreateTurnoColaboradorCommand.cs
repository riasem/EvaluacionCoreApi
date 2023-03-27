using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Calendario.Interfaces;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Configuration;

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

        try
        {
            DateTime fechaDesde = request.TurnoRequest.FechaAsignacionDesde;
            DateTime fechaHasta = request.TurnoRequest.FechaAsignacionHasta;
            TimeSpan difFechas = fechaHasta - fechaDesde;
            

            for (int i = 0; i <= difFechas.Days; i++)
            {
                var consulta = await _repoTurnoAsync.ListAsync(new GetTurnosByFechaAsignacionSpec(fechaDesde.AddDays(i)), cancellationToken);

                foreach (var item in request.TurnoRequest.ClienteSubturnos)
                {
                    var filtro = consulta.Where(e => e.FechaAsignacion == fechaDesde.AddDays(i) && e.IdColaborador == item.IdCliente).ToList();

                    if (filtro.Count > 0)
                        return new ResponseType<string>() { Data = null, Message = "Ya existen turnos asignados en el rango de las fechas indicadas", StatusCode = "101", Succeeded = false };

                    var subturno = await _repoTurAsync.GetBySpecAsync(new TurnoByIdPadreSpec(request.TurnoRequest.IdTurno), cancellationToken);

                    //Consulta de Días Feriados
                    var objCliente = await _repoCliente.GetByIdAsync(item.IdCliente);
                    var diasferiados = await _repoCalendario.GetDiasFeriadosByIdentificacion(objCliente.Identificacion, fechaDesde.AddDays(i));
                    if (diasferiados.Data != null)
                    {
                        var uidTurnoFeriado = _config.GetSection("TurnosUid:Feriado").Get<string>();

                        TurnoColaborador objClient = new()
                        {
                            Id = Guid.NewGuid(),
                            Estado = "A",
                            UsuarioCreacion = "SYSTEM",
                            IdTurno = Guid.Parse(uidTurnoFeriado),
                            IdColaborador = item.IdCliente,
                            FechaAsignacion = fechaDesde.AddDays(i)
                        };

                        turnos.Add(objClient);

                    }else
                    {
                        //Asignacion del turno escogido y subturno en caso de escogerlo
                        var diaSemana = (int)fechaDesde.AddDays(i).DayOfWeek;

                        if (diaSemana != 6 && diaSemana != 0)
                        {
                            TurnoColaborador objClient = new()
                            {
                                Id = Guid.NewGuid(),
                                Estado = "A",
                                UsuarioCreacion = "SYSTEM",
                                IdTurno = request.TurnoRequest.IdTurno,
                                IdColaborador = item.IdCliente,
                                FechaAsignacion = fechaDesde.AddDays(i)
                            };

                            turnos.Add(objClient);

                            if (item.Subturnos.Count > 0)
                            {
                                foreach (var item2 in item.Subturnos)
                                {
                                    TurnoColaborador objClient2 = new()
                                    {
                                        Id = Guid.NewGuid(),
                                        Estado = "A",
                                        UsuarioCreacion = "SYSTEM",
                                        IdTurno = item2.IdSubturno,
                                        IdColaborador = item.IdCliente,
                                        FechaAsignacion = fechaDesde.AddDays(i)
                                    };

                                    subturnos.Add(objClient2);
                                }
                            }
                        }
                        else
                        {
                            #region Asignacion de turnos fines de semana
                            var uidLibre = _config.GetSection("TurnosUid:Libre").Get<string>();

                            TurnoColaborador objClient = new()
                            {
                                Id = Guid.NewGuid(),
                                Estado = "A",
                                UsuarioCreacion = "SYSTEM",
                                IdTurno = Guid.Parse(uidLibre),
                                IdColaborador = item.IdCliente,
                                FechaAsignacion = fechaDesde.AddDays(i)
                            };

                            turnos.Add(objClient);

                            #endregion

                        }
                    }
                    //if (subturno != null)
                    //{
                    //    TurnoColaborador objSubturno = new()
                    //    {
                    //        Id = Guid.NewGuid(),
                    //        Estado = "A",
                    //        UsuarioCreacion = "SYSTEM",
                    //        IdTurno = subturno.Id,
                    //        IdColaborador = item.IdCliente,
                    //        FechaAsignacion = fechaDesde.AddDays(i)
                    //    };
                    //    turnos.Add(objSubturno);
                    //}
                }
            }

            var resTurnos = await _repoTurnoAsync.AddRangeAsync(turnos, cancellationToken);

            if (!resTurnos.Any())
                return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación del turno Laboral", StatusCode = "101", Succeeded = false };

            if (subturnos.Any())
            {
                var resSubturnos = await _repoTurnoAsync.AddRangeAsync(subturnos, cancellationToken);

                if (!resSubturnos.Any())
                    return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación del (los) turno(s) de receso", StatusCode = "101", Succeeded = false };
            }

            return new ResponseType<string>() { Data = null, Message = "Turnos asignados correctamente", StatusCode = "100", Succeeded = true };
        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "102", Succeeded = false };
        }
    }
}
