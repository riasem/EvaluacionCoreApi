namespace EvaluacionCore.Application.Features.Biometria.Commands.GetFaceVerification
{
    public class GetFaceVerificationRequest
    {
        public string Base64 { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
    }
}
