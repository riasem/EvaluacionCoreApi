using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_Locacion", Schema = "dbo")]
public class Locacion
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("codigo", Order = 1, TypeName = "varchar")]
    public string Codigo { get; set; } = string.Empty;

    [Column("idEmpresa", Order = 2, TypeName = "varchar")]
    public string IdEmpresa { get; set; } = string.Empty;

    [Column("latitud", Order = 3, TypeName = "varchar")]
    public string Latitud { get; set; } = string.Empty;

    [Column("longitud", Order = 4, TypeName = "varchar")]
    public string Logintud { get; set; } = string.Empty;

    [Column("descripcion", Order = 5, TypeName = "varchar")]
    public Guid Descripcion { get; set; }

    [Column("estado", Order = 6, TypeName = "varchar")]
    public string Estado { get; set; } = string.Empty;

    //AUDITORIA
    [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 8, TypeName = "datetime")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 10, TypeName = "datetime")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

