using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Asistencia;

[Table("SG_LocalidadAdministrador", Schema = "dbo")]
public class LocalidadAdministrador
{
    [Key]
    [Required]
    [Column("idLocalidadAdministrador", Order = 0, TypeName = "uniqueidentifier")]
    public Guid IdLocalidadAdministrador { get; set; } = Guid.NewGuid();

    [Column("idLocalidad", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdLocalidad { get; set; }
    public virtual Localidad Localidad { get; set; }

    [Column("identificacion", Order = 2, TypeName = "varchar")]
    public string Identificacion { get; set; }

    [Column("claveOffLine", Order = 3, TypeName = "varchar")]
    public string ClaveOffLine { get; set; }

    [Column("estado", Order = 4, TypeName = "varchar")]
    public string Estado { get; set; } = "A";


    //AUDITORIA
    [Column("usuarioCreacion", Order = 5, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 6, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 7, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 8, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }
}