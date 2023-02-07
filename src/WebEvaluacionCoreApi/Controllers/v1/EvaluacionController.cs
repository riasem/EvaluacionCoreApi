using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;
using EvaluacionCore.Application.Features.EvalCore.Queries.GetComboPeriodoAsync;
using EvaluacionCore.Application.Features.EvalCore.Queries.GetEvaluacionAsistenciaAsync;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebEvaluacionCoreApi.Controllers;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class EvaluacionController : ApiControllerBase
{

    /// <summary>
    /// Evaluador de Asistencias
    /// </summary>
    /// <param name="identificacion"></param>
    /// <param name="fechaDesde"></param>
    /// <param name="fechaHasta"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Capacidad que realiza la evaluación de asistencias.</returns>
    /// <response code="201">Evaluación Realizada</response>
    /// <response code="400">Ocurrió un error al realizar la evaluación</response>
    [HttpGet("EvaluarAsistencias")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> EvaluarAsistencias(DateTime? fechaDesde, DateTime? fechaHasta, CancellationToken cancellationToken, string identificacion = "0")
    {
        if (identificacion == "0")
        {
            identificacion = "";
        }
        var objResult = await Mediator.Send(new EvaluarAsistenciasCommand(identificacion, fechaDesde, fechaHasta), cancellationToken);
        return Ok(objResult);
        
    }

    /// <summary>
    /// Obtener Control de Asistencias
    /// </summary>
    /// <param name="identificacion"></param>
    /// <param name="periodo"></param>
    /// <param name="Udn"></param>
    /// <param name="Departamento"></param>
    /// <param name="Area"></param>
    /// <param name="FiltroNovedades"></param>
    /// <param name="idCanal"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Capacidad que realiza la evaluación de asistencias.</returns>
    /// <response code="201">Evaluación Realizada</response>
    /// <response code="400">Ocurrió un error al realizar la evaluación</response>
    [HttpGet("GetAsistencias")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> GetAsistencias(string periodo, string? Udn, string? Departamento, string? Area, CancellationToken cancellationToken, string? identificacion, string FiltroNovedades, Guid? idCanal)
    {
        //if (identificacion == "0")
        //{
        //    identificacion = "";
        //}
        var identificacionSession = new JwtSecurityToken(this.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
        var objResult = await Mediator.Send(new GetEvaluacionAsistenciaAsyncQuery(identificacion, periodo, Udn, Area, Departamento, FiltroNovedades,identificacionSession,idCanal), cancellationToken);
        return Ok(objResult);
        
    }

    /// <summary>
    /// Obtener Combo de novedades para control de asistencias
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Capacidad que realiza la evaluación de asistencias.</returns>
    /// <response code="201">Consulta Realizada</response>
    /// <response code="400">Ocurrió un error al realizar la evaluación</response>
    [HttpGet("GetComboNovedades")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> GetComboNovedades( CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetComboNovedadesAsyncQuery(), cancellationToken);
        return Ok(objResult);
    }

    /// <summary>
    /// Obtener Combo de periodos para control de asistencias
    /// </summary>
    /// <param name="codUdn"></param>
    /// <param name="fechaConsulta"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Capacidad que realiza la evaluación de asistencias.</returns>
    /// <response code="201">Consulta Realizada</response>
    /// <response code="400">Ocurrió un error al realizar la evaluación</response>
    [HttpGet("GetComboPeriodos")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> GetComboPeriodo(string codUdn, DateTime? fechaConsulta, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new GetComboPeriodoAsyncQuery(codUdn, fechaConsulta), cancellationToken);
        return Ok(objResult);
    }
}
