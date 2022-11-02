using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateSubturnoCliente
{
    public class CreateSubturnoClienteRequest
    {
        [JsonPropertyName("idSubTurno")]
        public Guid IdSubturno { get; set; }
        
        [JsonPropertyName("idCliente")]
        public Guid IdCliente { get; set; }

    }
}
