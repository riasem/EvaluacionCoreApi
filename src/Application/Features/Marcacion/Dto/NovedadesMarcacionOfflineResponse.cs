using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Dto;

public class NovedadesMarcacionOfflineResponse
{
    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }

    [JsonPropertyName("empleado")]
    public string Empleado { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("imagenColaborador")]
    public string ImagenColaborador { get; set; }

    [JsonPropertyName("imagenMarcacion")]
    public string ImagenMarcacion { get; set; }

    [JsonPropertyName("estadoValidacion")]
    public bool estadoValidacion { get; set; }

    [JsonPropertyName("estadoReconocimiento")]
    public string estadoReconocimiento { get; set; }

}
