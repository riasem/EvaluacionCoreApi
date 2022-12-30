using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoColaboradorTreeSpec : Specification<TurnoColaborador>
{
    public TurnoColaboradorTreeSpec(DateTime fechaDesde, DateTime fechaHasta)
    {
        Query.
            Include(p => p.Turno)
            .Include(p => p.Turno)
            .Where(e => e.FechaAsignacion >= fechaDesde && e.FechaAsignacion <= fechaHasta);
    }
}