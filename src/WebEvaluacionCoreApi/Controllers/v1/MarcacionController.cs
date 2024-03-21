using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EnrolApp.Application.Features.Marcacion.Commands.NovedadMarcacion;
using EnrolApp.Application.Features.Marcacion.Commands.NovedadMarcacionWeb;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacionCapacidadesEspeciales;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesTxt;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateCabeceraLog;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionApp;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionOffline;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb;
using EvaluacionCore.Application.Features.Marcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Queries.GetDispositivoMarcacion;
using EvaluacionCore.Application.Features.Marcacion.Queries.GetHorasExtrasColaborador;
using EvaluacionCore.Application.Features.Marcacion.Queries.GetListadoColaborador;
using EvaluacionCore.Application.Features.Marcacion.Queries.GetNovedadesMarcacionOffline;
using EvaluacionCore.Application.Features.Marcacion.Queries.GetRecurso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        [ProducesResponseType(typeof(ResponseType<MarcacionWebResponseType>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMarcacionWeb([FromBody] CreateMarcacionWebRequest request, CancellationToken cancellationToken)
        {
            var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
            request.IdentificacionJefe = Identificacion;

            var query = new CreateMarcacionWebCommand(request);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Consulta de novedades de marcaciones para App
        /// </summary>
        /// <param name="Identificacion"></param>
        /// <param name="FiltroNovedades"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetNovedadesMarcaciones")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<MarcacionWebResponseType>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNovedadMarcacion(string Identificacion, string FiltroNovedades, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken)
        {
            //var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
            //request.IdentificacionJefe = Identificacion;

            if (Identificacion.Contains('[') || Identificacion.Contains(']'))
            {
                Identificacion = Identificacion[1..^1];
            }
            
            if (FiltroNovedades.Contains('[') || FiltroNovedades.Contains(']'))
            {
                FiltroNovedades = FiltroNovedades[1..^1];
            }

            var query = new NovedadMarcacionCommand(Identificacion, FiltroNovedades, fechaDesde, fechaHasta);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }


        /// <summary>
        /// Consulta de novedades de marcaciones para Web
        /// </summary>
        /// <param name="Identificacion"></param>
        /// <param name="FiltroNovedades"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetNovedadesMarcacionesWeb")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<MarcacionWebResponseType>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNovedadMarcacionWeb(string Identificacion, string FiltroNovedades, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken)
        {
            //var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
            //request.IdentificacionJefe = Identificacion;

            var query = new NovedadMarcacionWebCommand(Identificacion, FiltroNovedades, fechaDesde, fechaHasta);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }


        /// <summary>
        /// Marcacion App EnrolApp diferentes CD's
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("GenerarMarcacionApp")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMarcacionApp([FromBody] CreateMarcacionAppRequest request, CancellationToken cancellationToken)
        {
            var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;

            var query = new CreateMarcacionAppCommand(request,Identificacion);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }


        [HttpPost("GenerarMarcacionAppLast")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMarcacionAppLast([FromForm] CreateMarcacionAppLastRequest request , CancellationToken cancellationToken)
        {
            var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;

            var query = new CreateMarcacionAppLastCommand(request, Identificacion);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }


        /// <summary>
        /// Listado de Colaboradores por Dispositivo Tablets
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetListadoColaboradorByDispositivo")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListadoColaboradorByDispositivo( CancellationToken cancellationToken)
        {
            var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;

            var query = new GetListadoColaboradorQueries(Identificacion);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }



        /// <summary>
        /// Creación de marcaciones offline con autenticación 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("GenerarMarcacionOffline")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerarMarcacionOffline([FromBody] CreateMarcacionOfflineRequest request, CancellationToken cancellationToken)
        {
            
            var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;

            var query = new CreateMarcacionOfflineCommand(request, Identificacion);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }



        [HttpGet("GetNovedadesMarcacionOffline")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<NovedadesMarcacionOfflineResponse>>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNovedadesMarcacionOffline(string? CodUdn,string? identificacion, DateTime? fechaDesde, DateTime? fechaHasta,int? DeviceId, CancellationToken cancellationToken)
        {
            var IdentificacionSesion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;

            var query = new GetNovedadesMarcacionOfflineQueries(CodUdn,identificacion,fechaDesde,fechaHasta,DeviceId, IdentificacionSesion);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Crear Cabecera de Log para marcaciones Offline
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("CreateCabeceraLog")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogCabeceraOffline([FromBody] CreateCabeceraLogRequest request, CancellationToken cancellationToken)
        {
            var IdentificacionSesion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;

            var query = new CreateCabeceraLogCommand(request, IdentificacionSesion);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Listado de dispositivos de marcaciones tablets
        /// </summary>
        /// <returns></returns>
        [HttpGet("DispositivosMarcacion")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<DispositivosMarcacionResponse>>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> DispositivosMarcacion()
        {
            var query = new GetDispositivoMarcacionQueries();
            var objResult = await Mediator.Send(query);
            return Ok(objResult);
        }

        /// <summary>
        /// Carga de Marcaciones Manuales por archivo Excel
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("CargaMarcacionesExcel")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CargaMarcacionesExcel([FromBody] List<CargaMarcacionesExcelRequest> request, CancellationToken cancellationToken)
        {
            var IdentificacionSesion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
            var query = new CargaMarcacionesExcelCommand(request, IdentificacionSesion);
            var objResult = await Mediator.Send(query);
            return Ok(objResult);
        }


        /// <summary>
        /// Carga de Marcaciones Offline por archivo Txt
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("CargaMarcacionesTxt")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CargaMarcacionesTxt([FromBody] List<CargaMarcacionesTxtRequest> request, CancellationToken cancellationToken)
        {
            var IdentificacionSesion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
            var query = new CargaMarcacionesTxtCommand(request, IdentificacionSesion);
            var objResult = await Mediator.Send(query);
            return Ok(objResult);
        }

        [HttpPost("GetConsultaHorasExtrasColaborador")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<HorasExtrasColaboradorResponse>>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetConsultaHorasExtrasColaborador([FromBody] HorasExtrasColaboradorRequest request, CancellationToken cancellationToken)
        {
            var IdentificacionSesion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;

            var query = new GetHorasExtrasColaboradorQueries(request, IdentificacionSesion);
            var objResult = await Mediator.Send(query, cancellationToken);
            return Ok(objResult);
        }

    }
}