using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class MarcacionByColaborador: Specification<MarcacionColaborador>
{
    public MarcacionByColaborador(Guid? identificacion, DateTime? fecha)
    {
        Query
            .Where(p => p.TurnoColaborador.Colaborador.Id == identificacion && p.EstadoProcesado == false && p.FechaCreacion.Date == fecha.Value.Date)
            .Include(p => p.TurnoColaborador)
            .ThenInclude(p => p.Colaborador)
            .Include(p => p.TurnoColaborador)
            .ThenInclude(p => p.Turno);

    }
}

