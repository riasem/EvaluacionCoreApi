using Ardalis.Specification;
using EvaluacionCore.Domain.Entities;

namespace EnrolApp.Application.Features.Clients.Specifications;

public class ClienteByCodigoSpec : Specification<Cliente>
{
    public ClienteByCodigoSpec(string Codigo)
    {
        Query.Where(p => p.CodigoIntegracion == Codigo)
            .Include(p => p.CargoId);
           
    }
}
