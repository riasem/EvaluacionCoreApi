using EvaluacionCore.Domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Configurations.Calendario;

public class PaisConfiguration : IEntityTypeConfiguration<Pais>
{
    public void Configure(EntityTypeBuilder<Pais> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(g => g.Provincias)
            .WithOne(g => g.Pais)
            .HasForeignKey(g => g.IdPais)
            .IsRequired();

        builder.HasMany(g => g.CalendarioNacional)
            .WithOne(g => g.Pais)
            .HasForeignKey(g => g.IdPais)
            .IsRequired();


    }
}
