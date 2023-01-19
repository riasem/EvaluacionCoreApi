using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Marcaciones;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class MarcacionByColaboradorAndTime: Specification<AccMonitoLogRiasem>
{
    public MarcacionByColaboradorAndTime(string codBiometrico, DateTime fechaMarcacion)
    {
        Query
            .Where(p => p.Pin == codBiometrico && p.Time.Value.Date == fechaMarcacion.Date && p.Device_Id == 999);
            

    }
}

