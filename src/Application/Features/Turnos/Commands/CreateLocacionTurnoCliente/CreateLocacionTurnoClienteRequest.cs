using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateLocalidadTurnoCliente
{
    public class CreateLocalidadTurnoClienteRequest
    {
        [JsonPropertyName("idSubTurno")]
        public Guid IdSubturno { get; set; }
        
        [JsonPropertyName("idCliente")]
        public Guid IdCliente { get; set; }

    }
}
