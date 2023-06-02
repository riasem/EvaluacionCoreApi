using EvaluacionCore.Domain.Entities.Organizacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workflow.Persistence.Configurations.Organizacion;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(g => g.Localidades)
            .WithOne(g => g.Empresa)
            .HasForeignKey(g => g.IdEmpresa)
            .IsRequired();
    }
}