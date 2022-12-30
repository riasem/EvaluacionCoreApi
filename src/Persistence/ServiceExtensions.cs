﻿using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using EvaluacionCore.Application.Features.Calendario.Interfaces;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Persistence.Contexts;
using EvaluacionCore.Persistence.Repository;
using EvaluacionCore.Persistence.Repository.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Persistence.Contexts;
using Workflow.Persistence.Repository;
using Workflow.Persistence.Repository.BitacoraMarcacion;
using Workflow.Persistence.Repository.Calendario;

namespace EvaluacionCore.Persistence;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddDbContext<ApplicationDbGRiasemContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Bd_Marcaciones_GRIAMSE")));


        #region Repositories

        services.AddTransient(typeof(IRepositoryAsync<>),typeof(CustomRepositoryAsync<>));
        services.AddTransient(typeof(IRepositoryGRiasemAsync<>), typeof(CustomRepositoryGRiasemAsync<>));
        //services.AddTransient<IAdjuntoService, AdjuntoService>();
        services.AddTransient<IBitacoraMarcacion, BitacoraMarcacionService>();
        services.AddTransient<IMarcacion, MarcacionService>();
        services.AddTransient<IEvaluacion, EvaluacionService>();
        services.AddTransient<ICalendario, CalendarioServices>();


        #endregion
        return services;
    }
}