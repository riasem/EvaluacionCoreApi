using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Subturnos.Commands.CreateSubturno
{
    public class CreateSubturnoRequest 
    {
        [JsonPropertyName("idTipoSubturno")]
        public Guid IdTipoSubturno { get; set; }

        [JsonPropertyName("idTturno")]
        public Guid IdTurno { get; set; }

        [JsonPropertyName("codigoSubturno")]
        public string CodigoSubturno { get; set; } = string.Empty;

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("entrada")]
        public DateTime Entrada { get; set; }

        [JsonPropertyName("salida")]
        public DateTime Salida { get; set; }

        [JsonPropertyName("margenEntrada")]
        public DateTime MargenEntrada { get; set; }

        [JsonPropertyName("margenSalida")]
        public DateTime MargenSalida { get; set; }


    }
}
