using Ardalis.Specification;
using EnrolApp.Domain.Entities.Horario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class MarcacionByUserIdSpec : Specification<CheckInOut>
{
    public MarcacionByUserIdSpec(int userId)
    {
        Query.Where(p => p.UserId == userId && p.CheckTime.Date == DateTime.Now.Date);
    }
}
