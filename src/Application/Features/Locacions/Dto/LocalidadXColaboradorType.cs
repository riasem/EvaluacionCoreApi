using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Dto;
public class LocalidadXColaboradorType
{
    [JsonPropertyName("identificacionLocalidad")]
    public Guid IdentificacionLocalidad { get; set; }

    [JsonPropertyName("codigoLocalidad")]
    public string CodigoLocalidad { get; set; }

    [JsonPropertyName("nombreLocalidad")]
    public string NombreLocalidad { get; set; }
}
