using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Calendario;

[Table("AS_CiudadPais", Schema = "dbo")]
public class CiudadPais
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [NotMapped]
    [Column("pais", Order = 1, TypeName = "varchar")]
    public string Pais { get; set; }

    [Column("ciudad", Order = 2, TypeName = "varchar")]
    public string Ciudad { get; set; }

    [Column("estado", Order = 3, TypeName = "varchar")]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order =5, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Calendario> Calendarios { get; set; }
}
