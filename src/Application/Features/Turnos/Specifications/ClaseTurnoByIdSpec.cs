using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class ClaseTurnoByIdSpec : Specification<ClaseTurno>
{
    public ClaseTurnoByIdSpec(Guid id)
    {
        Query.Where((p => p.Id == id));
            
    }
}