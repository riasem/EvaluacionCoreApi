using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Permisos.Dto;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.EvalCore.Queries.GetComboPeriodoAsync;

public record GetComboPeriodoAsyncQuery(string CodUdn) : IRequest<ResponseType<List<ComboPeriodoType>>>;

public class GetComboNovedadesAsyncHandler : IRequestHandler<GetComboPeriodoAsyncQuery, ResponseType<List<ComboPeriodoType>>>
{
    private readonly IRepositoryGRiasemAsync<PeriodosLaborales> _repositoryPeriodoAsync;


    public GetComboNovedadesAsyncHandler(IRepositoryGRiasemAsync<PeriodosLaborales> apisPeriodoAsync)
    {
        _repositoryPeriodoAsync = apisPeriodoAsync;
    }
    async Task<ResponseType<List<ComboPeriodoType>>> IRequestHandler<GetComboPeriodoAsyncQuery, ResponseType<List<ComboPeriodoType>>>.Handle(GetComboPeriodoAsyncQuery request, CancellationToken cancellationToken)
    {
        var objPeriodo = await _repositoryPeriodoAsync.ListAsync(cancellationToken);
        
        List<ComboPeriodoType> objResult = new() { };

        objResult = objPeriodo.Where(e => e.Udn == request.CodUdn).Select(e => new ComboPeriodoType
        {
            Id = e.Id,
            DesPeriodo = e.DesPeriodo,
            Udn = e.Udn
        }).ToList();


        return new ResponseType<List<ComboPeriodoType>>() { Data = objResult, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        
    }

}