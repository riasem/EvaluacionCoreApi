﻿using EnrolApp.Domain.Entities.Horario;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using EvaluacionCore.Domain.Entities.Marcaciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DbSet<AccMonitorLog> AccMonitorLog { get; set; }
        public DbSet<AccMonitoLogRiasem> AccMonitoLogRiasem { get; set; }
        public DbSet<ControlAsistenciaCab> ControlAsistenciaCab { get; set; }
        public DbSet<ControlAsistenciaDet> ControlAsistenciaDet { get; set; }
        public DbSet<ControlAsistenciaNovedad> ControlAsistenciaNovedad { get; set; }
        public DbSet<PeriodosLaborales> PeriodosLaborales { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbGRiasemContext).Assembly);
    }
}
