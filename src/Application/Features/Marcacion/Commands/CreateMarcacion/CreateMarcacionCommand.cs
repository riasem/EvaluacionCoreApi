using AutoMapper;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;

public record CreateMarcacionCommand(CreateMarcacionRequest CreateMarcacion) : IRequest<ResponseType<string>>;

public class CreateMarcacionCommandHandler : IRequestHandler<CreateMarcacionCommand, ResponseType<string>>
{

    private readonly IMarcacion _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateMarcacionCommandHandler> _log;

    public CreateMarcacionCommandHandler(IMarcacion repository, IMapper mapper, ILogger<CreateMarcacionCommandHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<string>> Handle(CreateMarcacionCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.CreateMarcacion(request.CreateMarcacion, cancellationToken);

        return objResult;


    }
}
