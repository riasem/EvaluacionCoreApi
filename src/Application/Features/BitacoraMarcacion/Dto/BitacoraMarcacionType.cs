using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Dto
{
    public class BitacoraMarcacionType
    {
        [JsonPropertyName("udn")]
        public string Udn { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("cedula")]
        public string Cedula { get; set; }
        
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }
        
        [JsonPropertyName("area")] 
        public string Area { get; set; }
        
        [JsonPropertyName("subcentroCosto")]
        public string SubCentroCosto { get; set; }

        [JsonPropertyName("fechaHora")]
        public string FechaHora { get; set; }

        [JsonPropertyName("fecha")]
        public string Fecha { get; set; }

        [JsonPropertyName("hora")]
        public string Hora { get; set; }

        [JsonPropertyName("codEvento")]
        public string CodEvento { get; set; }

        [JsonPropertyName("evento")]
        public string Evento { get; set; }

        [JsonPropertyName("novedad")]
        public string Novedad { get; set; }
    }
}
