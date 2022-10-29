using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_SubTurnoCliente", Schema = "dbo")]
public class SubTurnoCliente
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idSubturno", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdSubturno { get; set; }
    //public virtual SubTurno SubTurno { get; set; }

    [Column("idCliente", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdCliente { get; set; }
    //public virtual Cliente Cliente { get; set; }

    [Column("estado", Order = 3, TypeName = "varchar")]
    public string Estado { get; set; } = "A";



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

