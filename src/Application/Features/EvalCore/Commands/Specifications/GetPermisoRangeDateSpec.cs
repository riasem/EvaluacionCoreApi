using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.EvalCore.Commands.Specifications;

public class GetPermisoRangeDateSpec : Specification<SolicitudPermiso>
{
    public GetPermisoRangeDateSpec(int codigo, DateTime? marcacionEntrada)
    {
        Query.Where(c => c.IdBeneficiario == codigo
        && c.FechaDesde.Date >= marcacionEntrada.Value.Date && c.FechaHasta.Date <= marcacionEntrada.Value.Date);
/*        && marcacionEntrada.Value.TimeOfDay >= DateTime.Parse(c.HoraInicio).TimeOfDay && marcacionEntrada.Value.TimeOfDay >= DateTime.Parse(c.HoraFin).TimeOfDay)*/;
    }
}
