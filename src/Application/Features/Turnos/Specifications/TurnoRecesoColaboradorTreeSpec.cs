using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoRecesoColaboradorTreeSpec : Specification<TurnoColaborador>
{
    public TurnoRecesoColaboradorTreeSpec(string Identificacion, DateTime fechaAsignacion, Guid? TurnoPadre)
    {
        Query
            .Include(p => p.Turno)
            .Where(e => e.FechaAsignacion == fechaAsignacion && e.Turno.IdTurnoPadre == TurnoPadre && e.Turno.Estado == "A")
            .Include(p => p.Colaborador)
            .Where(f => f.Colaborador.Identificacion == Identificacion);
    }
}