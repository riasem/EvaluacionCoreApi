using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoColaboradorByFilterSpec : Specification<TurnoColaborador>
{
    public TurnoColaboradorByFilterSpec(Guid Colaborador, DateTime FechaDesde, DateTime FechaHasta)
    {
        Query.Where(e =>  e.FechaAsignacion > FechaDesde
                    && e.FechaAsignacion < FechaHasta)
             .Include(p => p.Colaborador).Where(p => p.Colaborador.ClientePadreId == Colaborador);
            
    }
}