using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Specifications
{
    public class GetLocationByColaboradorSpec : Specification<LocalidadColaborador>
    {
        public GetLocationByColaboradorSpec(string Identificacion)
        {
            Query.Where(p => p.Colaborador.Identificacion == Identificacion && p.Estado == "A")
                .Include(p => p.Localidad);
        }
    }
}
