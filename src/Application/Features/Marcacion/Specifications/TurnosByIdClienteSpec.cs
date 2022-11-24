using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class TurnosByIdClienteSpec : Specification<TurnoColaborador>
{
    public TurnosByIdClienteSpec(Guid idCliente)
    {
        Query.Where(p => p.IdColaborador == idCliente)
            .Include(p => p.Turno);
    }
}
