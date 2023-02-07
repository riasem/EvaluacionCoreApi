using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Marcacion.Dto
{
    public class MarcacionWebResponseType
    {
        [JsonPropertyName("colaborador")]
        public string Colaborador { get; set; }

        [JsonPropertyName("identificacion")]
        public string Identificacion { get; set; }

        [JsonPropertyName("mensaje")]
        public string Mensaje { get; set; }
    }
}
