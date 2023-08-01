using EvaluacionCore.Domain.Entities.Seguridad;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Configurations.Seguridad;

public class CargoSGConfiguration : IEntityTypeConfiguration<CargoSG>
{
    public void Configure(EntityTypeBuilder<CargoSG> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.RolCargoSG)
            .WithOne(g => g.CargoSG)
            .HasForeignKey(g => g.CargoSGId)
            .IsRequired();


    }

}
