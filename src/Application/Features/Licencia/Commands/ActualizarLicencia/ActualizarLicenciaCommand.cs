using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Licencia.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Licencia.Commands.ActualizarLicencia;

public record ActualizarLicenciaCommand(ActualizarLicenciaRequest requestLicencia,string IdentificacionSesion) : IRequest<ResponseType<string>>;

public class ActualizarLicenciaCommandHandler : IRequestHandler<ActualizarLicenciaCommand, ResponseType<string>>
{
    private readonly ILicencia _repoLicencia;

    public ActualizarLicenciaCommandHandler(ILicencia repoLicencia)
    {
        _repoLicencia = repoLicencia;
    }

    public async Task<ResponseType<string>> Handle(ActualizarLicenciaCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repoLicencia.ActualizarLicencia(request.requestLicencia,request.IdentificacionSesion);

        return objResult;
    }
}
