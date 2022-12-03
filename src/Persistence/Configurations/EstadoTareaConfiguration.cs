using EvaluacionCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkFlow.Persistence.Configurations.Workflow;

public class EstadoTareaConfiguration : IEntityTypeConfiguration<EstadoTarea>
{
    public void Configure(EntityTypeBuilder<EstadoTarea> builder)
    {

        //builder.HasKey(x => x.IdEstado);
        //builder.HasMany(g => g.Instancias)
        //    .WithOne(g => g.EstadoTarea)
        //    .HasForeignKey(g => g.EstadoId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(g => g.SolicitudPermiso)
            .WithOne(g => g.EstadoTarea)
            .HasForeignKey(g => g.IdEstadoSolicitud)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(g => g.SolicitudJustificacion)
            .WithOne(g => g.EstadoTarea)
            .HasForeignKey(g => g.IdEstadoSolicitud)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(g => g.SolicitudVacaion)
            .WithOne(g => g.EstadoTarea)
            .HasForeignKey(g => g.IdEstadoSolicitud)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Codigo)
            .HasMaxLength(20).IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(100).IsRequired();
      
    }
}