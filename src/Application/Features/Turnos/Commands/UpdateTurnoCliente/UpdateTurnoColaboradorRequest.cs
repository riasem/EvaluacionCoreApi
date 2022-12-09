using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;
using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.UpdateTurnoColaborador
{
    public class UpdateTurnoColaboradorRequest
    {
        [JsonPropertyName("idTurnoColaborador")]
        public Guid IdTurnoColaborador { get; set; }

        [JsonPropertyName("idTurno")]
        public Guid IdTurno { get; set; }

        [JsonPropertyName("fechaAsignacionDesde")]
        public DateTime FechaAsignacionDesde { get; set; }

        [JsonPropertyName("fechaAsignacionHasta")]
        public DateTime FechaAsignacionHasta { get; set; }

        [JsonPropertyName("clienteSubturno")]
        public List<ClienteSubturno2> ClienteSubturnos { get; set; }

    }

    public class ClienteSubturno2
    {
        [JsonPropertyName("idCliente")]
        public Guid IdCliente { get; set; }
        [JsonPropertyName("clienteSubturno")]
        public List<Subturno2> Subturnos { get; set; }
    }

    public class Subturno2
    {
        [JsonPropertyName("idTurnoColaborador")]
        public Guid IdSubTurnoColaborador { get; set; }
        public Guid IdSubturno { get; set; }
    }
}
