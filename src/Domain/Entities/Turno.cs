using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_Turno", Schema = "dbo")]
public class Turno
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();


    [Column("idTipoSubturno", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdTipoSubturno { get; set; }
    public virtual TipoTurno TipoSubTurno { get; set; }


    [Column("codigoTurno", Order = 2, TypeName = "varchar")]
    public string CodigoTurno { get; set; } = string.Empty;


    [Column("descripcion", Order = 3, TypeName = "varchar")]
    public string Descripcion { get; set; } = string.Empty;


    [Column("entrada", Order = 4, TypeName = "datetime")]
    public DateTime Entrada { get; set; }


    [Column("salida", Order = 5, TypeName = "datetime")]
    public DateTime Salida { get; set; }


    [Column("margenEntrada", Order = 6, TypeName = "datetime")]
    public DateTime MargenEntrada { get; set; }


    [Column("margenSalida", Order = 7, TypeName = "varchar")]
    public DateTime MargenSalida { get; set; }


    [Column("totalHoras", Order = 8, TypeName = "varchar")]
    public string TotalHoras { get; set; } = string.Empty;


    [Column("estado", Order = 9, TypeName = "varchar")]
    public string Estado { get; set; } = string.Empty;

    //AUDITORIA
    [Column("usuarioCreacion", Order = 10, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 11, TypeName = "datetime")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 12, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 13, TypeName = "datetime")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

