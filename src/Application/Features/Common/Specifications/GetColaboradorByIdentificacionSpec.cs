using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;

namespace EvaluacionCore.Application.Features.Common.Specifications;

public class GetColaboradorByIdentificacionSpec : Specification<Cliente>
{
    public GetColaboradorByIdentificacionSpec(string Identificacion)
    {
        Query.Where(x => x.Identificacion == Identificacion).Take(1)
            .Include(x => x.ImagenPerfil);
    }
}
