using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;

public class CargaMarcacionesExcelRequest
{
    public int Order { get; set; }

    public string Identificacion { get; set; }

    public DateTime Time { get; set; }

    public string IdentificacionDispositivo { get; set; }


}
