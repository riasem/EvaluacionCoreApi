namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb
{
    public class CreateMarcacionWebRequest
    {
        public string IdentificacionJefe { get; set; }
        public string IdentificacionColaborador { get; set; }
        public string PinColaborador { get; set; }
        public string TipoMarcacion { get; set; }
        public string Base64Archivo { get; set; } = string.Empty;
        public string NombreArchivo { get; set; } = string.Empty;
        public string ExtensionArchivo { get; set; } = string.Empty;
        public string TipoComunicacion { get; set; } = string.Empty;
    }
}
