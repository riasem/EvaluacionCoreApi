using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Calendario.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Calendario.Interfaces;

public interface ICalendario
{
    Task<ResponseType<List<DiasFeriadoIdentificacionResponseType>>> GetDiasFeriadosByIdentificacion(string identificacion, DateTime fecha);

    Task<ResponseType<List<DiasFeriadosResponseType>>> GetDiasFeriadosByCanton(Guid idCanton, DateTime fecha);
}
