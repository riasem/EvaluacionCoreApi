using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Subturnos.Specifications;
using EvaluacionCore.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Subturnos.Commands.CreateSubturno;

public record CreateSubturnoCommand(CreateSubturnoRequest SubturnoRequest) : IRequest<ResponseType<string>>;


public class CreateSubturnoCommandHandler : IRequestHandler<CreateSubturnoCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<SubTurno> _repoSubturnoAsync;
    private readonly IMapper _mapper;

    public CreateSubturnoCommandHandler(IRepositoryAsync<SubTurno> repository, 
        IConfiguration config, IMapper mapper)
    {
        _repoSubturnoAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateSubturnoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            SubTurno subSubturno = new();
            var objClient = _mapper.Map<SubTurno>(request.SubturnoRequest);
            var objConsult = await _repoSubturnoAsync.FirstOrDefaultAsync(new SubturnoByCodigoSpec(objClient.CodigoSubturno), cancellationToken);
            objClient.Id = Guid.NewGuid();
            objClient.Estado = "A";

            //Caso donde el codigo del Subturno ya existe
            if (objConsult is not null)
            {
                return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar, Subturno ya existente", StatusCode = "101", Succeeded = true };
            }
            var objResult = await _repoSubturnoAsync.AddAsync(objClient, cancellationToken);

            //Caso donde no se guarda correctamente el Subturno
            if (objResult is null)
            {
                return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Ocurrió un error al registrar el Subturno", StatusCode = "102", Succeeded = false };

            }

            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Subturno registrado exitosamente", StatusCode = "100", Succeeded = true };
        }
        catch (Exception e)
        {
            return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar el Subturno ", StatusCode = "102", Succeeded = false };
        }
        
    }
}
