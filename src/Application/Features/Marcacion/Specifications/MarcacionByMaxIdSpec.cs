using Ardalis.Specification;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Domain.Entities.Marcaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class MarcacionByMaxIdSpec : Specification<AccMonitorLog>
{
    public MarcacionByMaxIdSpec()
    {
        Query.OrderByDescending(x => x.Id).Take(1);
    }
}
