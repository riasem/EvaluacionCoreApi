﻿using EnrolApp.Domain.Entities.Horario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Contexts
{
    public class ApplicationDbGRiasemContext : DbContext
    {
        public ApplicationDbGRiasemContext(DbContextOptions<ApplicationDbGRiasemContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<CheckInOut> CheckInOut { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbGRiasemContext).Assembly);
    }
}
