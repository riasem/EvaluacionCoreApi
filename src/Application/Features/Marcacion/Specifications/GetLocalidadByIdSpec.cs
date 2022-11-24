using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class GetLocalidadByIdSpec : Specification<LocalidadColaborador>
{
    public GetLocalidadByIdSpec(Guid id, string codigoEmpleado)
    {
        Query.Where(p => p.Id == id)
            .Include(p => p.Colaborador).Where(p => p.Colaborador.CodigoConvivencia == codigoEmpleado);
           
    }
}
