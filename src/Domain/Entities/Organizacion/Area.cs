
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EvaluacionCore.Domain.Entities.Organizacion;

[Table("OG_Area", Schema = "dbo")]
public class Area
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("empresaId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid EmpresaId { get; set; }
    public virtual Empresa Empresa { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    [StringLength(15)]
    public string Codigo { get; set; }

    [Column("nombre", Order = 3, TypeName = "varchar")]
    [StringLength(50)]
    public string Nombre { get; set; }

    [Column("estado", Order = 4, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; set; }
}
