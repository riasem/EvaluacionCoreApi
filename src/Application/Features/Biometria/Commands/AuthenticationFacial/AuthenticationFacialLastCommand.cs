using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial;

public record AuthenticationFacialLastCommand(AuthenticationFacialLastRequest BiometriaRequest) : IRequest<ResponseType<string>>;
public class AuthenticationFacialLastCommandHandler : IRequestHandler<AuthenticationFacialLastCommand, ResponseType<string>>
{
    private readonly IBiometria _repoBiometriaAsync;

    public AuthenticationFacialLastCommandHandler(IBiometria repoBiometriaAsync)
    {
        _repoBiometriaAsync = repoBiometriaAsync;
    }

    public async Task<ResponseType<string>> Handle(AuthenticationFacialLastCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string OnlineOffline = "ONLINE";
            var objResult = await _repoBiometriaAsync.AuthenticationFacialLastAsync(request.BiometriaRequest, null, OnlineOffline);

            return objResult;
        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
        }
    }

}