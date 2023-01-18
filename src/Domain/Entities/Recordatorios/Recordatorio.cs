using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Permisos;

[Table("AS_Recordatorio", Schema = "dbo")]
public class Recordatorio
{
    [Key]
    [Column("id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("inicioRecordatorio", TypeName = "datetime")]
    public DateTime InicioRecordatorio { get; set; }

    [Column("fechaLimite", TypeName = "datetime")]
    public DateTime FechaLimite { get; set; }

    [Column("finRecordatorio", TypeName = "datetime")]
    public DateTime FinRecordatorio { get; set; }

    [Column("periodoRecordatorio", TypeName = "varchar")]
    public string PeriodoRecordatorio { get; set; }
}
