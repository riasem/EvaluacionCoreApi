using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Subturnos.Commands.CreateSubturno;
using EvaluacionCore.Application.Features.Subturnos.Queries.GetSubturnoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebEvaluacionCoreApi.Controllers;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class SubturnosController : ApiControllerBase
{

    /// <summary>
    /// Crea un nuevo Subturno
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna el codigo  uid del nuevo Subturno </returns>
    /// <remarks>
    /// Ejemplo request:
    ///
    ///{
    ///  "idTipoSubturno": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///  "codigoSubturno": "string",
    ///  "descripcion": "string",
    ///  "entrada": "2022-10-26T20:06:46.379Z",
    ///  "salida": "2022-10-26T20:06:46.379Z",
    ///  "margenEntrada": "2022-10-26T20:06:46.379Z",
    ///  "margenSalida": "2022-10-26T20:06:46.379Z",
    ///  "totalHoras": "string",
    ///  "estado": "string",
    ///  "usuario": "string"
    ///}
    ///
    /// </remarks>
    /// <response code="201">Subturno creado</response>
    /// <response code="400">Si el registro es nulo</response>
    [HttpPost("CreateSubturno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateSubturno([FromBody] CreateSubturnoRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateSubturnoCommand(request), cancellationToken);
        return Ok(objResult);
        
    }

    /// <summary>
    /// Retorna el listado de Subturnos
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetSubturno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetSubturnos(CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetSubturnosAsyncQuery(), cancellationToken);
        return Ok(objResult);
    }

}
