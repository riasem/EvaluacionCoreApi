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

public class GetFeriadosNacionalByIdentificacionSpec : Specification<CalendarioNacional>
{
    public GetFeriadosNacionalByIdentificacionSpec(Guid idPais, DateTime fecha)
    {
        Query.Where(p => p.IdPais == idPais && p.FechaFestiva.Date == fecha.Date);
            
    }
}
