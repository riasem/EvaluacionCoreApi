using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Permisos;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class NovedadRecordatorioCabByPeriodoSpec : Specification<NovedadRecordatorioCab>
{
    public NovedadRecordatorioCabByPeriodoSpec(string periodo, DateTime fechaEvaluacion)
    {
        //SE CONSULTA EL PERIODO ACTUAL
        Query.Where(p => p.Periodo == periodo && p.FechaEvaluacion == fechaEvaluacion)
            .OrderByDescending(p => p.FechaEvaluacion);

    }
}