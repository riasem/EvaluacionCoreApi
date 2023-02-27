﻿using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial
{
    public record AuthenticationFacialCommand(AuthenticationFacialRequest BiometriaRequest) : IRequest<ResponseType<string>>;

    public class AuthenticationFacialCommandHandler : IRequestHandler<AuthenticationFacialCommand, ResponseType<string>>
    {
        private readonly IBiometria _repoBiometriaAsync;

        public AuthenticationFacialCommandHandler(IBiometria repoBiometriaAsync)
        {
            _repoBiometriaAsync = repoBiometriaAsync;
        }

        public async Task<ResponseType<string>> Handle(AuthenticationFacialCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoBiometriaAsync.AuthenticationFacialAsync(request.BiometriaRequest);

                return objResult;
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

    }
}