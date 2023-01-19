using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoColaboradorTreeSpec : Specification<TurnoColaborador>
{
    public TurnoColaboradorTreeSpec(string Identificacion, DateTime fechaAsignacion)
    {
        try
        {
            Query
                .Include(p => p.Turno)
                .Where(e => e.FechaAsignacion.Date == fechaAsignacion.Date && e.Turno.IdTurnoPadre == null && e.Turno.Estado == "A")
                .Include(p => p.Colaborador)
                .Where(f => f.Colaborador.Identificacion == Identificacion)
                .Include(p => p.Turno.ClaseTurno);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}