using EvaluacionCore.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.TurnoColaboradores)
              .WithOne(g => g.Colaborador)
              .HasForeignKey(g => g.IdColaborador)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.LocalidadColaboradores)
              .WithOne(g => g.Colaborador)
              .HasForeignKey(g => g.IdColaborador)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.TipoIdentificacion)
                .HasMaxLength(1)
               .IsRequired();
            builder.Property(x => x.Identificacion)
              .HasMaxLength(20)
              .IsRequired();
            builder.Property(p => p.Nombres)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.Apellidos)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(x => x.Alias)
                .HasMaxLength(50);
            builder.Property(x => x.Latitud)
                .IsRequired();
            builder.Property(x => x.Longitud)
                .IsRequired();
            builder.Property(p => p.Direccion)
               .HasMaxLength(200)
               .IsRequired();
           
            builder.Property(p => p.Celular)
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(p => p.TipoIdentificacionFamiliar)
               .HasMaxLength(10);

            builder.Property(p => p.IndentificacionFamiliar)
               .HasMaxLength(10);

            builder.Property(p => p.Correo)
              .HasMaxLength(40)
              .IsRequired();

            builder.Property(p => p.FechaRegistro)
              .HasColumnType("datetime2")
              .HasDefaultValueSql("getdate()")
              .ValueGeneratedOnAdd();


            builder.Property(p => p.ServicioActivo)
             .IsRequired();

            builder.Property(p => p.Estado)
           .IsRequired();
        }
    }
}
