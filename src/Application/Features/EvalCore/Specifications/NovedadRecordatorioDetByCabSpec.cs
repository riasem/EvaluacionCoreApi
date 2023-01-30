using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Permisos;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class NovedadRecordatorioDetByCabSpec : Specification<NovedadRecordatorioDet>
{
    public NovedadRecordatorioDetByCabSpec(Guid Id)
    {
        //SE CONSULTA EL PERIODO ACTUAL
        Query.Where(p => p.IdNovedadRecordatorioCab == Id);

    }
}