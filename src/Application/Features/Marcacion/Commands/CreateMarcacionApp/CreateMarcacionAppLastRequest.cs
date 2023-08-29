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
    public string DispositivoId { get; set; }
    public IFormFile Adjunto { get; set; }
    public string TipoComunicacion { get; set; }
}
