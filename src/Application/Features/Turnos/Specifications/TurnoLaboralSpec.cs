using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoLaboralSpec : Specification<Turno>
{
    public TurnoLaboralSpec()
    {
        Query.Where((p => p.IdTurnoPadre == null));   
    }
}