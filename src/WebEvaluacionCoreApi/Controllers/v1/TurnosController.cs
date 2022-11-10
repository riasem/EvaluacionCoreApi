using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateSubturnoCliente;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurno;
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
    /// <remarks>
    /// Ejemplo request:
    /// {
    ///       "idTipoTurno": "A04BB059-033B-43EA-9738-A5F351E00C8B",
    ///       "codigoTurno": "A1",
    ///       "codigoIntegracion": "A1",
    ///       "descripcion": "ADMINISTRATIVO 1 PRUEBA",
    ///       "entrada": "1990-01-01T08:00:00.570Z",
    ///       "salida": "1990-01-01T17:00:00.070Z",
    ///       "margenEntrada": "1990-01-01T00:05:00.570Z",
    ///       "margenSalida": "1990-01-01T14:05:00.500Z",
    ///       "totalHoras": "8"
    /// }
    /// 
    /// </remarks>
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
    [HttpGet("GetMatestrosTurno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetMaestrosTurnos(CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetMaestrosTurnoAsyncQuery(), cancellationToken);
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

}
