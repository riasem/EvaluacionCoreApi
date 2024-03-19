using System.ComponentModel.DataAnnotations.Schema;
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

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("mensaje")]
        public string Mensaje { get; set; }

        [JsonPropertyName("margenEntradaPrevio")]
        public int? MargenEntradaPrevio { get; set; }

        [JsonPropertyName("margenSalidaPosterior")]
        public int? MargenSalidaPosterior { get; set; }

        [JsonPropertyName("margenEntradaGracia")]
        public int? MargenEntradaGracia { get; set; }

        [JsonPropertyName("margenSalidaGracia")]
        public int? MargenSalidaGracia { get; set; }

        [JsonPropertyName("tiempoMinLaboExtra")]
        public int? TiempoMinLaboExtra { get; set; }

        [JsonPropertyName("tiempoMaxLaboExtra")]
        public int? TiempoMaxLaboExtra { get; set; }

        [JsonPropertyName("horasExtraordinariasAprobadas")]
        public int? HorasExtraordinariasAprobadas { get; set; }

        [JsonPropertyName("comentariosAprobacion")]
        public string ComentariosAprobacion { get; set; }

    }
}
