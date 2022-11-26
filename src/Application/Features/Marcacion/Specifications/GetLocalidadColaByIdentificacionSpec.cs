using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class GetLocalidadColaByIdentificacionSpec : Specification<LocalidadColaborador>
{
    public GetLocalidadColaByIdentificacionSpec(string identificacion)
    {
        Query
            .Include(p => p.Colaborador).Where(p => p.Colaborador.Identificacion == identificacion);
           
    }
}
