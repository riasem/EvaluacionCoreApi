using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurno;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoSubTurno;
using EvaluacionCore.Application.Features.Turnos.Commands.GetTurnosAsignadosExcel;
using EvaluacionCore.Application.Features.Turnos.Commands.InactivaTurnoColaborador;
using EvaluacionCore.Application.Features.Turnos.Commands.UpdateTurnoColaborador;
using EvaluacionCore.Application.Features.Turnos.Queries.GetMaestrosTurnoAsync;
using EvaluacionCore.Application.Features.Turnos.Queries.GetTurnosAsync;
using EvaluacionCore.Application.Features.Turnos.Queries.GetTurnosColaborador;
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
    /// Crea un nuevo turno con sus respectivos turnos
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
    [HttpPost("AsignarTurnoColaborador")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> AsignarSubturnoColaborador([FromBody] CreateTurnoColaboradorRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateTurnoColaboradorCommand(request), cancellationToken);
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


    /// <summary>
    /// Retorna el listado de turnos asignados al colaborador en un rango de fechas 
    /// </summary>
    /// <param name="identificacion"></param>
    /// <param name="fechaDesde"></param>
    /// <param name="fechaHasta"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetTurnosAsignados")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetTurnosColaborador(string identificacion, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetTurnosColaboradorAsyncQuery(identificacion, fechaDesde, fechaHasta), cancellationToken);
        return Ok(objResult);
    }



    /// <summary>
    /// Crea un nuveo turno con sus respectivos turnos
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("UpdateTurnoSubturno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> CreaeteTurnoSubturno([FromBody] UpdateTurnoColaboradorRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new UpdateTurnoColaboradorCommand(request), cancellationToken);
        return Ok(objResult);
    }



    /// <summary>
    /// Inactiva Turnos Asignados al usuario
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("InactivateTurnoSubturno")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> InactivateTurno(List<InactivaTurnoColaboradorRequest> request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new InactivaTurnoColaboradorCommand(request), cancellationToken);
        return Ok(objResult);
    }

    ///// <summary>
    ///// Actualiza los turnos a un colaborador
    ///// </summary>
    ///// <param name="request"></param>
    ///// <param name="cancellationToken"></param>
    ///// <returns></returns>
    //[HttpPut("ActualizarTurnoColaborador")]
    //[EnableCors("AllowOrigin")]
    //[ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    //[Authorize]
    //public async Task<IActionResult> ActualizarTurnoColaborador([FromBody] UpdateTurnoColaboradorRequest request, CancellationToken cancellationToken)
    //{
    //    var objResult = await Mediator.Send(new UpdateTurnoColaboradorCommand(request), cancellationToken);
    //    return Ok(objResult);
    //}

    /// <summary>
    /// Consulta información generar archivos turnos asignados
    /// </summary>
    /// <param name="codUdn"></param>
    /// <param name="codArea"></param>
    /// <param name="codScc"></param>
    /// <param name="fechaDesde"></param>
    /// <param name="fechaHasta"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetInfoTurnosAsignadosExcel")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetInfoTurnosAsignadosExcel(string codUdn, string? codArea, string? codScc, string fechaDesde, string fechaHasta, CancellationToken cancellationToken)
    {
        GetTurnosAsignadosExcelRequest request = new GetTurnosAsignadosExcelRequest
        {
            CodUdn = codUdn,
            CodArea = string.IsNullOrEmpty(codArea) ? "" : codArea,
            CodScc = string.IsNullOrEmpty(codScc) ? "" : codScc,
            FechaDesde = fechaDesde,
            FechaHasta = fechaHasta
        };

        var objResult = await Mediator.Send(new GetTurnosAsignadosExcelCommand(request), cancellationToken);
        return Ok(objResult);
    }

}
