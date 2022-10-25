using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_Locacion", Schema = "dbo")]
public class Locacion
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("codigo", Order = 1, TypeName = "varchar")]
    [StringLength(5)] public string Codigo { get; set; } = string.Empty;


    [Column("idEmpresa", Order = 2, TypeName = "varchar")]
    [StringLength(20)] public string IdEmpresa { get; set; } = string.Empty;


    [Column("latitud", Order = 3, TypeName = "float")]
    [MaxLength(8)] public double Latitud { get; set; } 

    [Column("longitud", Order = 4, TypeName = "float")]
    [MaxLength(8)] public double Logintud { get; set; }

    [Column("descripcion", Order = 5, TypeName = "varchar")]
    public Guid Descripcion { get; set; }

    [Column("estado", Order = 6, TypeName = "varchar")]
    [StringLength(1)] public string Estado { get; set; } = "A";

    //AUDITORIA
    [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 8, TypeName = "datetime2")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 10, TypeName = "datetime2")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

