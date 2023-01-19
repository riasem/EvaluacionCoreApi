using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;

namespace EvaluacionCore.Application.Features.Vacaciones.Specifications;

public class ClientesByEmpresaSpec : Specification<Cliente>
{
    public ClientesByEmpresaSpec()
    {
        Query.Where(p => p.Estado == p.Estado);
            //.Include(e => e.Cargo)
            //.ThenInclude(e => e.Departamento)
            //.Include(ec => ec.Cargo)
            //.ThenInclude(e => e.Departamento.Area)
            //.Include(ec => ec.Cargo)
            //.ThenInclude(e => e.Departamento.Area.Empresa);
    }
}
