
using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Clients.Dto
{
    public class TurnoType
    {
        public Guid IdTipoSubturno { get; set; }

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

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = string.Empty;

        [JsonPropertyName("usuario")]
        public string Usario { get; set; } = string.Empty;

    }
}
