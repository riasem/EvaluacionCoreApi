using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Localidads.Commands.AsignarLocalidadCliente;
using EvaluacionCore.Application.Features.Localidads.Commands.CreateLocalidad;
using EvaluacionCore.Application.Features.Localidads.Commands.DeleteLocation;
using EvaluacionCore.Application.Features.Localidads.Commands.UpdateLocalidad;
using EvaluacionCore.Application.Features.Localidads.Dto;
using EvaluacionCore.Application.Features.Localidads.Queries.GetLocalidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEvaluacionCoreApi.Controllers.v1;

[ApiVersion("1.0")]
public class LocalidadController : ApiControllerBase
{
    /// <summary>
    /// Crear una Nueva locación de una empresa
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("CreateLocalidad")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> CreateLocalidad([FromBody] CreateLocalidadRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateLocalidadCommand(request), cancellationToken);
        return Ok(objResult);

    }
    /// <summary>
    /// Obtener el listado de las locacilidades (opcional el id)
    /// </summary>
    /// <param name="IdLocalidad"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetLocalidad")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<LocalidadType>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> GetLocalidad(string? IdLocalidad , CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetLocalidadAsyncQueries(IdLocalidad), cancellationToken);
        return Ok(objResult);

    }
    /// <summary>
    /// Actualiza la información de la localización
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("UpdateLocalidad")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> UpdateLocalidad([FromBody] UpdateLocalidadRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new UpdateLocalidadCommand(request), cancellationToken);
        return Ok(objResult);

    }
    /// <summary>
    /// Eliminar logicamente una Locación
    /// </summary>
    /// <param name="IdLocalidad"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("DeleteLocalidad")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> DeleteLocalidad(string IdLocalidad, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new DeleteLocalidadCommand(IdLocalidad), cancellationToken);
        return Ok(objResult);

    }


    [HttpPost("AsignaLocalidadCliente")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> AsignaLocalidadCliente([FromBody] AsignarLocalidadClienteRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new AsignarLocalidadClienteCommand(request), cancellationToken);
        return Ok(objResult);

    }


}
