using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class MarcacionByColaborador: Specification<MarcacionColaborador>
{
    public MarcacionByColaborador(Guid? identificacion)
    {
        Query
            .Where(p => p.TurnoColaborador.Colaborador.Id == identificacion)
            .Include(p => p.TurnoColaborador)
            .ThenInclude(p => p.Colaborador);

    }
}
