using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class ClaseTurnoConfiguration : IEntityTypeConfiguration<ClaseTurno>
    {
        public void Configure(EntityTypeBuilder<ClaseTurno> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.Turnos)
              .WithOne(g => g.ClaseTurno)
              .HasForeignKey(g => g.IdClaseTurno)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CodigoClaseturno)
              .HasMaxLength(10)
              .IsRequired();

            builder.Property(x => x.Descripcion)
              .HasMaxLength(50)
              .IsRequired();
            
            builder.Property(p => p.Estado)
                .HasDefaultValueSql("A")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(p => p.FechaCreacion)
              .HasColumnType("datetime2")
              .HasDefaultValueSql("getdate()")
              .ValueGeneratedOnAdd();

        }
    }
}
