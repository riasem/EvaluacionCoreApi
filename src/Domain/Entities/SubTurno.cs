using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_Subturno", Schema = "dbo")]
public class SubTurno
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();


    [Column("idTipoSubturno", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdTipoSubturno { get; set; }
    public virtual TipoSubTurno TipoSubTurno { get; set; }


    [Column("idTurno", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdTurno { get; set; }
    public virtual Turno Turno { get; set; }


    [Column("codigoSubturno", Order = 3, TypeName = "varchar")]
    [StringLength(10)] public string CodigoSubturno { get; set; } = string.Empty;


    [Column("descripcion", Order = 4, TypeName = "varchar")]
    [StringLength(50)] public string Descripcion { get; set; } = string.Empty;


    [Column("entrada", Order = 5, TypeName = "datetime2")]
    public DateTime Entrada { get; set; }


    [Column("salida", Order = 6, TypeName = "datetime2")]
    public DateTime Salida { get; set; }


    [Column("margenEntrada", Order = 7, TypeName = "datetime2")]
    public DateTime MargenEntrada { get; set; }


    [Column("margenSalida", Order = 8, TypeName = "datetime2")]
    public DateTime MargenSalida { get; set; }


    [Column("totalHoras", Order = 9, TypeName = "varchar")]
    public string TotalHoras { get; set; } = string.Empty;


    [Column("estado", Order = 10, TypeName = "varchar")]
    public string Estado { get; set; } = "A";

    //AUDITORIA
    [Column("usuarioCreacion", Order = 11, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 12, TypeName = "datetime2")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 13, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 14, TypeName = "datetime2")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

