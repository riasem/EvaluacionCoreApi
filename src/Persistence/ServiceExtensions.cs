using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using EvaluacionCore.Persistence.Contexts;
using EvaluacionCore.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Persistence.Repository.BitacoraMarcacion;

namespace EvaluacionCore.Persistence;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


        #region Repositories

        services.AddTransient(typeof(IRepositoryAsync<>),typeof(CustomRepositoryAsync<>));
        //services.AddTransient<IAdjuntoService, AdjuntoService>();
        services.AddTransient<IBitacoraMarcacion, BitacoraMarcacionService>();

        #endregion
        return services;
    }
}