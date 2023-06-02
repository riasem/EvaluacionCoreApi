
using EvaluacionCore.Domain.Entities.Asistencia;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Organizacion;

[Table("OG_Empresa", Schema = "dbo")]
public class Empresa
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("grupoEmpresarialId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid GrupoEmpresarialId { get; set; }
    public virtual GrupoEmpresarial GrupoEmpresarial { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    [StringLength(15)]
    public string Codigo { get; set; }

    [Column("ruc", Order = 3, TypeName = "varchar")]
    [StringLength(13)]
    public string Ruc { get; set; }

    [Column("nombreComercial", Order = 4, TypeName = "varchar")]
    [StringLength(50)]
    public string NombreComercial { get; set; }

    [Column("razonSocial", Order = 5, TypeName = "varchar")]
    [StringLength(80)]
    public string RazonSocial { get; set; }


    //[Column("logo", TypeName = "varchar")]

    [Column("logo", Order = 6, TypeName = "varbinary")]
    public byte[] Logo { get; set; }

    [Column("estado", Order = 7, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    
    public virtual ICollection<Area> Areas { get; set; }

    public virtual ICollection<Localidad> Localidades { get; set; }
}
