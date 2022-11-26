using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Permisos;

[Table("WF_TipoPermiso", Schema = "dbo")]
public class TipoPermiso
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("codigo")]
    [StringLength(5)]
    public string Codigo { get; set; }

    [Column("descripcion")]
    [StringLength(150)]
    public string Descripcion { get; set; }

    [Column("estado")]
    [StringLength(1)]
    public string Estado { get; set; }

    public virtual ICollection<SolicitudPermiso> SolicitudPermiso { get; set; }


}
