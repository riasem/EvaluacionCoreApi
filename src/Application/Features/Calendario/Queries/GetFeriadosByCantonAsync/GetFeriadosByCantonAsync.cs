using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Calendario.Dto;
using EvaluacionCore.Application.Features.Calendario.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Calendario.Queries.GetFeriadosByCantonAsync;

public record GetFeriadosByCantonAsync(Guid IdCanton, DateTime FechaFeriado) : IRequest<ResponseType<List<DiasFeriadosResponseType>>>;
public class GetFeriadosByCantonAsyncHandler : IRequestHandler<GetFeriadosByCantonAsync, ResponseType<List<DiasFeriadosResponseType>>>
{
    private readonly ICalendario _repository;

    public GetFeriadosByCantonAsyncHandler(ICalendario repository)
    {
        _repository = repository;
    }

    public async Task<ResponseType<List<DiasFeriadosResponseType>>> Handle(GetFeriadosByCantonAsync request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.GetDiasFeriadosByCanton(request.IdCanton, request.FechaFeriado);

        return objResult;
    }
}
