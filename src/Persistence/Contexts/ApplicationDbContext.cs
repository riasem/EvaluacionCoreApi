using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Calendario;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using EvaluacionCore.Domain.Entities.Justificacion;
using EvaluacionCore.Domain.Entities.Marcaciones;
using EvaluacionCore.Domain.Entities.Organizacion;
using EvaluacionCore.Domain.Entities.Permisos;
using EvaluacionCore.Domain.Entities.Seguridad;
using EvaluacionCore.Domain.Entities.Vacaciones;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionCore.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<SolicitudJustificacion> SolicitudJustificacions { get; set; }
    public DbSet<LocalidadColaborador> LocalidadClientes { get; set; }
    public DbSet<MarcacionColaborador> MarcacionClientes { get; set; }
    public DbSet<SolicitudPermiso> SolicitudPermisos { get; set; }
    public DbSet<SolicitudVacacion> SolicitudVacacions { get; set; }
    public DbSet<Localidad> Localidades { get; set; }
    public DbSet<TipoTurno> TipoTurnos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<CalendarioLocal> CalendarioLocal { get; set; }
    public DbSet<CalendarioNacional> CalendarioNacional { get; set; }
    public DbSet<Pais> Pais { get; set; }
    public DbSet<Provincia> Provincia { get; set; }
    public DbSet<Canton> Canton { get; set; }
    public DbSet<NovedadRecordatorioCab> NovedadRecordatorioCabs { get; set; }
    public DbSet<NovedadRecordatorioDet> NovedadRecordatorioDet { get; set; }
    public DbSet<Recordatorio> Recordatorios { get; set; }
    public DbSet<ColaboradorConvivencia> ColaboradorConvivencia { get; set; }

    

    public DbSet<CanalSG> CanalSG => Set<CanalSG>();
    public DbSet<FeatureSG> FeatureSG => Set<FeatureSG>();
    public DbSet<RolCargoSG> RolCargoSG { get; set; }
    public DbSet<AtributoRolSG> AtributoRolSG { get; set; }

    public DbSet<TurnoColaborador> TurnoColaborador { get; set; }
    public DbSet<CargoEje> CargoEje { get; set; }

    public DbSet<LicenciaTerceroSG> LicenciaTerceroSG { get; set; }
    public DbSet<LogLicenciaTerceroSG> LogLicenciaTerceroSG { get; set; }
    public DbSet<ServicioTerceroSG> ServicioTerceroSG { get; set; }
    public DbSet<LocalidadAdministrador> LocalidadAdministrador { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

 
}