using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.EvalCore.Dto
{
    public class EvaluacionAsistenciaResponseType
    {
        [JsonPropertyName("colaborador")]
        public string Colaborador { get; set; }

        [JsonPropertyName("identificacion")]
        public string Identificacion { get; set; }

        [JsonPropertyName("codBiometrico")]
        public string CodBiometrico { get; set; }

        [JsonPropertyName("udn")]
        public string Udn { get; set; }

        [JsonPropertyName("area")]
        public string Area { get; set; }

        [JsonPropertyName("subCentroCosto")]
        public string SubCentroCosto { get; set; }

        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }

        [JsonPropertyName("turnoLaboral")]
        public TurnoLaboral TurnoLaboral { get; set; }

        [JsonPropertyName("turnoReceso")]
        public TurnoReceso TurnoReceso { get; set; }

        [JsonPropertyName("novedad")]
        public string Novedad { get; set; }

        [JsonPropertyName("solicitudes")]
        public Solicitud Solicitudes { get; set; }

    }

    public class TurnoLaboral
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("entrada")]
        public DateTime Entrada { get; set; }

        [JsonPropertyName("salida")]
        public DateTime Salida { get; set; }

        [JsonPropertyName("totalHoras")]
        public string TotalHoras { get; set; }
    }

    public class TurnoReceso
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("entrada")]
        public DateTime Entrada { get; set; }

        [JsonPropertyName("salida")]
        public DateTime Salida { get; set; }

        [JsonPropertyName("totalHoras")]
        public string TotalHoras { get; set; }
    }

    public class Solicitud
    {
        [JsonPropertyName("id")]
        public Guid IdSolicitud { get; set; }

        [JsonPropertyName("idTipoSolicitud")]
        public Guid IdTipoSolicitud { get; set; }

        [JsonPropertyName("tipoSolicitud")]
        public string TipoSolicitud { get; set; }
    }
}
