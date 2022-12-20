using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Calendario;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Justificacion;
using EvaluacionCore.Domain.Entities.Organizacion;
using EvaluacionCore.Domain.Entities.Permisos;
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
    public DbSet<Calendario> Calendarios { get; set; }
    public DbSet<CiudadPais> CiudadPais { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

 
}