using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class GetListadoAdministradorByLocalidadSpec : Specification<LocalidadAdministrador>
{
    public GetListadoAdministradorByLocalidadSpec(string codigoLocalidad)
    {
        Query.Where(p => p.Localidad.Codigo == codigoLocalidad && p.Estado == "A" && p.Localidad.Estado == "A")
            .Include(p => p.Localidad)
                .ThenInclude(p => p.Empresa)
        .OrderBy(p => p.Identificacion);
    }
}