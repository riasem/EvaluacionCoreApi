using EnrolApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Configurations.Organizacion;

public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
{
    public void Configure(EntityTypeBuilder<Cargo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.Clientes)
            .WithOne(g => g.Cargo)
            .HasForeignKey(g => g.CargoId)
            .IsRequired();
    }
}