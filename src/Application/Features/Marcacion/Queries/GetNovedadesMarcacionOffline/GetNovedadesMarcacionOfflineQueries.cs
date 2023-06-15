using AutoMapper;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Marcacion.Queries.GetNovedadesMarcacionOffline;

public record GetNovedadesMarcacionOfflineQueries(string Identificacion,DateTime? FechaDesde, DateTime? FechasHasta,string IdentificacionSesion) : IRequest<ResponseType<List<NovedadesMarcacionOfflineResponse>>>;

public class GetNovedadesMarcacionOfflineQueriesHandler : IRequestHandler<GetNovedadesMarcacionOfflineQueries, ResponseType<List<NovedadesMarcacionOfflineResponse>>>
{
    private readonly IMapper _mapper;
    private readonly IMarcacion _repository;

    public GetNovedadesMarcacionOfflineQueriesHandler(IMapper mapper, IMarcacion repositoryAsync)
    {
        _mapper = mapper;
        _repository = repositoryAsync;
    }

    public async Task<ResponseType<List<NovedadesMarcacionOfflineResponse>>> Handle(GetNovedadesMarcacionOfflineQueries request, CancellationToken cancellationToken)
    {

        var objResult = await _repository.NovedadesMaracionOffline(request.Identificacion,request.FechaDesde,request.FechasHasta,request.IdentificacionSesion, cancellationToken);

        return objResult;
    }
}
