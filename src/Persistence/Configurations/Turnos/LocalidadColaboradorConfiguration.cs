using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class LocalidadColaboradorConfiguration : IEntityTypeConfiguration<LocalidadColaborador>
    {
        public void Configure(EntityTypeBuilder<LocalidadColaborador> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.MarcacionColaboradores)
              .WithOne(g => g.LocalidadColaborador)
              .HasForeignKey(g => g.IdLocalidadColaborador)
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
