using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Calendario.Dto;
using EvaluacionCore.Application.Features.Calendario.Queries.GetFeriadosByCantonAsync;
using EvaluacionCore.Application.Features.Calendario.Queries.GetFeriadosByIdentificacionAsync;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEvaluacionCoreApi.Controllers.v1;

[ApiVersion("1.0")]
public class CalendarioController : ApiControllerBase
{


    [HttpGet("GetFeriadosByIdentificacion")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<List<DiasFeriadoIdentificacionResponseType>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeriadosByIdentificacion(string Identificacion, DateTime Fecha, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetFeriadosByIdentificacionAsync(Identificacion,Fecha), cancellationToken);
        return Ok(objResult);
    }


    [HttpGet("GetFeriadosByCanton")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<List<DiasFeriadosResponseType>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeriadosByCanton(Guid IdCanton, DateTime Fecha, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetFeriadosByCantonAsync(IdCanton, Fecha), cancellationToken);
        return Ok(objResult);
    }
}
