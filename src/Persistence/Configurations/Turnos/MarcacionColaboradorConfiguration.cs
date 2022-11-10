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


            builder.Property(p => p.FechaCreacion)
              .HasColumnType("datetime2")
              .HasDefaultValueSql("getdate()")
              .ValueGeneratedOnAdd();

        }
    }
}
