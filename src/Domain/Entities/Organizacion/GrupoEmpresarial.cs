
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EvaluacionCore.Domain.Entities.Organizacion;

[Table("OG_GrupoEmpresarial", Schema = "dbo")]
public class GrupoEmpresarial
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("codigo", Order = 1, TypeName = "varchar")]
    [StringLength(15)]
    public string Codigo { get; set; }

    [Column("nombre", Order = 2, TypeName = "varchar")]
    [StringLength(50)]
    public string Nombre { get; set; }

    [Column("logo", Order = 3, TypeName = "varbinary")]
    public byte[] Logo { get; set; }

    [Column("estado", Order = 4, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    public virtual ICollection<Empresa> Empresas { get; set; }
}
