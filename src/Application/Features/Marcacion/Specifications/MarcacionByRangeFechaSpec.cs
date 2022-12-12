using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class MarcacionByRangeFechaSpec : Specification<MarcacionColaborador>
{
    public MarcacionByRangeFechaSpec(string identificacion, DateTime FechaDesde, DateTime FechaHasta)
    {
        Query.Where(x => x.FechaCreacion.Date >= FechaDesde.Date && x.FechaCreacion.Date <= FechaHasta && x.LocalidadColaborador.Colaborador.Identificacion == identificacion)
            .Include(x => x.LocalidadColaborador)
            .ThenInclude(x => x.Localidad)
            .Include(x => x.LocalidadColaborador)
            .ThenInclude(x => x.Colaborador);
    }
}
