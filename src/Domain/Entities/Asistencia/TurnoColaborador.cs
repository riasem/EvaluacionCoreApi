using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EvaluacionCore.Domain.Entities.Common;

namespace EvaluacionCore.Domain.Entities.Asistencia;

[Table("AS_TurnoColaborador", Schema = "dbo")]
public partial class TurnoColaborador
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idTurno", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdTurno { get; set; }
    public virtual Turno Turno { get; set; }

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

