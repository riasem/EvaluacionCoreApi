using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateSubturnoCliente;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurno;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoSubTurno;
using EvaluacionCore.Application.Features.Turnos.Queries.GetTurnoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebEvaluacionCoreApi.Controllers;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class TurnosController : ApiControllerBase
{

    /// <summary>
    /// Crea un nuevo Turno
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna el codigo  uid del nuevo Turno </returns>
    /// <response code="201">Turno creado</response>
    /// <response code="400">Si el registro es nulo</response>
    [HttpPost("CreateTurno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> CreateTurno([FromBody] CreateTurnoRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateTurnoCommand(request), cancellationToken);
        return Ok(objResult);
        
    }

    /// <summary>
    /// Crea un nuveo turno con sus respectivos turnos
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("CreateTurnoSubturno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> CreateTurnoSubturno([FromBody] CreateTurnoSubTurnoRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateTurnoSubTurnoCommand(request), cancellationToken);
        return Ok(objResult);
    }


    /// <summary>
    /// Asigna turno a un empleado
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("AsignarSubturnoCliente")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> AsignarSubturnoCliente([FromBody] CreateSubturnoClienteRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateSubturnoClienteCommand(request), cancellationToken);
        return Ok(objResult);
    }


    /// <summary>
    /// Retorna el listado de turnos
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetTurno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetTurnos(CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetTurnosAsyncQuery(), cancellationToken);
        return Ok(objResult);
    }
    

    /// <summary>
    /// Retorna el listado de maestros de turnos
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetMaestrosTurno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetMaestrosTurnos(CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetMaestrosTurnoAsyncQuery(), cancellationToken);
        return Ok(objResult);
    }
    
}
