using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Common.Specifications;

public class GetColaboradorByIdentificacionSpec : Specification<Cliente>
{
    public GetColaboradorByIdentificacionSpec(string Identificacion)
    {
        Query.Where(x => x.Identificacion == Identificacion).Take(1);
    }
}
