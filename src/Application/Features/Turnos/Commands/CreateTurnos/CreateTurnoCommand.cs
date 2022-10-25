using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Clients.Commands.CreateTurno;

public record CreateTurnoCommand(CreateTurnoRequest TurnoRequest) : IRequest<ResponseType<string>>;


public class CreateTurnoCommandHandler : IRequestHandler<CreateTurnoCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<Turno> _repoTurnoAsync;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly string UrlBaseApiAuth = "";
    private readonly string UrlBaseApiUtils = "";
    private readonly string UrlBaseApiEcommerce = "";
    private string nombreEnpoint = "";
    private string uriEnpoint = "";

    public CreateTurnoCommandHandler(IRepositoryAsync<Turno> repository, IRepositoryAsync<Turno> repoProspAsync,
        IConfiguration config, IMapper mapper)
    {
        _repoTurnoAsync = repository;
        _mapper = mapper;
        _config = config;
    }

    public async Task<ResponseType<string>> Handle(CreateTurnoCommand request, CancellationToken cancellationToken)
    {
        var objClient = _mapper.Map<Turno>(request.TurnoRequest);

        objClient.Id = Guid.NewGuid();
        objClient.Estado = "A";

        
        var objResult = await _repoTurnoAsync.AddAsync(objClient, cancellationToken);
        if (objResult is null)
        {
            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Ocurrió un error al registrar el turno", StatusCode = "000", Succeeded = true };

        }
       
        return new ResponseType<string>() { Data = objResult.Id.ToString(),Message = "Turno registrado exitosamente", StatusCode ="000",Succeeded = true };
    }
}
