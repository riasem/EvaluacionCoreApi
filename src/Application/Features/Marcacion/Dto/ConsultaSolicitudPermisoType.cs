using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Marcacion.Dto
{
    public class ConsultaSolicitudPermisoType
    {
        [JsonPropertyName("codigoBiometricoBeneficiario")]
        public int CodigoBiometricoBeneficiario { get; set; }

        [JsonPropertyName("idSolicitudPermiso")]
        public Guid IdSolicitudPermiso { get; set; } = Guid.Empty;

        [JsonPropertyName("numeroSolicitud")]
        public int NumeroSolicitud { get; set; }

        [JsonPropertyName("idEstadoSolicitud")]
        public Guid IdEstadoSolicitud { get; set; } = Guid.Empty;

        [JsonPropertyName("codigoEstado")]
        public string CodigoEstado { get; set; }

        [JsonPropertyName("fechaAprobacion")]
        public DateTime FechaAprobacion { get; set; }

        [JsonPropertyName("fechaPermisoDesde")]
        public DateTime? FechaPermisoDesde { get; set; }

        [JsonPropertyName("fechaPermisoHasta")]
        public DateTime? FechaPermisoHasta { get; set; }
    }
}
