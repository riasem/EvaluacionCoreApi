using Ardalis.Specification;
using EvaluacionCore.Domain.Entities;

namespace EnrolApp.Application.Features.Clients.Specifications;

public class ClienteByIdentificacionSpec : Specification<Cliente>
{
    public ClienteByIdentificacionSpec(string Identificacion)
    {
        Query.Where(p => p.Identificacion == Identificacion);
           
    }
}
