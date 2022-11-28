using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebEvaluacionCoreApi.Controllers;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class EvaluacionController : ApiControllerBase
{

    /// <summary>
    /// Evaluador de Asistencias
    /// </summary>
    /// <param name="identificacion"></param>
    /// <param name="fechaDesde"></param>
    /// <param name="fechaHasta"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Capacidad que realiza la evaluación de asistencias.</returns>
    /// <response code="201">Evaluación Realizada</response>
    /// <response code="400">Ocurrió un error al realizar la evaluación</response>
    [HttpGet("EvaluarAsistencias")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> CreateTurno(DateTime? fechaDesde, DateTime? fechaHasta, CancellationToken cancellationToken, string identificacion = "0")
    {
        if (identificacion == "0")
        {
            identificacion = "";
        }
        var objResult = await Mediator.Send(new EvaluarAsistenciasCommand(identificacion, fechaDesde, fechaHasta), cancellationToken);
        return Ok(objResult);
        
    }
}
