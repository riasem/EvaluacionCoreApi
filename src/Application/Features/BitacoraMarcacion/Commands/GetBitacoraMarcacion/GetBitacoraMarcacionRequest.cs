using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion
{
    public class GetBitacoraMarcacionRequest
    {
        public string Suscriptor { get; set; }
        public string CodUdn { get; set; }
        public string CodArea { get; set; }
        public string CodSubcentro { get; set; }
        public string CodMarcacion { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
    }
}
