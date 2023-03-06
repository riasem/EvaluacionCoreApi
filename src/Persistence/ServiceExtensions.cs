using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using EvaluacionCore.Application.Features.Calendario.Interfaces;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Application.Features.Turnos.Interfaces;
using EvaluacionCore.Persistence.Contexts;
using EvaluacionCore.Persistence.Repository;
using EvaluacionCore.Persistence.Repository.BitacoraMarcacion;
using EvaluacionCore.Persistence.Repository.Employees;
using EvaluacionCore.Persistence.Repository.General;
using EvaluacionCore.Persistence.Repository.RecordatorioTurnos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Persistence.Repository.Biometria;
using Workflow.Persistence.Repository.Calendario;
using Workflow.Persistence.Repository.TurnosAsignadosExcel;

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
        services.AddTransient<IApisConsumoAsync, ApisConsumoAsync>();
        services.AddTransient<IBitacoraMarcacion, BitacoraMarcacionService>();
        services.AddTransient<ITurnosAsignadosExcel, TurnosAsignadosExcelService>();
        services.AddTransient<IMarcacion, MarcacionService>();
        services.AddTransient<IEvaluacion, EvaluacionService>();
        services.AddTransient<ICalendario, CalendarioServices>();
        services.AddTransient<IRecordatorio, RecordatorioService>();
        services.AddTransient<IBiometria, BiometriaService>();

        #endregion
        return services;
    }
}