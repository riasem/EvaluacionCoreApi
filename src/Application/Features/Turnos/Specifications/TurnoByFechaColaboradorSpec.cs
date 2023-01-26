using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class TurnoByFechaColaboradorSpec : Specification<TurnoColaborador>
{
    public TurnoByFechaColaboradorSpec(DateTime fechaDesde, DateTime fechaHasta, Guid idColaborador)
    {
        Query.Where(e => e.FechaAsignacion.Date >= fechaDesde.Date && e.FechaAsignacion.Date <= fechaHasta.Date && e.Estado == "A" && e.IdColaborador == idColaborador)
              .Include(p => p.Turno)
              .ThenInclude(p => p.ClaseTurno);
    }

}
