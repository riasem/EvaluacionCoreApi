using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Queries.ConsultaJefaturasXCoordinador;
public class JefaturasXCoordinadorRequest
{
    [JsonPropertyName("fecha")]
    public DateTime? Fecha { get; set; }
}