using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Commands.CreateFacePerson;
using EvaluacionCore.Application.Features.Biometria.Commands.GetFaceVerification;

namespace EvaluacionCore.Application.Features.Biometria.Interfaces
{
    public interface IBiometria
    {
        Task<ResponseType<string>> GetFaceVerificationAsync(GetFaceVerificationRequest request);
        Task<ResponseType<string>> CreateFacePersonAsync(CreateFacePersonRequest request);
    }
}
