using Ardalis.Specification;
using EnrolApp.Domain.Entities.Horario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.EvalCore.Commands.Specifications;

public class GetMarcacionesByRangeDateSpec : Specification<CheckInOut>
{
    public GetMarcacionesByRangeDateSpec(int userId, DateTime? fechaDesde, DateTime? fechaHasta)
    {
        Query.Where(x => x.UserId == userId && x.CheckTime.Date >= fechaDesde.Value.Date && x.CheckTime.Date <= fechaHasta.Value.Date)
            .OrderByDescending(x => x.CheckTime);


    }

}
