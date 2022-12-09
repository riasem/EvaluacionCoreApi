using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Vacaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.EvalCore.Commands.Specifications;

public class GetVacacionRangeDateSpec : Specification<SolicitudVacacion>
{
    public GetVacacionRangeDateSpec(int codigo, DateTime? fecha)
    {
        Query.Where(c => c.IdBeneficiario == codigo && fecha.Value.Date >= c.FechaDesde.Date && fecha.Value.Date <= c.FechaHasta.Date);
    }
}
