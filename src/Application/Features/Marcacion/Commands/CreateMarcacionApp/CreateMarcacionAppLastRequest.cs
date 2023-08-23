using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionApp;

public class CreateMarcacionAppLastRequest
{
    public string Identificacion { get; set; }

    //public Guid LocalidadId { get; set; }
    public string DispositivoId { get; set; }

    public IFormFile Adjunto { get; set; }

    //public string Base64 { get; set; } = string.Empty;
    //public string Nombre { get; set; } = string.Empty;
    //public string Extension { get; set; } = string.Empty;
}
