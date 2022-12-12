using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Interfaces;

public interface IMarcacion
{
    Task<ResponseType<MarcacionResponseType>> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken);

    Task<ResponseType<List<ConsultaRecursoType>>> ConsultarRecursos(string Identificacion, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken);

}
