using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial;
using EvaluacionCore.Application.Features.Biometria.Commands.CreateFacePerson;
using EvaluacionCore.Application.Features.Biometria.Commands.GetFaceVerification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEvaluacionCoreApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BiometriaController : ApiControllerBase
    {
        /// <summary>
        /// Verificación facial 
        /// </summary>
        /// <param name="biometriaRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("VerificacionFacial")]
        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> VerificacionFacial(GetFaceVerificationRequest biometriaRequest, CancellationToken cancellationToken)
        {
            var objResult = await Mediator.Send(new GetFaceVerificationCommand(biometriaRequest), cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Creación persona facial
        /// </summary>
        /// <param name="biometriaRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("CreacionFacialPersona")]
        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreacionFacialPersona(CreateFacePersonRequest biometriaRequest, CancellationToken cancellationToken)
        {
            var objResult = await Mediator.Send(new CreateFacePersonCommand(biometriaRequest), cancellationToken);
            return Ok(objResult);
        }

        /// <summary>
        /// Autenticación facial
        /// </summary>
        /// <param name="biometriaRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AutenticacionFacialPersona")]
        [EnableCors("AllowOrigin")]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AutenticacionFacialPersona(AuthenticationFacialRequest biometriaRequest, CancellationToken cancellationToken)
        {
            var objResult = await Mediator.Send(new AuthenticationFacialCommand(biometriaRequest), cancellationToken);
            return Ok(objResult);
        }


        [HttpPost("AutenticacionFacialPersonaLast")]
        [EnableCors("AllowOrigin")]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AutenticacionFacialPersonaLast([FromForm] AuthenticationFacialLastRequest biometriaRequest, CancellationToken cancellationToken)
        {

            var objResult = await Mediator.Send(new AuthenticationFacialLastCommand(biometriaRequest), cancellationToken);
            return Ok(objResult);
        }






    }
}
