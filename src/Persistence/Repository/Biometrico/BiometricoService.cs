using EvaluacionCore.Application.Features.Biometrico.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Workflow.Persistence.Repository.Biometrico
{
    public class BiometricoService : IBiometrico
    {
        private readonly IConfiguration _config;
        private readonly ILogger<BiometricoService> _log;
        private string nombreEnpoint = string.Empty;
        private string uriEndPoint = string.Empty;

        public BiometricoService(IConfiguration config, ILogger<BiometricoService> log)
        {
            _log = log;
            _config = config;
        }

        public Task<int> AlertarNovedadBiometricoAsync(string parametro)
        {
            return Task.FromResult(1);
        }
    }
}
