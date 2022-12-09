using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class TurnoConfiguration : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.TurnoColaboradores)
              .WithOne(g => g.Turno)
              .HasForeignKey(g => g.IdTurno)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IdTurnoPadre)
               .IsRequired(false);

            builder.Property(x => x.MargenEntradaPrevio)
              .HasMaxLength(4)
              .IsRequired();


            //builder.Property(x => x.MargenEntradaPosterior)
            //  .HasMaxLength(4)
            //  .IsRequired();


            //builder.Property(x => x.MargenSalidaPrevio)
            //  .HasMaxLength(4)
            //  .IsRequired();


            builder.Property(x => x.MargenSalidaPosterior)
              .HasMaxLength(4)
              .IsRequired();


            builder.Property(x => x.CodigoTurno)
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
