using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial;

public class AuthenticationFacialLastRequest
{
    //public string Base64 { get; set; } = string.Empty;
    //public string Nombre { get; set; } = string.Empty;
    //public string Extension { get; set; } = string.Empty;

    public string FacialPersonUid { get; set; } = string.Empty;
    public string Identificacion { get; set; } = string.Empty;
    public IFormFile AdjuntoFiles { get; set; }
}
