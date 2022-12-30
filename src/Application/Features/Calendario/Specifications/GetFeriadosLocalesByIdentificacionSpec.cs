using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Calendario;
using EvaluacionCore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Calendario.Specifications;

public class GetFeriadosLocalesByIdentificacionSpec : Specification<CalendarioLocal>
{
    public GetFeriadosLocalesByIdentificacionSpec(Guid idCanton, DateTime fecha)
    {
        Query.Where(p => p.IdCanton == idCanton && p.FechaFestiva.Date == fecha.Date);
            
    }
}
