using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Marcaciones;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class NovedadesMarcacionOfflineSpec : Specification<MonitorLogFileOffline>
{
    public NovedadesMarcacionOfflineSpec(string identificacion, DateTime? fechaDesde, DateTime? fechaHasta)
    {
        if (fechaDesde == null && fechaHasta == null)
        {
            Query.Where(x => x.Identificacion == (string.IsNullOrEmpty(identificacion) ? x.Identificacion : identificacion) 
            && x.EstadoReconocimiento != "CORRECTO");
        }
        else if (fechaDesde != null && fechaHasta != null)
        {
            Query.Where(x => x.Identificacion == (string.IsNullOrEmpty(identificacion) ? x.Identificacion : identificacion) &&
            (x.Time.Date >= (fechaDesde == null ? x.Time.Date : fechaDesde.Value.Date)
            && x.Time.Date <= (fechaHasta == null ? x.Time.Date : fechaHasta.Value.Date)) 
            && x.EstadoReconocimiento != "CORRECTO");
        }
    }

}
