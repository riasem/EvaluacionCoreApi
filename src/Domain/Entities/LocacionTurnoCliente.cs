using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_LocacionTurnoCliente", Schema = "dbo")]
public class LocacionTurnoCliente
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();


    [Column("idLocacion", Order = 1, TypeName = "uniqueidentifier")]
    public virtual Locacion Locacion { get; set; }


    [Column("idSubturnoCliente", Order = 2, TypeName = "uniqueidentifier")]
    public virtual SubTurnoCliente SubTurnoCliente { get; set; }


    [Column("fechaAsignacion", Order = 3, TypeName = "datetime2")]
    public System.DateTime FechaEvaluacion { get; set; } = System.DateTime.Now;



    //AUDITORIA
    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 5, TypeName = "datetime2")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

    public virtual ICollection<SubTurno> SubTurnos { get; set; }
    public virtual ICollection<SubTurnoCliente> SubTurnoClientes { get; set; }

}

