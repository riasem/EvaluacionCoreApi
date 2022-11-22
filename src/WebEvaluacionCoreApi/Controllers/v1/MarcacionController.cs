using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Commands.GetBitacoraMarcacion;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEvaluacionCoreApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MarcacionController : ApiControllerBase
    {
        /// <summary>
        /// Consulta información de la bitácora de marcaciones
        /// </summary>
        /// <param name="CodUdn"></param>
        /// <param name="CodArea"></param>
        /// <param name="CodSubcentro"></param>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Suscriptor"></param>
        /// <returns></returns>
        [HttpGet("GetBitacoraMarcacion")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<BitacoraMarcacionType>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBitacoraMarcacion(string CodUdn, string CodArea, string CodSubcentro, string FechaDesde, string FechaHasta, CancellationToken cancellationToken, string Suscriptor)
        {
            var request = new GetBitacoraMarcacionRequest() { Suscriptor = Suscriptor, CodUdn = CodUdn, CodArea = CodArea, CodSubcentro = CodSubcentro, FechaDesde = FechaDesde, FechaHasta = FechaHasta };

            var objResult = await Mediator.Send(new GetBitacoraMarcacionCommand(request), cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Consulta información de los combos para la opción bitácora de marcaciones
        /// </summary>
        /// <param name="Tipo"></param>
        /// <param name="Codigo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetComboBitacoraMarcacion")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<BitacoraMarcacionType>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComboBitacoraMarcacion(string Tipo, string Codigo, CancellationToken cancellationToken)
        {
            var request = new GetComboBitacoraMarcacionRequest() { Tipo = Tipo, Codigo = Codigo };

            var objResult = await Mediator.Send(new GetComboBitacoraMarcacionCommand(request), cancellationToken);
            return Ok(objResult);
        }
    }
}