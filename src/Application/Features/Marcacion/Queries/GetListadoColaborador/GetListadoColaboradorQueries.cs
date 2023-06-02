using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Marcacion.Queries.GetListadoColaborador;

public record GetListadoColaboradorQueries(string IdentificacionSesion) : IRequest<ResponseType<List<ColaboradorByLocalidadResponseType>>>;
public class GetListadoColaboradorQueriesHandler : IRequestHandler<GetListadoColaboradorQueries, ResponseType<List<ColaboradorByLocalidadResponseType>>>
{
    private readonly IMarcacion _repositoryAsync;

    public GetListadoColaboradorQueriesHandler(IMarcacion repositoryAsync)
    {
        _repositoryAsync = repositoryAsync;
    }

    public async Task<ResponseType<List<ColaboradorByLocalidadResponseType>>> Handle(GetListadoColaboradorQueries request, CancellationToken cancellationToken)
    {
        var resultObject = await _repositoryAsync.ListadoColaboradorByLocalidad(request.IdentificacionSesion, cancellationToken);

        return resultObject;
    }
}
