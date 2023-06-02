using EvaluacionCore.Domain.Entities.Calendario;
using EvaluacionCore.Domain.Entities.Organizacion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Asistencia;

[Table("AS_Localidad", Schema = "dbo")]
public class Localidad
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idEmpresa", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdEmpresa { get; set; }
    [Column("idCanton", TypeName = "uniqueidentifier")]
    public virtual Empresa Empresa { get; set; }
    public Guid IdCanton { get; set; }
    public virtual Canton Canton { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    [StringLength(10)] public string Codigo { get; set; } = string.Empty;

    [Column("latitud", Order = 3, TypeName = "float")]
    [MaxLength(8)] public double Latitud { get; set; }

    [Column("longitud", Order = 4, TypeName = "float")]
    [MaxLength(8)] public double Longitud { get; set; }

    [Column("radio", Order = 5, TypeName = "float")]
    [MaxLength(8)] public double Radio { get; set; }

    [Column("descripcion", Order = 6, TypeName = "varchar")]
    public string Descripcion { get; set; }

    [Column("estado", Order = 7, TypeName = "varchar")]
    [StringLength(1)] public string Estado { get; set; } = "A";

    //AUDITORIA
    [Column("usuarioCreacion", Order = 8, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 9, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 10, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 11, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }


    public virtual ICollection<LocalidadColaborador> LocalidadColaboradores { get; set; }

}

