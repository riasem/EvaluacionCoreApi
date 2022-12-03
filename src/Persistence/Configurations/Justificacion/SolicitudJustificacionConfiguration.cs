using EvaluacionCore.Domain.Entities.Justificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkFlow.Persistence.Configurations.Justificacion;

public class SolicitudJustificacionConfiguration : IEntityTypeConfiguration<SolicitudJustificacion>
{
    public void Configure(EntityTypeBuilder<SolicitudJustificacion> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IdTipoJustificacion)
                .IsRequired();

        builder.Property(x => x.IdEstadoSolicitud)
        .IsRequired();

        builder.Property(x => x.Comentarios)
        .HasMaxLength(255);

        builder.Property(x => x.FechaModificacion)
        .HasDefaultValue(null);


    }
}
