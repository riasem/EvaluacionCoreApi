using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;

namespace EvaluacionCore.Application.Features.Common.Specifications;

public class GetColaboradorByJefe : Specification<Cliente>
{
    public GetColaboradorByJefe(Guid idClientePadre)
    {
        Query.Where(e => e.ClientePadreId == idClientePadre);
    }
}
