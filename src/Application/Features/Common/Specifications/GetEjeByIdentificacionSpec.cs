using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;

namespace EvaluacionCore.Application.Features.Common.Specifications
{
    public class GetEjeByIdentificacionSpec: Specification<CargoEje>
    {
        public GetEjeByIdentificacionSpec(string Identificacion)
        {
            Query.Where(p => p.Identificacion == Identificacion).Take(1);
        }
    }
}
