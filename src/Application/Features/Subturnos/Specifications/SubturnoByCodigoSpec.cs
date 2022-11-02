using Ardalis.Specification;
using EvaluacionCore.Domain.Entities;

namespace EvaluacionCore.Application.Features.Subturnos.Specifications;

public class SubturnoByCodigoSpec : Specification<SubTurno>
{
    public SubturnoByCodigoSpec(string codigoSubturno)
    {
        Query.Where((p => p.CodigoSubturno == codigoSubturno));
            
    }
}