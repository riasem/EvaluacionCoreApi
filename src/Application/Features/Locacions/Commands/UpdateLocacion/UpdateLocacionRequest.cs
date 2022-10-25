using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Commands.UpdateLocacion;

public class UpdateLocacionRequest
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("codigo")]
    public string Codigo { get; set; }

    [JsonPropertyName("latitud")]
    public double Latitud { get; set; }

    [JsonPropertyName("longitud")]
    public double Longitud { get; set; }

    [JsonPropertyName("descripcion")]
    public string Descripcion { get; set; }
}
