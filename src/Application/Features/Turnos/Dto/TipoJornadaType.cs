using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Turnos.Dto
{
    public class TipoJornadaType
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        //[JsonPropertyName("codigoSubclase")]
        //public string CodigoSubclaseTurno { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        //[JsonPropertyName("estado")]
        //public string Estado { get; set; }

    }
}
