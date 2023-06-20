using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateCabeceraLog;

public class CreateCabeceraLogRequest
{
    public int TotalMarcacion { get; set; }

    public DateTime FechaInicio { get; set; }

    public string IdentificacionInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string IdentificacionFin { get; set; }

    public DateTime FechaSincronizacion { get; set; }


}


