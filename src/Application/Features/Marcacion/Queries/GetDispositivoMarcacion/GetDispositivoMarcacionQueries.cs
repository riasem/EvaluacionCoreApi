using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Queries.GetDispositivoMarcacion;

public record GetDispositivoMarcacionQueries() : IRequest<ResponseType<List<DispositivosMarcacionResponse>>>;
public class GetDispositivoMarcacionQueriesHandler : IRequestHandler<GetDispositivoMarcacionQueries, ResponseType<List<DispositivosMarcacionResponse>>>
{
    public readonly IMarcacion _repository;
    public GetDispositivoMarcacionQueriesHandler(IMarcacion repository)
    {
        _repository = repository;
    }

    public async Task<ResponseType<List<DispositivosMarcacionResponse>>> Handle(GetDispositivoMarcacionQueries request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetDispositivoMarcacion();

        return result;
    }

}
