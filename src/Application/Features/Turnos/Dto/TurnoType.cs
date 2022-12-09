using System.Text.Json.Serialization;


namespace EvaluacionCore.Application.Features.Turnos.Dto
{
    public class TurnoType
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("idTurnoPadre")]
        public Guid? IdTurnoPadre { get; set; }

        [JsonPropertyName("idTipoTurno")]
        public Guid? IdTipoTurno { get; set; }

        [JsonPropertyName("idClaseTurno")]
        public Guid? IdClaseTurno { get; set; }

        [JsonPropertyName("idSubclaseTurno")]
        public Guid? IdSubclaseTurno { get; set; }

        [JsonPropertyName("idTipoJornada")]
        public Guid? IdTipoJornada { get; set; }

        [JsonPropertyName("idModalidadJornada")]
        public Guid? IdModalidadJornada { get; set; }

        [JsonPropertyName("tipoTurno")]
        public string TipoTurno { get; set; } = string.Empty;

        [JsonPropertyName("claseTurno")]
        public string ClaseTurno { get; set; } = string.Empty;

        [JsonPropertyName("subclaseTurno")]
        public string SubclaseTurno { get; set; } = string.Empty;

        [JsonPropertyName("tipoJornada")]
        public string TipoJornada { get; set; } = string.Empty;

        [JsonPropertyName("modalidadJornada")]
        public string ModalidadJornada { get; set; } = string.Empty;

        [JsonPropertyName("codigoTurno")]
        public string CodigoTurno { get; set; } = string.Empty;

        [JsonPropertyName("codigoIntegracion")]
        public string CodigoIntegracion { get; set; } = string.Empty;

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("entrada")]
        public DateTime Entrada { get; set; }

        [JsonPropertyName("salida")]
        public DateTime Salida { get; set; }

        [JsonPropertyName("margenEntradaPrevio")]
        public int? MargenEntradaPrevio { get; set; }

        [JsonPropertyName("margenSalidaPosterior")]
        public int? MargenSalidaPosterior { get; set; }

        [JsonPropertyName("margenEntradaGracia")]
        public int? MargenEntradaGracia { get; set; }

        [JsonPropertyName("margenSalidaGracia")]
        public int? MargenSalidaGracia { get; set; }



        [JsonPropertyName("codigoEntrada")]
        public int? CodigoEntrada { get; set; }

        [JsonPropertyName("codigoSalida")]
        public int? CodigoSalida { get; set; }

        [JsonPropertyName("totalHoras")]
        public string TotalHoras { get; set; } = string.Empty;

        [JsonPropertyName("SubturnoType")]
        public List<SubturnoType> SubturnoType { get; set; }

    }
}
