using EnrolApp.Application.Features.Clients.Queries.GetTurnoById;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Clients.Commands.CreateTurno;
using EvaluacionCore.Application.Features.Turnos.Commands.AsignarTurno;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebEvaluacionCoreApi.Controllers;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class TurnosController : ApiControllerBase
{

    ///// <summary>
    ///// Obtener Turno por el codigo
    ///// </summary>
    ///// <param name="Codigo">codigo del Turno</param>
    ///// <param name="cancellationToken"></param>
    ///// <returns></returns>
   
    //[HttpGet("{Codigo:int}")]
    //[EnableCors("AllowOrigin")]
    //[ProducesResponseType(typeof(TurnoType), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetTurnoById(string Codigo, CancellationToken cancellationToken)
    //{
    //    var query = new GetTurnosAync(Codigo);
    //    var Turno = await Mediator.Send(query, cancellationToken);
    //    return Ok(Turno);
    //}

    /// <summary>
    /// Crea un nuevo Turno
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna el codigo  uid del nuevo Turno </returns>
    /// <remarks>
    /// Ejemplo request:
    ///
    ///     POST /CreateTurno
    ///     {
    ///         "tipoidentificacion": "C",
    ///         "identificacion": "0920693975",
    ///         "genero": "M",
    ///         "latitud": "-2.093042",
    ///         "longitud": "-79.950629",
    ///         "direccion": "Cdla Los Almendros Mz 14 villa: 15",
    ///         "fechaNacimiento": "2022-07-28",
    ///         "correo": "dsanch152000@hotmail.com",
    ///         "password": "Pa$$w0rd214"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Turno creado</response>
    /// <response code="400">Si el registro es nulo</response>
    [HttpPost("CreateTurno")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
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
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetTurnos(CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetTurnosAsyncQuery(), cancellationToken);
        return Ok(objResult);
    }


    /// <summary>
    /// Asigna turno a un empleado
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("AsignarTurno")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> AsignarTurnos([FromBody] AsignarTurnoRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new AsignarTurnoCommand(request), cancellationToken);
        return Ok(objResult);
    }

}
