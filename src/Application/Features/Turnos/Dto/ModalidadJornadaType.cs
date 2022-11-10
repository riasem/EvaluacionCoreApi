using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Turnos.Dto
{
    public class ModalidadJornadaType
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        //[JsonPropertyName("codigoSubclase")]
        //public string CodigoSubclaseTurno { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        //[JsonPropertyName("estado")]
        //public string Estado { get; set; }

    }
}
