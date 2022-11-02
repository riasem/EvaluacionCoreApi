using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateSubturnoCliente;

public record CreateSubturnoClienteCommand(CreateSubturnoClienteRequest TurnoRequest) : IRequest<ResponseType<string>>;


public class CreateSubturnoClienteCommandHandler : IRequestHandler<CreateSubturnoClienteCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<SubTurnoCliente> _repoTurnoAsync;
    private readonly IMapper _mapper;

    public CreateSubturnoClienteCommandHandler(IRepositoryAsync<SubTurnoCliente> repository, IMapper mapper)
    {
        _repoTurnoAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateSubturnoClienteCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var objClient = _mapper.Map<SubTurnoCliente>(request.TurnoRequest);

            objClient.Id = Guid.NewGuid();
            objClient.Estado = "A";
            objClient.UsuarioCreacion = "Admin";

            var objResult = await _repoTurnoAsync.AddAsync(objClient, cancellationToken);
            if (objResult is null)
            {
                return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "101", Succeeded = true };
            }
            return new ResponseType<string>() { Data = objResult.Id.ToString(),Message = "Turnos asignados correctamente", StatusCode ="100",Succeeded = true };
        }
        catch (Exception ex)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "102", Succeeded = false };
        }
        
       
    }
}
