using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class SubclaseTurnoConfiguration : IEntityTypeConfiguration<SubclaseTurno>
    {
        public void Configure(EntityTypeBuilder<SubclaseTurno> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.Turnos)
              .WithOne(g => g.SubclaseTurno)
              .HasForeignKey(g => g.IdSubclaseTurno)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CodigoSubclaseTurno)
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
