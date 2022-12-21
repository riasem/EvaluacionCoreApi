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

        [JsonPropertyName("novedades")]
        public List<Novedad> Novedades { get; set; }

        [JsonPropertyName("solicitudes")]
        public List<Solicitud> Solicitudes { get; set; }

    }

    public class TurnoLaboral
    {
        [JsonPropertyName("idTurno")]
        public Guid? Id { get; set; }

        [JsonPropertyName("entrada")]  //turno entrada
        public DateTime? Entrada { get; set; }

        [JsonPropertyName("idMarcacionEntrada")]
        public int IdMarcacionEntrada { get; set; }

        [JsonPropertyName("marcacionEntrada")]
        public DateTime? MarcacionEntrada { get; set; }

        [JsonPropertyName("salida")] // turno salida
        public DateTime? Salida { get; set; }

        [JsonPropertyName("idMarcacionSalida")]
        public int IdMarcacionSalida { get; set; }

        [JsonPropertyName("marcacionSalida")]
        public DateTime? MarcacionSalida { get; set; }

        [JsonPropertyName("totalHoras")] //duracion del turno laboral
        public string TotalHoras { get; set; }
    }

    public class TurnoReceso
    {
        [JsonPropertyName("idTurno")]
        public Guid? Id { get; set; }

        [JsonPropertyName("entrada")]  //turno entrada
        public DateTime? Entrada { get; set; }

        [JsonPropertyName("idMarcacionEntrada")]
        public int IdMarcacionEntrada { get; set; }

        [JsonPropertyName("marcacionEntrada")]
        public DateTime? MarcacionEntrada { get; set; }

        [JsonPropertyName("salida")] // turno salida
        public DateTime? Salida { get; set; }

        [JsonPropertyName("idMarcacionSalida")]
        public int IdMarcacionSalida { get; set; }

        [JsonPropertyName("marcacionSalida")]
        public DateTime? MarcacionSalida { get; set; }

        [JsonPropertyName("totalHoras")]
        public string TotalHoras { get; set; } // duracion de turno de receso
    }

    public class Solicitud
    {
        [JsonPropertyName("id")]
        public Guid IdSolicitud { get; set; }

        [JsonPropertyName("idTipoSolicitud")] 
        public Guid IdTipoSolicitud { get; set; }

        [JsonPropertyName("tipoSolicitud")] // VAC, PER, JUS (HORAS EXTRAS)
        public string TipoSolicitud { get; set; }

        [JsonPropertyName("aplicaDescuento")]
        public bool AplicaDescuento { get; set; }
    }

    public class Novedad
    {
        [JsonPropertyName("idMarcacion")]
        public int IdMarcacion { get; set; }

        [JsonPropertyName("idSolicitud")]
        public Guid IdSolicitud { get; set; }

        [JsonPropertyName("usuarioAprobador")]
        public string UsuarioAprobador { get; set; }

        [JsonPropertyName("fechaAprobacion")]
        public DateTime? FechaAprobacion { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("minutosNovedad")]
        public string MinutosNovedad { get; set; }

        [JsonPropertyName("estadoMarcacion")]
        public string EstadoMarcacion { get; set; }
    }
}
