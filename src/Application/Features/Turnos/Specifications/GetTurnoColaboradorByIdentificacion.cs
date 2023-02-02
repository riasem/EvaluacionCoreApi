using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications
{
    public class GetTurnoColaboradorByIdentificacion : Specification<TurnoColaborador>
    {
        public GetTurnoColaboradorByIdentificacion(string Identificacion, DateTime FechaDesde, DateTime FechaHasta)
        {
            Query.Where(p => p.Colaborador.Identificacion == Identificacion &&
                             p.FechaAsignacion.Date >= FechaDesde.Date &&
                             p.FechaAsignacion.Date <= FechaHasta.Date &&
                             p.Turno.IdTurnoPadre == null &&
                             p.Estado == "A")
                .Include(p => p.Turno)
                .Include(p => p.Colaborador);
        }
    }
}
