using EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;

namespace EvaluacionCore.Application.Features.EvalCore.Interfaces;

public interface IEvaluacion
{
    //InformacionGeneralEvaluacion GetInfoGeneralByIdentificacion(string Identificacion);

    Task<(string response, int success)> EvaluateAsistencias(string identificacion, DateTime? fechaDesde, DateTime? fechaHasta);
}
