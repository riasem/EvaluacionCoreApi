using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion
{
    public class GetComboBitacoraMarcacionRequest
    {
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Identificacion { get; set; }
    }
}
