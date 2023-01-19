using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Permisos;

namespace EnrolApp.Application.Features.RecordatorioTurnos.Specifications;

public class RecordatorioByPeriodoSpec : Specification<Recordatorio>
{
    public RecordatorioByPeriodoSpec(string periodo)
    {
        Query.Where(p => p.PeriodoRecordatorio == periodo);
           
    }
}
