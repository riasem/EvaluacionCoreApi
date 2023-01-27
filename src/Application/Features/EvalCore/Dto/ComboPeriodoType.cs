using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Permisos.Dto;

public class ComboPeriodoType
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("udn")]
    public string Udn { get; set; }

    [JsonPropertyName("desPeriodo")]
    public string DesPeriodo { get; set; }

    [JsonPropertyName("fechaDesdeCorte")]
    public DateTime FechaDesdeCorte { get; set; }

    [JsonPropertyName("fechaHastaCorte")]
    public DateTime FechaHastaCorte { get; set; }

}
