using EvaluacionCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionCore.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Canal> Canales { get; set; }
    public DbSet<Locacion> Locaciones { get; set; }
    public DbSet<LocacionColaborador> LocacionClientes { get; set; }
    public DbSet<MarcacionColaborador> MarcacionColaboradores { get; set; }
    public DbSet<SubTurno> SubTurnos { get; set; }
    public DbSet<SubTurnoColaborador> SubTurnoColaborador { get; set; }
    public DbSet<TipoSubTurno> TipoSubTurnos { get; set; }
    public DbSet<TipoTurno> TipoTurnos { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<TurnoColaborador> TurnoColaboradores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

 
}