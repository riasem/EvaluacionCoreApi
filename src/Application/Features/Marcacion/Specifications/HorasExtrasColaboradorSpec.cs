using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Marcaciones;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;
public class HorasExtrasColaboradorSpec : Specification<MarcacionOffline>
{
    public HorasExtrasColaboradorSpec(string? IdentificacionSesion, DateTime? fechaDesde, DateTime? fechaHasta)
    {
        if (fechaDesde != null && fechaHasta != null)
        {
            Query.Where(p => string.IsNullOrEmpty(IdentificacionSesion) ? p.Identificacion == p.Identificacion : (p.Empleado.Contains(IdentificacionSesion) || p.Identificacion == IdentificacionSesion))
          .Where(p => p.Time.Value.Date >= fechaDesde.Value.Date && p.Time.Value.Date <= fechaHasta.Value.Date)
          .Where(p => p.EstadoReconocimiento != "CORRECTO");
        }
        else
        {
            Query.Where(p => string.IsNullOrEmpty(IdentificacionSesion) ? p.Identificacion == p.Identificacion : (p.Empleado.Contains(IdentificacionSesion) || p.Identificacion == IdentificacionSesion))
                .Where(p => p.EstadoReconocimiento != "CORRECTO"); ;
        }

    }

}