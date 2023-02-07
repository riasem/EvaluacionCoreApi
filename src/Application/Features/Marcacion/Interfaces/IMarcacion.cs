using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb;
using EvaluacionCore.Application.Features.Marcacion.Dto;

namespace EvaluacionCore.Application.Features.Marcacion.Interfaces;

public interface IMarcacion
{
    Task<ResponseType<MarcacionResponseType>> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken);

    Task<ResponseType<List<ConsultaRecursoType>>> ConsultarRecursos(Guid IdCliente, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken);

    Task<ResponseType<string>> CreateMarcacionWeb(CreateMarcacionWebRequest Request, CancellationToken cancellationToken);
}
