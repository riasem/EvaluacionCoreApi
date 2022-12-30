using AutoMapper;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Queries.GetRecurso;

public record GetRecursoQueries(Guid IdCliente, DateTime FechaDesde, DateTime FechaHasta) : IRequest<ResponseType<List<ConsultaRecursoType>>>;

public class GetRecursoQueriesHandler : IRequestHandler<GetRecursoQueries, ResponseType<List<ConsultaRecursoType>>>
{
    private readonly IMapper _mapper;
    private readonly IMarcacion _repositoryAsync;

    public GetRecursoQueriesHandler(IMapper mapper, IMarcacion repositoryAsync)
    {
        _mapper = mapper;
        _repositoryAsync = repositoryAsync;
    }

    public async Task<ResponseType<List<ConsultaRecursoType>>> Handle(GetRecursoQueries request, CancellationToken cancellationToken)
    {
        var objResult = await _repositoryAsync.ConsultarRecursos(request.IdCliente, request.FechaDesde, request.FechaHasta, cancellationToken);

        return objResult;

    }


}
