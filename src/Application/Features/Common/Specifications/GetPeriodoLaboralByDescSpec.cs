using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using System.Globalization;

namespace EvaluacionCore.Application.Features.Common.Specifications
{
    public class GetPeriodoLaboralByDescSpec : Specification<PeriodosLaborales>
    {
        public GetPeriodoLaboralByDescSpec(string Udn, string DescPeriodo)
        {
            Query.Where(ud => ud.Udn == (Udn == "" ? ud.Udn : Udn) && ud.DesPeriodo == DescPeriodo);
        }
    }
}