﻿using AutoMapper;
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

public record CreateMarcacionAppCommand(CreateMarcacionAppRequest CreateMarcacion,string IdentificacionSesion) : IRequest<ResponseType<CreateMarcacionResponseType>>;
public class CreateMarcacionAppCommandHandler : IRequestHandler<CreateMarcacionAppCommand, ResponseType<CreateMarcacionResponseType>>
{

    private readonly IMarcacion _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateMarcacionAppCommandHandler> _log;

    public CreateMarcacionAppCommandHandler(IMarcacion repository, IMapper mapper, ILogger<CreateMarcacionAppCommandHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<CreateMarcacionResponseType>> Handle(CreateMarcacionAppCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.CreateMarcacionApp(request.CreateMarcacion,request.IdentificacionSesion, cancellationToken);

        return objResult;


    }
}