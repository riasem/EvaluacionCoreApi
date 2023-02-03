using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.ControlAsistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class ControlAsistenciaSolicitudByIdDetalle : Specification<ControlAsistenciaSolicitudes>
{
    public ControlAsistenciaSolicitudByIdDetalle(int idControlAsistenciaDet)
    {
        try
        {
            Query.Where(p => p.IdControlAsistenciaDet == idControlAsistenciaDet);
        }
        catch (Exception ex)
        {

            throw;
        }
            
    }
}