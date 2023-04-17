using AutoMapper;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionApp;

public record CreateMarcacionAppLastCommand(CreateMarcacionAppLastRequest CreateMarcacion, string IdentificacionSesion) : IRequest<ResponseType<CreateMarcacionResponseType>>;

public class CreateMarcacionAppLastCommandHandler : IRequestHandler<CreateMarcacionAppLastCommand, ResponseType<CreateMarcacionResponseType>>
{

    private readonly IMarcacion _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateMarcacionAppLastCommandHandler> _log;

    public CreateMarcacionAppLastCommandHandler(IMarcacion repository, IMapper mapper, ILogger<CreateMarcacionAppLastCommandHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<CreateMarcacionResponseType>> Handle(CreateMarcacionAppLastCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.CreateMarcacionAppLast(request.CreateMarcacion, request.IdentificacionSesion, cancellationToken);

        return objResult;


    }
}