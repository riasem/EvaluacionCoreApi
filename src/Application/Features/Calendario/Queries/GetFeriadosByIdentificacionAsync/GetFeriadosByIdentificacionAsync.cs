using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Calendario.Dto;
using EvaluacionCore.Application.Features.Calendario.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Calendario.Queries.GetFeriadosByIdentificacionAsync;

public record GetFeriadosByIdentificacionAsync(string Identificacion, DateTime FechaFeriado) : IRequest<ResponseType<List<DiasFeriadoIdentificacionResponseType>>>;

public class GetFeriadosByIdentificacionAsyncHandler  : IRequestHandler<GetFeriadosByIdentificacionAsync, ResponseType<List<DiasFeriadoIdentificacionResponseType>>>
{
    private readonly ICalendario _repository;

    public GetFeriadosByIdentificacionAsyncHandler( ICalendario repository)
    {
        _repository = repository;
    }

    public async Task<ResponseType<List<DiasFeriadoIdentificacionResponseType>>> Handle(GetFeriadosByIdentificacionAsync request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.GetDiasFeriadosByIdentificacion(request.Identificacion,request.FechaFeriado);

        return objResult;
    }
}
