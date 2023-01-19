using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Calendario;

[Table("AS_CalendarioLocal", Schema = "dbo")]
public class CalendarioLocal
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("idCanton", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdCanton { get; set; }
    public virtual Canton Canton { get; set; }

    [Column("descripcion", Order = 2, TypeName = "varchar")]
    [StringLength(150)]
    public string Descripcion { get; set; }

    [Column("fechaFestiva", Order = 3, TypeName = "datetime2")]
    public DateTime FechaFestiva { get; set; }

    [Column("fechaConmemorativa", Order = 4, TypeName = "datetime2")]
    public DateTime FechaConmemorativa { get; set; }

    [Column("esRecuperable", Order = 5, TypeName = "bit")]
    public bool? EsRecuperable { get; set; }

    [Column("estado", Order = 6, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
    [StringLength(50)]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 8, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
    [StringLength(50)]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 10, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

}
