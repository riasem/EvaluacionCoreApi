using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Marcaciones;

namespace EnrolApp.Application.Features.RecordatorioTurnos.Specifications;

public class AlertasNovedadSpec : Specification<AlertasNovedadMarcacion>
{
    public AlertasNovedadSpec()
    {
        Query.Where(p => p.FechaModificacion == null || p.UsuarioModificacion == null);

    }
}
