using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateLocacionTurnoCliente
{
    public class CreateLocacionTurnoClienteRequest
    {
        [JsonPropertyName("idSubTurno")]
        public Guid IdSubturno { get; set; }
        
        [JsonPropertyName("idCliente")]
        public Guid IdCliente { get; set; }

    }
}
