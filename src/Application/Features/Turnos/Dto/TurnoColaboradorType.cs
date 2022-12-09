using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Turnos.Dto
{
    public class TurnoColaboradorType
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("idTurno")]
        public Guid IdTurno { get; set; }

        [JsonPropertyName("idTurnoPadre")]
        public Guid? IdTurnoPadre { get; set; }

        [JsonPropertyName("idColaborador")]
        public Guid IdColaborador { get; set; }

        [JsonPropertyName("nombresColaborador")]
        public string NombresColaborador { get; set; }

        [JsonPropertyName("apellidosColaborador")]
        public string ApellidosColaborador { get; set; }

        [JsonPropertyName("identificacion")]
        public string Identificacion { get; set; }

        [JsonPropertyName("fechaAsignacion")]
        public DateTime FechaAsignacion { get; set; }

        [JsonPropertyName("horaEntrada")]
        public DateTime HoraEntrada { get; set; }

        [JsonPropertyName("horaSalida")]
        public DateTime HoraSalida { get; set; }

        [JsonPropertyName("codigoTurno")]
        public string CodigoTurno { get; set; }

        [JsonPropertyName("codigoIntegracion")]
        public string CodigoIntegracion { get; set; }

    }
}
