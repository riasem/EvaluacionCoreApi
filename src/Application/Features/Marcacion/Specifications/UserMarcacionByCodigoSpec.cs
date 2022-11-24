using Ardalis.Specification;
using EnrolApp.Domain.Entities.Horario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class UserMarcacionByCodigoSpec : Specification<UserInfo>
{
    public UserMarcacionByCodigoSpec(string Codigo)
    {
        Query.Where(p => p.Badgenumber == Codigo);
    }
}
