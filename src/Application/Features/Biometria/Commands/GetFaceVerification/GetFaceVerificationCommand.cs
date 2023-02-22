using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Biometria.Commands.GetFaceVerification
{
    public record GetFaceVerificationCommand(GetFaceVerificationRequest BiometriaRequest) : IRequest<ResponseType<string>>;

    public class GetMarcacionCommandHandler : IRequestHandler<GetFaceVerificationCommand, ResponseType<string>>
    {
        private readonly IBiometria _repoBiometriaAsync;

        public GetMarcacionCommandHandler(IBiometria repoBiometriaAsync)
        {
            _repoBiometriaAsync = repoBiometriaAsync;
        }

        public async Task<ResponseType<string>> Handle(GetFaceVerificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoBiometriaAsync.GetFaceVerificationAsync(request.BiometriaRequest);

                return objResult;
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

    }
}
