using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class TurnoColaboradorByIdentificacionSpec : Specification<TurnoColaborador>
{
    public TurnoColaboradorByIdentificacionSpec(string identificacion, DateTime FechaDesde, DateTime FechaHasta)
    {
        Query.Where(e => e.FechaAsignacion.Date >= FechaDesde.Date && e.FechaAsignacion.Date <= FechaHasta.Date && e.Estado == "A")
             .Include(p => p.Colaborador).Where(p => p.Colaborador.Identificacion == identificacion)
             .Include(p => p.Turno)
             .ThenInclude(p => p.ClaseTurno)/*.Where(p => p.Turno.ClaseTurno.CodigoClaseturno == "LABORA")*/;

    }
}