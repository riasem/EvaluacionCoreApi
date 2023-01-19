using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Calendario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Calendario.Specifications;

public class GetCantonById : Specification<Canton>
{
    public GetCantonById(Guid idCanton)
    {
        Query.Where(p => p.Id == idCanton)
            .Include(p => p.Provincia)
            .ThenInclude(p => p.Pais);

    }
}
