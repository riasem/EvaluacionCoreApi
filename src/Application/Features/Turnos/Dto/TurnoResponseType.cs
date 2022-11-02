
using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Turnos.Dto
{
    public class TurnoResponseType
    {

        [JsonPropertyName("TipoTurno")]
        public string TipoTurno { get; set; }

        [JsonPropertyName("turnoType")]
        public List<TurnoType> TurnoType { get; set; }

    }
}
