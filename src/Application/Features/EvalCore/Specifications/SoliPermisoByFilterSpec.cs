using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Permisos;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class SoliPermisoByFilterSpec : Specification<SolicitudPermiso>
{
    public SoliPermisoByFilterSpec(int codBeneficiario, DateTime fechaNovedad)
    {
        try
        {
            Query.Where(p => p.IdBeneficiario == codBeneficiario /*&&
                            (fechaNovedad >= p.FechaDesde.AddHours(int.Parse(p.HoraInicio.Substring(1, 2))).AddMinutes(int.Parse(p.HoraInicio.Substring(4, 5)))) &&
                            (fechaNovedad <= p.FechaHasta.AddHours(int.Parse(p.HoraFin.Substring(1, 2))).AddMinutes(int.Parse(p.HoraFin.Substring(4, 5))))*/)
                .Include(e => e.EstadoTarea)
                .Where(e => e.EstadoTarea.Codigo == "APROBADA");
        }
        catch (Exception ex)
        {

            throw;
        }
            
    }
}