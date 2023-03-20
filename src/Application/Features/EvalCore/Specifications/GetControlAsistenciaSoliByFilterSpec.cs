using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.ControlAsistencia;

namespace EvaluacionCore.Application.Features.Turnos.Specifications;

public class GetControlAsistenciaSoliByFilterSpec : Specification<ControlAsistenciaSolicitudes_V>
{
    public GetControlAsistenciaSoliByFilterSpec(string Udn, string Area, string Departamento, string Periodo, string Suscriptor)
    {
        if (Departamento is null && Suscriptor is not null && Area is not null)
        {
            Query.Where(p => p.Periodo == Periodo && p.Udn == Udn && p.Area == Area && p.Identificacion == Suscriptor);
        }
        else if (Suscriptor is null && Departamento is not null && Area is not null)
        {
            Query.Where(p => p.Periodo == Periodo && p.Udn == Udn && p.Area == Area && p.SubcentroCosto == Departamento);
        }
        else if (Area is null && Suscriptor is not null && Departamento is not null)
        {
            Query.Where(p => p.Periodo == Periodo && p.Udn == Udn && p.SubcentroCosto == Departamento && p.Identificacion == Suscriptor);
        }
        else if (Area is null && Suscriptor is null && Departamento is not null)
        {
            Query.Where(p => p.Periodo == Periodo && p.Udn == Udn && p.SubcentroCosto == Departamento);
        }
        else if (Area is null && Suscriptor is not null && Departamento is null)
        {
            Query.Where(p => p.Periodo == Periodo && p.Udn == Udn && p.Identificacion == Suscriptor);
        }
        else if (Area is not null && Suscriptor is not null && Departamento is not null)
        {
            Query.Where(p => p.Periodo == Periodo && p.Udn == Udn && p.Area == Area && p.SubcentroCosto == Departamento && p.Identificacion == Suscriptor);
        }
        else
        {
            Query.Where(p => p.Periodo == Periodo && p.Udn == Udn && p.Area == Area);
        }
    }
}