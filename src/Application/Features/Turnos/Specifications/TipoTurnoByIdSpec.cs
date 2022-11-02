using Ardalis.Specification;
using EvaluacionCore.Domain.Entities;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TipoTurnoByIdSpec : Specification<TipoTurno>
{
    public TipoTurnoByIdSpec(Guid id)
    {
        Query.Where((p => p.Id == id));
            
    }
}