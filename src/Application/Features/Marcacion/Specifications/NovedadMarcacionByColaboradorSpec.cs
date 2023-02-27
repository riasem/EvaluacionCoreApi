using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Marcaciones;

namespace EnrolApp.Application.Features.Marcacion.Specifications;

public class NovedadMarcacionByColaboradorSpec : Specification<AlertasNovedadMarcacion>
{
    public NovedadMarcacionByColaboradorSpec(int CodigoBiometrico, DateTime FDesde, DateTime FHasta)
    {
        Query.Where(p => p.UsuarioMarcacion == CodigoBiometrico && p.FechaMarcacion >= FDesde && p.FechaMarcacion <= FHasta);

    }
}
