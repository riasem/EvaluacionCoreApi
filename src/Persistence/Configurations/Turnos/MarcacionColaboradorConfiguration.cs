using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class MarcacionColaboradorConfiguration : IEntityTypeConfiguration<MarcacionColaborador>
    {
        public void Configure(EntityTypeBuilder<MarcacionColaborador> builder)
        {

            builder.HasKey(x => x.Id);
                       

            builder.Property(x => x.MargenEntradaPrevio)
              .HasMaxLength(4)
              .IsRequired();
                       

            builder.Property(x => x.MargenEntradaPosterior)
              .HasMaxLength(4)
              .IsRequired();
                       

            builder.Property(x => x.MargenSalidaPrevio)
              .HasMaxLength(4)
              .IsRequired();
                       

            builder.Property(x => x.MargenSalidaPosterior)
              .HasMaxLength(4)
              .IsRequired();
                       

            builder.Property(p => p.FechaCreacion)
              .HasColumnType("datetime2")
              .HasDefaultValueSql("getdate()")
              .ValueGeneratedOnAdd();

        }
    }
}
