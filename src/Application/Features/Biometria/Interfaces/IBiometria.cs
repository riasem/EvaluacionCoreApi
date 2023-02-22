using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Commands.GetFaceVerification;

namespace EvaluacionCore.Application.Features.Biometria.Interfaces
{
    public interface IBiometria
    {
        Task<ResponseType<string>> GetFaceVerificationAsync(GetFaceVerificationRequest request);
    }
}
