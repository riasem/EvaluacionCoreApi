using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Common.Specifications
{
    public class GetTurnoColaboradorByTurnoAsignado : Specification<TurnoColaborador>
    {
        public GetTurnoColaboradorByTurnoAsignado(Guid idColaborador, Guid idTurno, DateTime fechaAsignacion)
        {
            Query.Where(x => x.IdTurno == idTurno && x.IdColaborador == idColaborador && x.FechaAsignacion == fechaAsignacion && x.Estado == "A" && x.Turno.IdTurnoPadre == null);
        }
    }
}



