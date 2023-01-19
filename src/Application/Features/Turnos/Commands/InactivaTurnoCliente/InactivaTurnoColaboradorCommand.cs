using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.InactivaTurnoColaborador;

public record InactivaTurnoColaboradorCommand(List<InactivaTurnoColaboradorRequest> TurnoRequest) : IRequest<ResponseType<string>>;


public class InactivaTurnoColaboradorCommandHandler : IRequestHandler<InactivaTurnoColaboradorCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoAsync;
    private readonly IMapper _mapper;

    public InactivaTurnoColaboradorCommandHandler(IRepositoryAsync<TurnoColaborador> repository, IMapper mapper)
    {
        _repoTurnoAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(InactivaTurnoColaboradorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var it in request.TurnoRequest)
            {
                if (it.Selected)
                {
                    if (it.IdAsignacion == null)
                    {
                        TurnoColaborador objClient = new()
                        {
                            Id = Guid.NewGuid(), //id turno colaborador
                            IdTurno = it.IdTurno,
                            IdColaborador = it.IdColaborador,
                            Estado = "A",
                            UsuarioCreacion = "SYSTEM",
                            FechaAsignacion = it.FechaAsignacion,
                            FechaCreacion = DateTime.Now
                        };

                        await _repoTurnoAsync.AddAsync(objClient, cancellationToken);
                    }

                    foreach (var item in it.Subturnos)
                    {
                        if (item.IdTurnoAsignado == null && item.Selected)
                        {
                            TurnoColaborador objClient = new()
                            {
                                Id = Guid.NewGuid(), //id turno colaborador
                                IdTurno = item.Id,
                                IdColaborador = it.IdColaborador,
                                Estado = "A",
                                UsuarioCreacion = "SYSTEM",
                                FechaAsignacion = it.FechaAsignacion,
                                FechaCreacion = DateTime.Now
                            };

                            await _repoTurnoAsync.AddAsync(objClient, cancellationToken);
                        }

                        if (item.IdTurnoAsignado != null)
                        {
                            TurnoColaborador objClient = new()
                            {
                                Id = item.IdTurnoAsignado,
                                Estado = item.Selected ? "A" : "I",
                                FechaAsignacion = it.FechaAsignacion,
                                FechaModificacion = DateTime.Now,
                                UsuarioModificacion = "SYSTEM",
                                IdTurno = item.Id,
                                IdColaborador = it.IdColaborador
                            };

                            await _repoTurnoAsync.UpdateAsync(objClient, cancellationToken);
                        }
                    }
                }
                else
                {
                    TurnoColaborador objClient = new()
                    {
                        Id = it.IdAsignacion,
                        Estado = "I",
                        IdColaborador = it.IdColaborador,
                        IdTurno = it.IdTurno,

                        FechaModificacion = DateTime.Now,
                        UsuarioModificacion = "SYSTEM",
                        FechaAsignacion = it.FechaAsignacion
                    };

                    await _repoTurnoAsync.UpdateAsync(objClient, cancellationToken);

                    foreach (var item in it.Subturnos)
                    {
                        TurnoColaborador objClient2 = new()
                        {
                            Id = item.IdTurnoAsignado,
                            Estado = "I",
                            IdTurno = item.Id,
                            IdColaborador = it.IdColaborador,
                            FechaModificacion = DateTime.Now,
                            UsuarioModificacion = "SYSTEM",
                            FechaAsignacion = it.FechaAsignacion,
                        };

                        await _repoTurnoAsync.UpdateAsync(objClient2, cancellationToken);
                    }
                }
            }

            return new ResponseType<string>() { Data = /*objResult.Id.ToString()*/ null, Message = "Turnos actualizados correctamente", StatusCode = "100", Succeeded = true };
        }
        catch (Exception ex)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "102", Succeeded = false };
        }


    }
}
