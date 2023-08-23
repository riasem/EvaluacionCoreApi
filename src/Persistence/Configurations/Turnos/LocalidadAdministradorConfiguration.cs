using EvaluacionCore.Domain.Entities.Asistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class LocalidadAdministradorConfiguration : IEntityTypeConfiguration<LocalidadAdministrador>
    {
        public void Configure(EntityTypeBuilder<LocalidadAdministrador> builder)
        {

            builder.HasKey(x => x.IdLocalidadAdministrador);

            builder.Property(p => p.FechaCreacion)
              .HasColumnType("datetime2")
              .HasDefaultValueSql("getdate()")
              .ValueGeneratedOnAdd();

            builder.Property(p => p.Estado)
           .IsRequired();
        }
    }
}
