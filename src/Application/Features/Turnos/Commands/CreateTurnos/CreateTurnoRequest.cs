using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurno
{
    public class CreateTurnoRequest 
    {

        [JsonPropertyName("idTurnoPadre")]
        public Guid? IdTurnoPadre { get; set; }

        [JsonPropertyName("idTipoTurno")]
        public Guid IdTipoTurno { get; set; }

        [JsonPropertyName("idClaseTurno")] //laboral - receso
        public Guid IdClaseTurno { get; set; }

        [JsonPropertyName("idSubclaseTurno")]  // laboral - A - D - P
        public Guid IdSubclaseTurno { get; set; }

        [JsonPropertyName("idTipoJornada")] // diurna - noctura
        public Guid IdTipoJornada { get; set; }

        [JsonPropertyName("idModalidadJornada")] // completa - parcial
        public Guid IdModalidadJornada { get; set; }

        [JsonPropertyName("codigoTurno")]
        public string CodigoTurno { get; set; }

        [JsonPropertyName("codigoIntegracion")]
        public string CodigoIntegracion { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("entrada")]
        public DateTime Entrada { get; set; }

        [JsonPropertyName("salida")]
        public DateTime Salida { get; set; }

        [JsonPropertyName("margenEntrada")]
        public DateTime MargenEntrada { get; set; }

        [JsonPropertyName("margenSalida")]
        public DateTime MargenSalida { get; set; }

        [JsonPropertyName("margenEntradaPrevio")]
        public string MargenEntradaPrevio { get; set; }

        [JsonPropertyName("margenEntradaPosterior")]
        public string MargenEntradaPosterior { get; set; }

        [JsonPropertyName("margenSalidaPrevio")]
        public string MargenSalidaPrevio { get; set; }

        [JsonPropertyName("margenSalidaPosterior")]
        public string MargenSalidaPosterior { get; set; }

        [JsonPropertyName("totalHoras")]
        public string TotalHoras { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; } 


        //AUDITORIA
        [JsonPropertyName("usuarioCreacion")]
        public string UsuarioCreacion { get; set; } = string.Empty;

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;

        [JsonPropertyName("usuarioModificacion")]
        public string UsuarioModificacion { get; set; } = string.Empty;

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

    }
}
