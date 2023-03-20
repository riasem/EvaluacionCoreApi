using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoByIdPadreSpec : Specification<Turno>
{
    public TurnoByIdPadreSpec(Guid id)
    {
        Query.Where(p => p.IdTurnoPadre == id);
            
    }
}