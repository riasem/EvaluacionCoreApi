using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionCore.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Localidad> Localidades { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<LocalidadColaborador> LocalidadClientes { get; set; }
    public DbSet<MarcacionColaborador> MarcacionClientes { get; set; }
    //public DbSet<SubTurno> SubTurnos { get; set; }
    //public DbSet<TipoSubTurno> TipoSubTurnos { get; set; }
    public DbSet<TipoTurno> TipoTurnos { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<Cargo> Cargos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

 
}