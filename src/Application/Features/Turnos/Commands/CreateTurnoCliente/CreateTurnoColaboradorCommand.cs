using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;

public record CreateTurnoColaboradorCommand(CreateTurnoColaboradorRequest TurnoRequest) : IRequest<ResponseType<string>>;

public class CreateTurnoColaboradorCommandHandler : IRequestHandler<CreateTurnoColaboradorCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoAsync;

    public CreateTurnoColaboradorCommandHandler(IRepositoryAsync<TurnoColaborador> repository)
    {
        _repoTurnoAsync = repository;
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
