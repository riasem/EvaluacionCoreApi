using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.AlertaTurnos.Commands.AlertaTurnosNoAsignados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEvaluacionCoreApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AlertaController : ApiControllerBase
    {
        /// <summary>
        /// Proceso batch que genera recordatorios de turnos no asignados a los jefes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GenerarRecordatorios")]
        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseType<List<string>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerarRecordatorios(CancellationToken cancellationToken)
        {
            var objResult = await Mediator.Send(new RecordarTurnosNoAsignadosCommand(), cancellationToken);
            return Ok(objResult);
        }

    }
}