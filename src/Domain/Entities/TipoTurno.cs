using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_TipoTurno", Schema = "dbo")]
public class TipoTurno
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();


    [Column("codigoTurno", Order = 1, TypeName = "varchar")]
    public string CodigoTurno { get; set; } = string.Empty;


    [Column("descripcion", Order = 2, TypeName = "varchar")]
    public string Descripcion { get; set; } = string.Empty;


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

