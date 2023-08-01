using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Licencia.Commands.ActualizarLicencia;

public class ActualizarLicenciaRequest
{
    public Guid IdLicencia { get; set; }
    public string TipoLicencia { get; set; }
    public string CodigoLicencia { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaUltima { get; set; }

    public DateTime FechaProxima { get; set; }
    public string ReferenciaTecnica { get; set; }

    public float CostoLicencia { get; set; }
}
