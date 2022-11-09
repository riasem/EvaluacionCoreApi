using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities.Asistencia;

[Table("AS_Turno", Schema = "dbo")]
public class Turno
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idTurnoPadre", Order = 1, TypeName = "uniqueidentifier")]
    public Guid? IdTurnoPadre { get; set; }

    [Column("idTipoTurno", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdTipoTurno { get; set; }
    public virtual TipoTurno TipoTurno { get; set; }

    [Column("idClaseTurno", Order = 3, TypeName = "uniqueidentifier")] //laboral - receso
    public Guid IdClaseTurno { get; set; }
    public virtual ClaseTurno ClaseTurno { get; set; }

    [Column("idSubclaseTurno", Order = 4, TypeName = "uniqueidentifier")]  // laboral - A - D - P
    public Guid IdSubclaseTurno { get; set; }
    public virtual SubclaseTurno SubclaseTurno { get; set; }

    [Column("idTipoJornada", Order = 5, TypeName = "uniqueidentifier")] // diurna - noctura
    public Guid IdTipoJornada { get; set; }

    [Column("idModalidadJornada", Order = 6, TypeName = "uniqueidentifier")] // completa - parcial
    public Guid IdMidalidadJornada { get; set; }

    [Column("codigoTurno", Order = 7, TypeName = "varchar")]
    [StringLength(10)] public string CodigoTurno { get; set; } = string.Empty;

    [Column("codigoIntegracion", Order = 8, TypeName = "varchar")]
    [StringLength(10)] public string CodigoIntegracion { get; set; } = string.Empty;

    [Column("descripcion", Order = 9, TypeName = "varchar")]
    [StringLength(50)] public string Descripcion { get; set; } = string.Empty;

    [Column("entrada", Order = 10, TypeName = "datetime")]
    public DateTime Entrada { get; set; }

    [Column("salida", Order = 11, TypeName = "datetime")]
    public DateTime Salida { get; set; }

    [Column("margenEntrada", Order = 12, TypeName = "datetime")]
    public DateTime MargenEntrada { get; set; }

    [Column("margenSalida", Order = 13, TypeName = "datetime")]
    public DateTime MargenSalida { get; set; }

    [Column("totalHoras", Order = 14, TypeName = "varchar")]
    [StringLength(2)] public string TotalHoras { get; set; } = string.Empty;

    [Column("estado", Order = 15, TypeName = "varchar")]
    public string Estado { get; set; } = "A";



    //AUDITORIA
    [Column("usuarioCreacion", Order = 16, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 17, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 18, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 19, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }


    public virtual ICollection<TurnoColaborador> TurnoColaboradores { get; set; }

}

