using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class GetListadoLocalidadSpec : Specification<LocalidadColaborador>
{
    public GetListadoLocalidadSpec(string Identificacion)
    {
        Query.Where(p => p.Colaborador.Identificacion == Identificacion && p.Estado == "A" && p.Localidad.Estado == "A")
            .Include(p => p.Colaborador)
            .ThenInclude(p => p.ImagenPerfil)
            .Include(p => p.Localidad.Empresa)
            .OrderBy(p => p.Colaborador.Identificacion);

    }
}