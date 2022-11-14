using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class SubClaseTurnoByIdSpec : Specification<SubclaseTurno>
{
    public SubClaseTurnoByIdSpec(Guid id)
    {
        Query.Where((p => p.Id == id));
            
    }
}