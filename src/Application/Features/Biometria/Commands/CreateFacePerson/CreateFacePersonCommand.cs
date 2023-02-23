using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Biometria.Commands.CreateFacePerson
{
    public record CreateFacePersonCommand(CreateFacePersonRequest BiometriaRequest) : IRequest<ResponseType<string>>;

    public class CreateFacePersonCommandHandler : IRequestHandler<CreateFacePersonCommand, ResponseType<string>>
    {
        private readonly IBiometria _repoBiometriaAsync;

        public CreateFacePersonCommandHandler(IBiometria repoBiometriaAsync)
        {
            _repoBiometriaAsync = repoBiometriaAsync;
        }

        public async Task<ResponseType<string>> Handle(CreateFacePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoBiometriaAsync.CreateFacePersonAsync(request.BiometriaRequest);

                return objResult;
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

    }

}
