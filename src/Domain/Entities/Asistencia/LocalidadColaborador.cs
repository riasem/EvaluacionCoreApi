using EvaluacionCore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Asistencia;

[Table("AS_LocalidadColaborador", Schema = "dbo")]
public class LocalidadColaborador
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idLocalidad", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdLocalidad { get; set; }
    public virtual Localidad Localidad { get; set; }

    [Column("idColaborador", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdColaborador { get; set; }
    public virtual Cliente Colaborador { get; set; }

    [Column("estado", Order = 3, TypeName = "varchar")]
    public string Estado { get; set; } = "A";


    //AUDITORIA
    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 5, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<MarcacionColaborador> MarcacionColaboradores { get; set; }

}

