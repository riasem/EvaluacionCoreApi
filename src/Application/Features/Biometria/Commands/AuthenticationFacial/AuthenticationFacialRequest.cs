namespace EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial
{
    public class AuthenticationFacialRequest
    {
        public string Base64 { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string FacialPersonUid { get; set; } = string.Empty;
    }
}
