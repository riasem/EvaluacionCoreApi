using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Commands.CreateLocacion;

public class CreateLocacionRequest
{
    [JsonPropertyName("codigo")]
    public string Codigo { get; set; }

    [JsonPropertyName("idEmpresa")]
    public Guid IdEmpresa { get; set; }

    [JsonPropertyName("latitud")]
    public double Latitud { get; set; }

    [JsonPropertyName("longitud")]
    public double Longitud { get; set; }

    [JsonPropertyName("descripcion")]
    public string Descripcion { get; set; }
}
