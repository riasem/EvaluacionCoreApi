using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.AsignarTurno
{
    public class AsignarTurnoRequest
    {
        [JsonPropertyName("idSubTurno")]
        public Guid IdSubturno { get; set; }
        
        [JsonPropertyName("idCliente")]
        public Guid IdCliente { get; set; }

    }
}
