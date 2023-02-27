using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Marcacion.Dto;

public class NovedadMarcacionType
{
    [JsonPropertyName("idMarcacion")]
    public int IdMarcacion { get; set; }

    [JsonPropertyName("colaborador")]
    public string Colaborador { get; set; }

    [JsonPropertyName("turnoEntrada")]
    public DateTime TurnoEntrada { get; set; }

    [JsonPropertyName("turnoSalida")]
    public DateTime TurnoSalida { get; set; }

    [JsonPropertyName("fechaMarcacion")]
    public DateTime FechaMarcacion { get; set; }

    [JsonPropertyName("canal")]
    public string Canal { get; set; }

    [JsonPropertyName("dispositivo")]
    public string Dispositivo { get; set; }

    [JsonPropertyName("tipoNovedad")]
    public string TipoNovedad { get; set; }

    [JsonPropertyName("descripcionMensaje")]
    public string DescripcionMensaje { get; set; }
    
}
