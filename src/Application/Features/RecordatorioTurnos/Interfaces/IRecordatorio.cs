namespace EvaluacionCore.Application.Features.EvalCore.Interfaces;

public interface IRecordatorio
{
    Task<(string response, int success)> ProcesarRecordatorios(CancellationToken cancellationToken);

}
