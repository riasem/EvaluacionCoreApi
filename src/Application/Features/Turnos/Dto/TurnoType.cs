
using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Clients.Dto
{
    public class TurnoType
    {
        [JsonPropertyName("idTipoTurno")]
        public Guid IdTipoTurno { get; set; }

        [JsonPropertyName("TipoTurno")]
        public string TipoTurno { get; set; }

        [JsonPropertyName("codigoTurno")]
        public string CodigoTurno { get; set; } = string.Empty;

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

        [JsonPropertyName("totalHoras")]
        public string TotalHoras { get; set; } = string.Empty;

    }
}
