using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Marcaciones;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class NovedadesMarcacionOfflineSpec : Specification<MarcacionOffline>
{
    public NovedadesMarcacionOfflineSpec(string Colaborador, DateTime? fechaDesde, DateTime? fechaHasta,int? deviceId, string codUdn)
    {
        if (fechaDesde != null && fechaHasta != null)
        {
            Query.Where(p => string.IsNullOrEmpty(Colaborador) ? p.Identificacion == p.Identificacion : (p.Empleado.Contains(Colaborador) || p.Identificacion == Colaborador))
          .Where(p => p.DeviceId == (deviceId == null ? p.DeviceId : deviceId))
          .Where(p => p.CodUdn == (string.IsNullOrEmpty(codUdn) ? p.CodUdn : codUdn))
          .Where(p => p.Time.Value.Date >= fechaDesde.Value.Date && p.Time.Value.Date <= fechaHasta.Value.Date);
        }
        else
        {
            Query.Where(p => string.IsNullOrEmpty(Colaborador) ? p.Identificacion == p.Identificacion : (p.Empleado.Contains(Colaborador) || p.Identificacion == Colaborador))
                .Where(p => p.DeviceId == (deviceId == null ? p.DeviceId : deviceId))
                .Where(p => p.CodUdn == (string.IsNullOrEmpty(codUdn) ? p.CodUdn : codUdn));
        }

    }

}
