using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateCabeceraLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Interfaces;

public interface IMarcacionOffline
{
    Task<ResponseType<string>> CreateCabeceraLogOffline(List<CreateCabeceraLogRequest> Request, string IdentificacionSesion, CancellationToken cancellationToken);
}
