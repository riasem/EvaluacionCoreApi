using WebEvaluacionCoreApi.Middleware;

namespace WebEvaluacionCoreApi.Extensions;

public static class AppExtensions
{
    public static void UseErrorHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
