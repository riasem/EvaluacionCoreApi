using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesTxt;

public record CargaMarcacionesTxtCommand(List<CargaMarcacionesTxtRequest> marcacionesTxt, string IdentificacionSesion) : IRequest<ResponseType<string>>;

public  class CargaMarcacionesTxtCommandHandler : IRequestHandler<CargaMarcacionesTxtCommand, ResponseType<string>>
{
    private readonly IMarcacion _repository;

    private readonly ILogger<CargaMarcacionesTxtCommandHandler> _log;

    public CargaMarcacionesTxtCommandHandler(IMarcacion repository, ILogger<CargaMarcacionesTxtCommandHandler> log)
    {
        _repository = repository;
        _log = log;
    }

    public async Task<ResponseType<string>> Handle(CargaMarcacionesTxtCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.CargarMarcacionesTxt(request.marcacionesTxt, request.IdentificacionSesion, cancellationToken);

        return objResult;

    }
}
