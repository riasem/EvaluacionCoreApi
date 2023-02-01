using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Common.Specifications;

public class GetColaboradorByCodBiometrico : Specification<Cliente>
{
    public GetColaboradorByCodBiometrico(string codBiometrico)
    {
        Query.Where(x => x.CodigoConvivencia == codBiometrico).Take(1);
    }
}
