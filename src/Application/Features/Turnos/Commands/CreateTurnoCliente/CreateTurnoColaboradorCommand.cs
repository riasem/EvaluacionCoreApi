using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;

public record CreateTurnoColaboradorCommand(CreateTurnoColaboradorRequest TurnoRequest) : IRequest<ResponseType<string>>;


public class CreateTurnoColaboradorCommandHandler : IRequestHandler<CreateTurnoColaboradorCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoAsync;
    private readonly IMapper _mapper;

    public CreateTurnoColaboradorCommandHandler(IRepositoryAsync<TurnoColaborador> repository, IMapper mapper)
    {
        _repoTurnoAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateTurnoColaboradorCommand request, CancellationToken cancellationToken)
    {

        try
        {
            foreach (var item in request.TurnoRequest.ClienteSubturnos)
            {
                TurnoColaborador objClient = new()
                {
                    Id = Guid.NewGuid(),
                    Estado = "A",
                    UsuarioCreacion = "Admin",
                    IdTurno = request.TurnoRequest.IdTurno,
                    IdColaborador = item.IdCliente
                };

                var objResult = await _repoTurnoAsync.AddAsync(objClient, cancellationToken);

                if (objResult is null)
                {
                    return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "101", Succeeded = false };
                }

                foreach (var item2 in item.Subturnos)
                {
                    TurnoColaborador objClient2 = new()
                    {
                        Id = Guid.NewGuid(),
                        Estado = "A",
                        UsuarioCreacion = "Admin",
                        IdTurno = item2.IdSubturno,
                        IdColaborador = item.IdCliente,
                        FechaAsignacion = item2.FechaAsignacion
                    };

                    var objResult2 = await _repoTurnoAsync.AddAsync(objClient, cancellationToken);

                    if (objResult2 is null)
                    {
                        return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "101", Succeeded = false };
                    }
                }
            }

            
            return new ResponseType<string>() { Data = /*objResult.Id.ToString()*/ null,Message = "Turnos asignados correctamente", StatusCode ="100",Succeeded = true };
        }
        catch (Exception ex)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "102", Succeeded = false };
        }
        
       
    }
}
