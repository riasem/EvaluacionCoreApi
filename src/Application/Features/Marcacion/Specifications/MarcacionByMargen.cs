using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Specifications;

public class MarcacionByMargen : Specification<MarcacionColaborador>
{
    public MarcacionByMargen(DateTime mPrevio, DateTime mPosterior, string tipoMarcacion, Guid idColaborador)
    {
        if (tipoMarcacion == "E")
        {
            Query.Where(p => p.MarcacionEntrada >= mPrevio && p.MarcacionEntrada <= mPosterior 
            && p.LocalidadColaborador.Colaborador.Id == idColaborador)
                .Include(x => x.LocalidadColaborador).ThenInclude(x => x.Colaborador);
        }
        else
        {
            Query.Where(p => p.MarcacionSalida >= mPrevio && p.MarcacionSalida <= mPosterior
            && p.LocalidadColaborador.Colaborador.Id == idColaborador)
                .Include(x => x.LocalidadColaborador).ThenInclude(x => x.Colaborador);
        }

    }
}
