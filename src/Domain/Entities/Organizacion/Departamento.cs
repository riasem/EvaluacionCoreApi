using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Organizacion;

[Table("OG_Departamento", Schema = "dbo")]
public class Departamento
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("areaId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid AreaId { get; set; }
    public virtual Area Area { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    [StringLength(15)]
    public string Codigo { get; set; }

    [Column("nombre", Order = 3, TypeName = "varchar")]
    [StringLength(50)]
    public string Nombre { get; set; }

    [Column("codigoHomologacion", Order = 4, TypeName = "varchar")]
    public string CodigoHomologacion { get; set; }

    [Column("nombreHomologacion", Order = 5, TypeName = "varchar")]
    public string NombreHomologacion { get; set; }

    [Column("estado", Order = 6, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }


    //public virtual ICollection<Prospecto> Prospectos { get; set; }
    //public virtual ICollection<Cargo> Cargos { get; set; }
    //public virtual ICollection<Cliente> Clientes { get; set; }
}
