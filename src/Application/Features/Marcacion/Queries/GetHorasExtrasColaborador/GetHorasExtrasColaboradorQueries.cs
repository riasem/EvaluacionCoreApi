using AutoMapper;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Marcacion.Queries.GetHorasExtrasColaborador;
public record GetHorasExtrasColaboradorQueries(HorasExtrasColaboradorRequest request, string IdentificacionSesion) : IRequest<ResponseType<List<HorasExtrasColaboradorResponse>>>;

public class GetHorasExtrasColaboradorQueriesHandler : IRequestHandler<GetHorasExtrasColaboradorQueries, ResponseType<List<HorasExtrasColaboradorResponse>>>
{
    private readonly IMapper _mapper;
    private readonly IMarcacion _repository;

    public GetHorasExtrasColaboradorQueriesHandler(IMapper mapper, IMarcacion repositoryAsync)
    {
        _mapper = mapper;
        _repository = repositoryAsync;
    }

    public async Task<ResponseType<List<HorasExtrasColaboradorResponse>>> Handle(GetHorasExtrasColaboradorQueries request, CancellationToken cancellationToken)
    {

        var objResult = await _repository.GetConsultaHorasExtrasColaborador(request.IdentificacionSesion, request.request.FechaDesde, request.request.FechaHasta, cancellationToken);

        return objResult;
    }
}