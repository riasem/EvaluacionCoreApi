using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Calendario;

[Table("AS_Pais", Schema = "dbo")]
public class Pais
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("codigo", Order = 1, TypeName = "varchar")]
    [StringLength(10)]
    public string Codigo { get; set; }

    [Column("descripcion", Order = 2, TypeName = "varchar")]
    [StringLength(50)]
    public string Descripcion { get; set; }

    [Column("estado", Order = 3, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    [StringLength (50)]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 5, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    [StringLength(50)]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

    //public virtual ICollection<CalendarioLocal> Calendarios { get; set; }
    public virtual ICollection<Provincia> Provincias { get; set; }
    public virtual ICollection<CalendarioNacional> CalendarioNacional { get; set; }

}
