using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Justificacion;

[Table("WF_TipoJustificacion", Schema = "dbo")]
public class TipoJustificacion
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

    [Column("fechaCreacion")]
    public DateTime? FechaCreacion { get; set; }

    [Column("usuarioCreacion")]
    public string UsuarioCreacion { get; set; }

    [Column("fechaModificacion")]
    public DateTime? FechaModificacion { get; set; }

    [Column("usuarioModificacion")]
    public string UsuarioModificacion { get; set; }

    public virtual ICollection<SolicitudJustificacion> SolicitudJustificacion { get; set; }
}
