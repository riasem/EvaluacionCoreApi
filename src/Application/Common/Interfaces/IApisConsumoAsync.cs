namespace EvaluacionCore.Application.Common.Interfaces
{
    public interface IApisConsumoAsync
    {

        Task<(bool Success, object Data)> GetEndPoint(object request, string uriEndPoint, string nombreEndPoint);

        Task<(bool Success, object Data)> PostEndPoint(object request, string uriEndPoint, string nombreEndPoint);

        Task<(bool Success, object Data)> PutEndPoint(object request, string uriEndPoint, string nombreEndPoint);

    }
}
