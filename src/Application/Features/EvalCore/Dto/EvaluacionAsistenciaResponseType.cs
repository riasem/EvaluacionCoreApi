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

        //[JsonPropertyName("solicitudes")]
        //public List<Solicitud> Solicitudes { get; set; }

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


        //se añaden campos en marcacion para el control de novedades y solicitudes de entrada
        [JsonPropertyName("idSolicitudEntrada")]
        public Guid? IdSolicitudEntrada  {get; set;}

        [JsonPropertyName("idFeatureEntrada")]
        public Guid? IdFeatureEntrada { get; set; }

        [JsonPropertyName("tipoSolicitudEntrada")]
        public string TipoSolicitudEntrada { get; set; }

        [JsonPropertyName("estadoEntrada")]
        public string EstadoEntrada { get; set; }

        [JsonPropertyName("fechaSolicitudEntrada")]
        public DateTime? FechaSolicitudEntrada { get; set; }

        [JsonPropertyName("usuarioSolicitudEntrada")]
        public string UsuarioSolicitudEntrada { get; set; }

        [JsonPropertyName("estadoSolicitudEntrada")]
        public Guid? EstadoSolicitudEntrada { get; set; }


        //se añaden campos en marcacion para el control de novedades y solicitudes de salida
        [JsonPropertyName("idSolicitudSalida")]
        public Guid? IdSolicitudSalida { get; set;}

        [JsonPropertyName("idFeatureSalida")]
        public Guid? IdFeatureSalida { get; set; }

        [JsonPropertyName("tipoSolicitudSalida")]
        public string TipoSolicitudSalida { get; set; }

        [JsonPropertyName("estadoSalida")]
        public string EstadoSalida { get; set; }

        [JsonPropertyName("fechaSolicitudSalida")]
        public DateTime? FechaSolicitudSalida { get; set; }

        [JsonPropertyName("usuarioSolicitudSalida")]
        public string UsuarioSolicitudSalida { get; set; }

        [JsonPropertyName("estadoSolicitudSalida")]
        public Guid? EstadoSolicitudSalida { get; set; }

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





        //se añaden campos en marcacion para el control de novedades y solicitudes de entrada
        [JsonPropertyName("idSolicitudEntradaReceso")]
        public Guid? IdSolicitudEntradaReceso { get; set; }

        [JsonPropertyName("idFeatureEntradaReceso")]
        public Guid? IdFeatureEntradaReceso { get; set; }

        [JsonPropertyName("estadoEntradaReceso")]
        public string EstadoEntradaReceso { get; set; }

        [JsonPropertyName("tipoSolicitudEntradaReceso")]
        public string TipoSolicitudEntradaReceso { get; set; }

        [JsonPropertyName("fechaSolicitudEntradaReceso")]
        public DateTime? FechaSolicitudEntradaReceso { get; set; }

        [JsonPropertyName("usuarioSolicitudEntradaReceso")]
        public string UsuarioSolicitudEntradaReceso { get; set; }

        [JsonPropertyName("estadoSolicitudEntradaReceso")]
        public Guid? EstadoSolicitudEntradaReceso { get; set; }


        //se añaden campos en marcacion para el control de novedades y solicitudes de salida
        [JsonPropertyName("idSolicitudSalidaReceso")]
        public Guid? IdSolicitudSalidaReceso { get; set; }

        [JsonPropertyName("idFeatureSalidaReceso")]
        public Guid? IdFeatureSalidaReceso { get; set; }

        [JsonPropertyName("tipoSolicitudSalidaReceso")]
        public string TipoSolicitudSalidaReceso { get; set; }

        [JsonPropertyName("estadoSalidaReceso")]
        public string EstadoSalidaReceso { get; set; }

        [JsonPropertyName("fechaSolicitudSalidaReceso")]
        public DateTime? FechaSolicitudSalidaReceso { get; set; }

        [JsonPropertyName("usuarioSolicitudSalidaReceso")]
        public string UsuarioSolicitudSalidaReceso { get; set; }

        [JsonPropertyName("estadoSolicitudSalidaReceso")]
        public Guid? EstadoSolicitudSalidaReceso { get; set; }


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
