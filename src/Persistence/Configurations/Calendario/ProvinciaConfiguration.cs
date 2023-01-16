using EvaluacionCore.Domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Configurations.Calendario;

public class ProvinciaConfiguration : IEntityTypeConfiguration<Provincia>
{
    public void Configure(EntityTypeBuilder<Provincia> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(g => g.Cantones)
            .WithOne(g => g.Provincia)
            .HasForeignKey(g => g.IdProvincia)
            .IsRequired();

    }
}
