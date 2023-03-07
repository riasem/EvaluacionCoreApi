namespace EvaluacionCore.Application.Features.Biometrico.Interfaces
{
    public interface IBiometrico
    {
        Task<int> AlertarNovedadBiometricoAsync(string parametro);
    }
}