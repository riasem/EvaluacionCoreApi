using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionOffline;

public class CreateMarcacionOfflineRequest
{
    public string Identificacion { get; set; }

    public DateTime Time { get; set; }

    public string CodigoBiometrico { get; set; }

    public IFormFile Imagen { get; set; }

}
