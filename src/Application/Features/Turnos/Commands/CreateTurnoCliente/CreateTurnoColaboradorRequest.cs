using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador
{
    public class CreateTurnoColaboradorRequest
    {
        [JsonPropertyName("idTurno")]
        public Guid IdTurno { get; set; }
        
        [JsonPropertyName("clienteSubturno")]
        public List<ClienteSubturno> ClienteSubturnos { get; set; }

    }

    public class ClienteSubturno
    {
        [JsonPropertyName("idCliente")]
        public Guid IdCliente { get; set; }
        [JsonPropertyName("clienteSubturno")]
        public List<Subturno> Subturnos { get; set; }
    }

    public class Subturno
    {
        public Guid IdSubturno { get; set; }
    }
}
