using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EvaluacionCore.Application.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Interfaces;

public interface IMarcacion
{
    Task<ResponseType<string>> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken);

}
