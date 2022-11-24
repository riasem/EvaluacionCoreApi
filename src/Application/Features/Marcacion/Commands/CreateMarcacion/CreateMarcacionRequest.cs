
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;

public class CreateMarcacionRequest
{
    [JsonPropertyName("codigoEmpleado")]
    public string CodigoEmpleado { get; set; }

    [JsonPropertyName("dispositivoId")]
    public string DispositivoId { get; set; }

    [JsonPropertyName("localidadId")]
    public Guid LocalidadId { get; set; }


}
