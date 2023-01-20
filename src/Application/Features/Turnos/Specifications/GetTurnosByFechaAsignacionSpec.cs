using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications
{
    public class GetTurnosByFechaAsignacionSpec : Specification<TurnoColaborador>
    {
        public GetTurnosByFechaAsignacionSpec(DateTime fechaAsignacion)
        {
            Query.Where(p => p.FechaAsignacion.Date == fechaAsignacion.Date && p.Estado == "A");
        }
    }
}
