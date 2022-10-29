using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_Canal", Schema = "dbo")]
public class Canal
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("codigoCanal", Order = 2, TypeName = "varchar")]
    [StringLength(20)]
    public string CodigoCanal { get; set; } = string.Empty;

    [Column("descripcion", Order = 3, TypeName = "varchar")]
    [StringLength(50)]
    public string Descripcion { get; set; } = string.Empty;


    //AUDITORIA
    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 5, TypeName = "datetime2")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

