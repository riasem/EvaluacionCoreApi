using AutoMapper;
using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateCabeceraLog;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;

public record CargaMarcacionesExcelCommand(List<CargaMarcacionesExcelRequest> marcacionesExcel, string IdentificacionSesion) : IRequest<ResponseType<string>>;
public class CargaMarcacionesExcelCommandHandler : IRequestHandler<CargaMarcacionesExcelCommand, ResponseType<string>>
{
    private readonly IMarcacion _repository;

    private readonly ILogger<CargaMarcacionesExcelCommandHandler> _log;

    public CargaMarcacionesExcelCommandHandler(IMarcacion repository,  ILogger<CargaMarcacionesExcelCommandHandler> log)
    {
        _repository = repository;
        _log = log;
    }

    public async Task<ResponseType<string>> Handle(CargaMarcacionesExcelCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.CargarMarcacionesExcel(request.marcacionesExcel,request.IdentificacionSesion, cancellationToken);

        return objResult;

    }
}
