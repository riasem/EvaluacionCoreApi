using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class GetLocalidadByIdSpec : Specification<Localidad>
{
    public GetLocalidadByIdSpec(Guid id, string codigoEmpleado)
    {
        Query.Where(p => p.Id == id && p.LocalidadColaboradores.Where(x => x.Colaborador.CodigoConvivencia == codigoEmpleado).Any())
            .Include(p => p.LocalidadColaboradores.Where(x => x.Colaborador.CodigoConvivencia == codigoEmpleado))
            .ThenInclude(p => p.Colaborador);
    }
}
