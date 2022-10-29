using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateLocalidadTurnoCliente;

public record CreateLocalidadTurnoClienteCommand(CreateLocalidadTurnoClienteRequest TurnoRequest) : IRequest<ResponseType<string>>;


public class CreateLocalidadTurnoClienteCommandHandler : IRequestHandler<CreateLocalidadTurnoClienteCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<SubTurnoCliente> _repoTurnoAsync;
    private readonly IMapper _mapper;

    public CreateLocalidadTurnoClienteCommandHandler(IRepositoryAsync<SubTurnoCliente> repository, IMapper mapper)
    {
        _repoTurnoAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateLocalidadTurnoClienteCommand request, CancellationToken cancellationToken)
    {
        var objClient = _mapper.Map<SubTurnoCliente>(request.TurnoRequest);

        objClient.Id = Guid.NewGuid();
        objClient.Estado = "A";
        objClient.UsuarioCreacion = "Admin";

        try
        {
            var objResult = await _repoTurnoAsync.AddAsync(objClient, cancellationToken);
            if (objResult is null)
            {
                return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Ocurrió un error al registrar la asignación del turno", StatusCode = "000", Succeeded = true };
            }
            return new ResponseType<string>() { Data = objResult.Id.ToString(),Message = "Turno asignado exitosamente", StatusCode ="000",Succeeded = true };
        }
        catch (Exception ex)
        {

            throw;
        }
        
       
    }
}
