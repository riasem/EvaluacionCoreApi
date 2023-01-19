using EvaluacionCore.Domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Configurations.Calendario;

public class CantonConfiguration : IEntityTypeConfiguration<Canton>
{
    public void Configure(EntityTypeBuilder<Canton> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(g => g.CalendariosLocal)
            .WithOne(g => g.Canton)
            .HasForeignKey(g => g.IdCanton)
            .IsRequired();

        builder.HasMany(g => g.Localidades)
            .WithOne(g => g.Canton)
            .HasForeignKey(g => g.IdCanton)
            .IsRequired();


    }
}
