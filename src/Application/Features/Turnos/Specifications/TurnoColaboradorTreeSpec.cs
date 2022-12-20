using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoColaboradorTreeSpec : Specification<TurnoColaborador>
{
    public TurnoColaboradorTreeSpec()
    {
        Query.
            Include(p => p.Turno)
            .Include(p => p.Turno);
    }
}