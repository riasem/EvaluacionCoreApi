using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Marcacion.Dto
{
    public class ControlAsistenciaType
    {
        [JsonPropertyName("codigoBiometrico")]
        public string CodigoBiometrico { get; set; }

        [JsonPropertyName("idTurno")]
        public Guid? IdTurno { get; set; } = Guid.Empty;

        [JsonPropertyName("codigoTurno")]
        public string CodigoTurno { get; set; }

        [JsonPropertyName("claseTurno")]
        public string ClaseTurno { get; set; }

        [JsonPropertyName("fechaAsginacion")]
        public DateTime? FechaAsginacion { get; set; }

        [JsonPropertyName("fechaTurno")]
        public DateTime? FechaTurno { get; set; }

        [JsonPropertyName("entrada")]
        public DateTime? Entrada { get; set; }

        [JsonPropertyName("salida")]
        public DateTime? Salida { get; set; }

        [JsonPropertyName("totalHoras")]
        public string TotalHoras { get; set; }

        [JsonPropertyName("fechaHoraIngreso")]
        public DateTime? FechaHoraIngreso { get; set; }

        [JsonPropertyName("minutosNovedadIngreso")]
        public int? MinutosNovedadIngreso { get; set; }

        [JsonPropertyName("novedadIngreso")]
        public string NovedadIngreso { get; set; }

        [JsonPropertyName("estadoIngreso")]
        public string EstadoIngreso { get; set; }

        [JsonPropertyName("idSolicitudIngreso")]
        public Guid? IdSolicitudIngreso { get; set; } = Guid.Empty;

        [JsonPropertyName("idFeatureIngreso")]
        public Guid? IdFeatureIngreso { get; set; } = Guid.Empty;

        [JsonPropertyName("fechaSolicitudIngreso")]
        public DateTime? FechaSolicitudIngreso { get; set; }

        [JsonPropertyName("usuarioSolicitudIngreso")]
        public string UsuarioSolicitudIngreso { get; set; }

        [JsonPropertyName("estadoSolicitudIngreso")]
        public Guid? EstadoSolicitudIngreso { get; set; } = Guid.Empty;

        [JsonPropertyName("fechaHoraSalida")]
        public DateTime? FechaHoraSalida { get; set; }

        [JsonPropertyName("minutosNovedadSalida")]
        public int? MinutosNovedadSalida { get; set; }

        [JsonPropertyName("novedadSalida")]
        public string NovedadSalida { get; set; }

        [JsonPropertyName("estadoSalida")]
        public string EstadoSalida { get; set; }

        [JsonPropertyName("idSolicitudSalida")]
        public Guid? IdSolicitudSalida { get; set; } = Guid.Empty;

        [JsonPropertyName("idFeatureSalida")]
        public Guid? IdFeatureSalida { get; set; } = Guid.Empty;

        [JsonPropertyName("fechaSolicitudSalida")]
        public DateTime? FechaSolicitudSalida { get; set; }

        [JsonPropertyName("usuarioSolicitudSalida")]
        public string UsuarioSolicitudSalida { get; set; }

        [JsonPropertyName("estadoSolicitudSalida")]
        public Guid? EstadoSolicitudSalida { get; set; } = Guid.Empty;

        [JsonPropertyName("fechaHoraEntradaReceso")]
        public DateTime? FechaHoraEntradaReceso { get; set; }

        [JsonPropertyName("minutosNovedadEntradaReceso")]
        public int? MinutosNovedadEntradaReceso { get; set; }

        [JsonPropertyName("novedadEntradaReceso")]
        public string NovedadEntradaReceso { get; set; }

        [JsonPropertyName("fechaHoraSalidaReceso")]
        public DateTime? FechaHoraSalidaReceso { get; set; }

        [JsonPropertyName("minutosNovedadSalidaReceso")]
        public int? MinutosNovedadSalidaReceso { get; set; }

        [JsonPropertyName("novedadSalidaReceso")]
        public string NovedadSalidaReceso { get; set; }

    }
}
