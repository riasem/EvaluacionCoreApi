using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacionCapacidadesEspeciales;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb;
using EvaluacionCore.Application.Features.Marcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Queries.GetRecurso;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebEvaluacionCoreApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MarcacionController : ApiControllerBase
    {
        /// <summary>
        /// Consulta información de la bitácora de marcaciones
        /// </summary>
        /// <param name="CodUdn"></param>
        /// <param name="CodArea"></param>
        /// <param name="CodSubcentro"></param>
        /// <param name="CodMarcacion"></param>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Suscriptor"></param>
        /// <returns></returns>
        [HttpGet("GetBitacoraMarcacion")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<BitacoraMarcacionType>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBitacoraMarcacion(string? CodUdn, string? CodArea, string? CodSubcentro, string? CodMarcacion, string FechaDesde, string FechaHasta, CancellationToken cancellationToken, string? Suscriptor)
        {
            var request = new GetBitacoraMarcacionRequest()
            {
                Suscriptor = string.IsNullOrEmpty(Suscriptor) ? string.Empty : Suscriptor,
                CodUdn = string.IsNullOrEmpty(CodUdn) ? string.Empty : CodUdn,
                CodArea = string.IsNullOrEmpty(CodArea) ? string.Empty : CodArea,
                CodSubcentro = string.IsNullOrEmpty(CodSubcentro) ? string.Empty : CodSubcentro,
                CodMarcacion = string.IsNullOrEmpty(CodMarcacion) ? string.Empty: CodMarcacion,
                FechaDesde = FechaDesde,
                FechaHasta = FechaHasta
            };

            var objResult = await Mediator.Send(new GetBitacoraMarcacionCommand(request), cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Consulta información de los combos para la opción bitácora de marcaciones
        /// </summary>
        /// <param name="Tipo"></param>
        /// <param name="Udn"></param>
        /// <param name="Area"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetComboBitacoraMarcacion")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<BitacoraMarcacionType>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComboBitacoraMarcacion(string Tipo, string? Udn, string? Area, CancellationToken cancellationToken)
        {
            var request = new GetComboBitacoraMarcacionRequest() { Tipo = Tipo, Udn = Udn ?? "", Area = Area ?? "" };

            var objResult = await Mediator.Send(new GetComboBitacoraMarcacionCommand(request), cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Registro de marcación de entrada y salida
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("GenerarMarcacion")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<MarcacionResponseType>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMarcacion([FromBody] CreateMarcacionRequest request, CancellationToken cancellationToken)
        {
            var query = new CreateMarcacionCommand(request);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }


        /// <summary>
        /// Obtener datos de horas trabajadas, horas asignadas y horas pendientes
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechasHasta"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("ConsultaRecursos")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<ConsultaRecursoType>>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultaRecursos(Guid IdCliente, DateTime fechaDesde, DateTime fechasHasta, CancellationToken cancellationToken)
        {
            var query = new GetRecursoQueries(IdCliente, fechaDesde,fechasHasta);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Consulta y genera información de la bitácora de marcaciones del departamento de capacidades especiales
        /// </summary>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetBitacoraMarcacionCapacidadesEspeciales")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<BitacoraMarcacionType>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBitacoraMarcacionCapacidadesEspeciales(string fechaDesde, string fechaHasta, CancellationToken cancellationToken)
        {
            var request = new GetBitacoraMarcacionCapacidadesEspecialesRequest()
            {
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta
            };

            var objResult = await Mediator.Send(new GetBitacoraMarcacionCapacidadesEspecialesCommand(request), cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Registro de marcación de entrada y salida para Enrolapp Web
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("GenerarMarcacionWeb")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMarcacionWeb([FromBody] CreateMarcacionWebRequest request, CancellationToken cancellationToken)
        {
            var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
            request.IdentificacionJefe = Identificacion;

            var query = new CreateMarcacionWebCommand(request);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }

    }
}