
using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Turnos.Dto
{
    public class TurnoResponseType
    {

        [JsonPropertyName("tipoJornada")]
        public string TipoJornada { get; set; }

        [JsonPropertyName("turnoType")]
        public List<TurnoType> TurnoType { get; set; }

    }
}
