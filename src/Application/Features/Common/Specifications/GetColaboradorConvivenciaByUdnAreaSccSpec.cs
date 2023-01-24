using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;

namespace EvaluacionCore.Application.Features.Common.Specifications
{
    public class GetColaboradorConvivenciaByUdnAreaSccSpec : Specification<ColaboradorConvivencia>
    {
        public GetColaboradorConvivenciaByUdnAreaSccSpec(string Udn, string Area, string Scc, string Colaborador)
        {
            Query.Where(p => Colaborador == "" ? p.Identificacion == p.Identificacion : (p.Empleado.Contains(Colaborador) || p.Identificacion == Colaborador || p.CodigoBiometrico.Contains(Colaborador)))
                .Where(p => p.CodSubcentroCosto == (Scc == "" ? p.CodSubcentroCosto : Scc))
                .Where(ar => ar.CodArea == (Area == "" ? ar.CodArea : Area))
                .Where(ud => ud.CodUdn == (Udn == "" ? ud.CodUdn : Udn));
        }
    }
}
