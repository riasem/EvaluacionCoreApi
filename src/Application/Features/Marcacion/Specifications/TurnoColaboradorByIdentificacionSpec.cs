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
        Query.Where(e => e.FechaAsignacion >= FechaDesde && e.FechaAsignacion <= FechaHasta && e.Estado == "A")
             .Include(p => p.Colaborador).Where(p => p.Colaborador.Identificacion == identificacion)
             .Include(p => p.Turno);

    }
}