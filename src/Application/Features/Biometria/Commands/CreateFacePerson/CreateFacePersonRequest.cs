namespace EvaluacionCore.Application.Features.Biometria.Commands.CreateFacePerson
{
    public class CreateFacePersonRequest
    {
        public string Colaborador { get; set; }
        public string Base64 { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string FacialPersonUid { get; set; } = string.Empty;
    }
}
