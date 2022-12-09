using EvaluacionCore.Domain.Entities.Justificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkFlow.Persistence.Configurations.Justificacion;

public class TipoJustificacionConfiguration : IEntityTypeConfiguration<TipoJustificacion>
{
    public void Configure(EntityTypeBuilder<TipoJustificacion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.SolicitudJustificacion)
            .WithOne(g => g.TipoJustificacion)
            .HasForeignKey(g => g.IdTipoJustificacion)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Estado)
            .HasDefaultValue("A")
            .ValueGeneratedOnAdd()
            .IsRequired();

    }

}
