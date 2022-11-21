using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Commands.GetBitacoraMarcacion;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEvaluacionCoreApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MarcacionController : ApiControllerBase
    {
        [HttpGet("GetBitacoraMarcacion")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<BitacoraMarcacionType>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBitacoraMarcacion(string CodUdn, string CodArea, string CodSubcentro, string FechaDesde, string FechaHasta, CancellationToken cancellationToken, string Suscriptor)
        {
            var request = new GetBitacoraMarcacionRequest() { Suscriptor = Suscriptor, CodUdn = CodUdn, CodArea = CodArea, CodSubcentro = CodSubcentro, FechaDesde = FechaDesde, FechaHasta = FechaHasta };

            var objResult = await Mediator.Send(new GetBitacoraMarcacionCommand(request), cancellationToken);
            return Ok(objResult);
        }
    }
}
