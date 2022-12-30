using Ardalis.Specification;
using EnrolApp.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Common;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class ClientePadreById : Specification<Cliente>
{
    public ClientePadreById(Guid clientePadreId)
    {
        Query.Where(p => p.ClientePadreId == clientePadreId);
           
    }
}
