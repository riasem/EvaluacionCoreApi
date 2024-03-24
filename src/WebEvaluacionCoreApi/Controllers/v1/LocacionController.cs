using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Locacions.Dto;
using EvaluacionCore.Application.Features.Locacions.Queries.ConsultaLocalidadesXCoordinador;
using EvaluacionCore.Application.Features.Localidads.Commands.AsignarLocalidadCliente;
using EvaluacionCore.Application.Features.Localidads.Commands.CreateLocalidad;
using EvaluacionCore.Application.Features.Localidads.Commands.DeleteLocation;
using EvaluacionCore.Application.Features.Localidads.Commands.UpdateLocalidad;
using EvaluacionCore.Application.Features.Localidads.Dto;
using EvaluacionCore.Application.Features.Localidads.Queries.GetLocalidad;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Application.Features.Turnos.Queries.ConsultaJefaturasXCoordinador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
    /// <param name="Identificacion"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetLocalidad")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<LocalidadType>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> GetLocalidad(string? IdLocalidad, string? Identificacion, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetLocalidadAsyncQueries(IdLocalidad, Identificacion), cancellationToken);
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

    /// <summary>
    /// Obtener el listado de las locacilidades que tienen asignadas como principal, los colaboradores que pertenecen al lineaje de un jefe
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("ConsultaLocalidadesXCoordinador")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<List<LocalidadXColaboradorType>>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConsultaLocalidadesXCoordinador(CancellationToken cancellationToken)
    {
        var IdentificacionSesion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;

        var query = new ConsultaLocalidadesXCoordinadorAsyncQueries(IdentificacionSesion);
        var objResult = await Mediator.Send(query, cancellationToken);
        return Ok(objResult);
    }
}
