using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoByCodigoSpec : Specification<Turno>
{
    public TurnoByCodigoSpec(string codigoTurno)
    {
        Query.Where((p => p.CodigoTurno == codigoTurno));
            
    }
}