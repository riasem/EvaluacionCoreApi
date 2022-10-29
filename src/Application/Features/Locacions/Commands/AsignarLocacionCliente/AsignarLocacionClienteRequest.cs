using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Localidads.Commands.AsignarLocalidadCliente;

public class AsignarLocalidadClienteRequest
{
    [JsonPropertyName("idLocalidad")]
    public Guid IdLocalidad { get; set; }

    [JsonPropertyName("idCliente")]
    public Guid IdCliente { get; set; }
 
}
