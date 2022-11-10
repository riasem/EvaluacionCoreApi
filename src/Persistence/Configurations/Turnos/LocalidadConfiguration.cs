using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class LocalidadConfiguration : IEntityTypeConfiguration<Localidad>
    {
        public void Configure(EntityTypeBuilder<Localidad> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.LocalidadColaboradores)
              .WithOne(g => g.Localidad)
              .HasForeignKey(g => g.IdLocaliad)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Latitud)
                .IsRequired();
            builder.Property(x => x.Longitud)
                .IsRequired();

            builder.Property(p => p.FechaCreacion)
              .HasColumnType("datetime2")
              .HasDefaultValueSql("getdate()")
              .ValueGeneratedOnAdd();

            builder.Property(p => p.Estado)
           .IsRequired();
        }
    }
}
