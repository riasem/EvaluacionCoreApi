using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;

public class EvaluarAsistenciasRequest 
{

    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }

    [JsonPropertyName("fechaDesde")]
    public DateTime? FechaDesde { get; set; }

    [JsonPropertyName("fechaHasta")]
    public DateTime? FechaHasta { get; set; }

}
