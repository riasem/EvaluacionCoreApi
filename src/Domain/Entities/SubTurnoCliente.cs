using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_SubTurnoCliente", Schema = "dbo")]
public class SubTurnoCliente
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();


    [Column("idTipoSubturno", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdTipoSubturno { get; set; }
    public virtual TipoSubTurno TipoSubTurno { get; set; }


    [Column("idCliente", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdCliente { get; set; }
    /**CLIENTE */ public virtual TipoSubTurno Cliente { get; set; }  /** CLIENTE */


    [Column("estado", Order = 3, TypeName = "varchar")]
    public string Estado { get; set; } = string.Empty;

    //AUDITORIA
    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 5, TypeName = "datetime")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 7, TypeName = "datetime")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

