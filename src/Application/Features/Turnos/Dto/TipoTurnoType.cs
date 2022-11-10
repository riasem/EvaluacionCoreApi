using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Turnos.Dto
{
    public class TipoTurnoType
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        //[JsonPropertyName("codigoTipoTurno")]
        //public string CodigoTipoTurno { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        //[JsonPropertyName("estado")]
        //public string Estado { get; set; }

    }
}
