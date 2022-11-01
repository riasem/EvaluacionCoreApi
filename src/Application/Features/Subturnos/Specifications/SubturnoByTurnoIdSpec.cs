using Ardalis.Specification;
using EvaluacionCore.Domain.Entities;

namespace EvaluacionCore.Application.Features.Subturnos.Specifications;

public class SubturnoByTurnoIdSpec : Specification<SubTurno>
{
    public SubturnoByTurnoIdSpec(Guid idTurno)
    {
        Query.Where((p => p.IdTurno == idTurno && p.EsSubturnoPrincipal == true));
            
    }
}