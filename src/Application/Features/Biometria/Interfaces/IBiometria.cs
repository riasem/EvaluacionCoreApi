using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial;
using EvaluacionCore.Application.Features.Biometria.Commands.CreateFacePerson;
using EvaluacionCore.Application.Features.Biometria.Commands.GetFaceVerification;

namespace EvaluacionCore.Application.Features.Biometria.Interfaces
{
    public interface IBiometria
    {
        Task<ResponseType<string>> GetFaceVerificationAsync(GetFaceVerificationRequest request);
        Task<ResponseType<string>> CreateFacePersonAsync(CreateFacePersonRequest request);
        Task<ResponseType<string>> AuthenticationFacialAsync(AuthenticationFacialRequest request, string IdentificacionSesion, string OnlineOffline);
        Task<ResponseType<string>> AuthenticationFacialLastAsync(AuthenticationFacialLastRequest request, string IdentificacionSesion, string OnlineOffline);
    }
}