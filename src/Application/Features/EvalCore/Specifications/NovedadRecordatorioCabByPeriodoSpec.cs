using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Permisos;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class NovedadRecordatorioCabByPeriodoSpec : Specification<NovedadRecordatorioCab>
{
    public NovedadRecordatorioCabByPeriodoSpec(string periodo)
    {
        //SE CONSULTA EL PERIODO ACTUAL
        Query.Where(p => p.Periodo == periodo);

    }
}