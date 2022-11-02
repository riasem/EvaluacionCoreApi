using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EvaluacionCore.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var objRequest = JsonConvert.SerializeObject(request);
        //_logger.LogInformation($"Handling {typeof(TRequest).Name}");
        _logger.LogInformation("Request:: Metodo [{TRequest}] - Param [{objRequest}] ", typeof(TRequest).Name, objRequest);

        var response = await next();

        var objResponse = JsonConvert.SerializeObject(response);
        _logger.LogInformation("Response: {objResponse} ", objResponse);
        return response;
    }
}
