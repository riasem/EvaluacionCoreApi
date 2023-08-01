using EvaluacionCore.Domain.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Configurations.Seguridad;

public class ServicioTerceroSGConfiguration : IEntityTypeConfiguration<ServicioTerceroSG>
{
    public void Configure(EntityTypeBuilder<ServicioTerceroSG> builder)
    {
        builder.HasKey(x => x.idServicioTercero);
        builder.HasMany(g => g.LicenciaTerceroSG)
                .WithOne(g => g.ServicioTercero)
                .HasForeignKey(g => g.IdServicioTercero)
                .IsRequired();

    }
}
