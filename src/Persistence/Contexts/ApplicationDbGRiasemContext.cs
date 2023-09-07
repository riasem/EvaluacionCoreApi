using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using EvaluacionCore.Domain.Entities.Marcaciones;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionCore.Persistence.Contexts
{
    public class ApplicationDbGRiasemContext : DbContext
    {
        public ApplicationDbGRiasemContext(DbContextOptions<ApplicationDbGRiasemContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<CheckInOut> CheckInOut { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<MonitorLogFileOffline> MonitorLogFileOffline { get; set; }

        public DbSet<AccMonitorLog> AccMonitorLog { get; set; }
        public DbSet<AccMonitoLogRiasem> AccMonitoLogRiasem { get; set; }
        public DbSet<ControlAsistenciaCab> ControlAsistenciaCab { get; set; }
        public DbSet<ControlAsistenciaDet> ControlAsistenciaDet { get; set; }
        public DbSet<ControlAsistenciaNovedad> ControlAsistenciaNovedad { get; set; }
        public DbSet<ControlAsistenciaSolicitudes> ControlAsistenciaSolicitudes { get; set; }
        public DbSet<PeriodosLaborales> PeriodosLaborales { get; set; }
        public DbSet<AlertasNovedadMarcacion> AlertasNovedadMarcacion { get; set; }
        public DbSet<ControlAsistenciaCab_V> ControlAsistenciaCabV { get; set; }
        public DbSet<ControlAsistenciaNovedad_V> ControlAsistenciaNovedadV { get; set; }
        public DbSet<ControlAsistenciaSolicitudes_V> ControlAsistenciaSolicitudesV { get; set; }
        public DbSet<Machines> Machines { get; set; }

        public DbSet<MarcacionOffline> MarcacionOffline { get; set; }

        public DbSet<AccLogMarcacionOffline> AccLogMarcacionOffline { get; set; }

        public DbSet<DispositivoMarcacion> DispositivoMarcacion { get; set; }

        public DbSet<ControlAsistencia_V> ControlAsistenciaV { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbGRiasemContext).Assembly);
    }
}
