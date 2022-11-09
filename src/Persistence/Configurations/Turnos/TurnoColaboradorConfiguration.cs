using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class TurnoColaboradorConfiguration : IEntityTypeConfiguration<TurnoColaborador>
    {
        public void Configure(EntityTypeBuilder<TurnoColaborador> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.MarcacionColaboradores)
              .WithOne(g => g.TurnoColaborador)
              .HasForeignKey(g => g.IdTurnoColaborador)
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
