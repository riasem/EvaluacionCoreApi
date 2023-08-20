using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class GetListadoAdministradorByLocalidadSpec : Specification<LocalidadAdministrador>
{
    public GetListadoAdministradorByLocalidadSpec(string Identificacion)
    {
        Query.Where(p => p.Localidad.LocalidadAdministrador.Where(x => x.Identificacion == Identificacion).Any() && p.Estado == "A" && p.Localidad.Estado == "A")
            .Include(p => p.Localidad.Empresa)
            .OrderBy(p => p.Identificacion);
    }
}