using EvaluacionCore.Domain.Entities.Permisos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkFlow.Persistence.Configurations.Permisos;

public class SolicitudPermisoConfiguration : IEntityTypeConfiguration<SolicitudPermiso>
{
    public void Configure(EntityTypeBuilder<SolicitudPermiso> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.HoraInicio)
                .HasMaxLength(10);

        builder.Property(x => x.HoraFin)
                .HasMaxLength(10);

        builder.Property(x => x.Observacion)
                .HasMaxLength(255);
    }
}
