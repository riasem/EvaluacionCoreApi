using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.EvalCore.Specifications;

public class GetAtributosByRolSpec : Specification<AtributoRolSG>
{
    public GetAtributosByRolSpec(Guid rolId)
    {
        Query.Where(x => x.RolSGId == rolId)
            .Include(x => x.AtributoSG)
            .ThenInclude(x => x.FeatureSG);
    }
}
