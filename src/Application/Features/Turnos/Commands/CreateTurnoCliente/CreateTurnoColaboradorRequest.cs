using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador
{
    public class CreateTurnoColaboradorRequest
    {
        [JsonPropertyName("idTurno")]
        public Guid IdTurno { get; set; }

        [JsonPropertyName("fechaAsignacionDesde")]
        public DateTime FechaAsignacionDesde { get; set; }

        [JsonPropertyName("fechaAsignacionHasta")]
        public DateTime FechaAsignacionHasta { get; set; }

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
