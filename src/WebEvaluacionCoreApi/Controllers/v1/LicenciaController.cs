using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Licencia.Commands.ActualizarLicencia;
using EvaluacionCore.Application.Features.Licencia.Dto;
using EvaluacionCore.Application.Features.Licencia.Queries.ConsultarLicencia;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;

namespace WebEvaluacionCoreApi.Controllers.v1;

[ApiVersion("1.0")]
public class LicenciaController : ApiControllerBase
{
    [HttpPut("ActualizarLicencia")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ActualizarLicencia([FromBody] ActualizarLicenciaRequest request, CancellationToken cancellationToken)
    {
        var IdentificacionSesion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
        var query = new ActualizarLicenciaCommand(request,IdentificacionSesion);
        var objResult = await Mediator.Send(query);
        return Ok(objResult);
    }

    [HttpGet("ConsultarLicencia")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<LicenciaResponseType>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConsultarLicencia(Guid idLicencia, CancellationToken cancellationToken)
    {
        var IdentificacionSesion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
        var query = new ConsultarLicenciaQueries(idLicencia, IdentificacionSesion);
        var objResult = await Mediator.Send(query);
        return Ok(objResult);
    }
}
