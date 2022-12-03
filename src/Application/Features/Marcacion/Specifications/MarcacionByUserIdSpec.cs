using Ardalis.Specification;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Domain.Entities.Marcaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class MarcacionByUserIdSpec : Specification<AccMonitorLog>
{
    public MarcacionByUserIdSpec(string codigo)
    {
        Query.Where(p => p.Pin == codigo && p.Time.Date == DateTime.Now.Date);
    }
}
