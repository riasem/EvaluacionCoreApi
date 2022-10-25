using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Locacions.Commands.CreateLocacion;
using EvaluacionCore.Application.Features.Locacions.Commands.DeleteLocation;
using EvaluacionCore.Application.Features.Locacions.Commands.UpdateLocacion;
using EvaluacionCore.Application.Features.Locacions.Dto;
using EvaluacionCore.Application.Features.Locacions.Queries.GetLocacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebEvaluacionCoreApi.Controllers.v1;

[ApiVersion("1.0")]
public class LocacionController : ApiControllerBase
{
    /// <summary>
    /// Crear una Nueva locación de una empresa
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("CreateLocacion")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateLocacion([FromBody] CreateLocacionRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateLocacionCommand(request), cancellationToken);
        return Ok(objResult);

    }
    /// <summary>
    /// Obtener el listado de las locacilidades (opcional el id)
    /// </summary>
    /// <param name="IdLocacion"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetLocacion")]
    [ProducesResponseType(typeof(ResponseType<LocacionType>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> GetLocacion(string? IdLocacion , CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetLocacionAsyncQueries(IdLocacion), cancellationToken);
        return Ok(objResult);

    }
    /// <summary>
    /// Actualiza la información de la localización
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("UpdateLocacion")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateLocacion([FromBody] UpdateLocacionRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new UpdateLocacionCommand(request), cancellationToken);
        return Ok(objResult);

    }

    [HttpGet("DeleteLocacion")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteLocacion(string IdLocacion, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new DeleteLocacionCommand(IdLocacion), cancellationToken);
        return Ok(objResult);

    }


}
