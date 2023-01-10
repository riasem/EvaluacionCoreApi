using Ardalis.Specification;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Domain.Entities.Marcaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class MarMonitorByRangeFechaSpec : Specification<AccMonitoLogRiasem>
{
    public MarMonitorByRangeFechaSpec(string codigo, DateTime fechaDesde, DateTime fechaHasta)
    {
        Query.Where(x => x.Pin == codigo && x.Time.Value.Date >= fechaDesde.Date && x.Time.Value.Date <= fechaHasta.Date);
    }
}
