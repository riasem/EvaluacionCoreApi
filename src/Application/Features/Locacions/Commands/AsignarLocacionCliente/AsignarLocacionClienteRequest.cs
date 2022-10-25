using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Commands.AsignarLocacionCliente;

public class AsignarLocacionClienteRequest
{
    [JsonPropertyName("idLocacion")]
    public Guid IdLocacion { get; set; }

    [JsonPropertyName("idCliente")]
    public Guid IdCliente { get; set; }
 
}
