using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class GetListadoPersonalByLocalidadSpec : Specification<LocalidadColaborador>
{
    public GetListadoPersonalByLocalidadSpec(string Identificacion)
    {
        Query.Where(p => p.Localidad.LocalidadColaboradores.Where(x => x.Colaborador.Identificacion == Identificacion).Any())
            .Include(p => p.Colaborador)
            .ThenInclude(p => p.ImagenPerfil)
            .Include(p => p.Localidad.Empresa);
            
            //.ThenInclude(p => p.Areas.Where(p => p.EmpresaId == p.Empresa.Id ))
            //.ThenInclude(p => p.Departamentos)
            //.ThenInclude(p => p.Cargos);

        

    }
}