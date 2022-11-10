
using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Turnos.Dto
{
    public class MaestrosTurnoResponseType
    {

        [JsonPropertyName("tipoTurnoType")]
        public List<TipoTurnoType> TipoTurnoType { get; set; }

        [JsonPropertyName("claseTurnoType")]
        public List<ClaseTurnoType> ClaseTurnoType { get; set; }

        [JsonPropertyName("subclaseTurnoType")]
        public List<SubclaseTurnoType> SubclaseTurnoType { get; set; }

        [JsonPropertyName("tipoJornadaType")]
        public List<TipoJornadaType> TipoJornadaType { get; set; }

        [JsonPropertyName("modalidadJornadaType")]
        public List<ModalidadJornadaType> ModalidadJornadaType { get; set; }

    }
}
