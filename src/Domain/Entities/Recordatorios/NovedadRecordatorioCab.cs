using EvaluacionCore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Permisos;

[Table("AS_NovedadRecordatorioCab", Schema = "dbo")]
public class NovedadRecordatorioCab
{
    [Key]
    [Column("id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("idJefe", TypeName = "uniqueidentifier")]
    public Guid IdJefe { get; set; }
    //public virtual Cliente Colaborador { get; set; }

    [Column("fechaEvaluacion", TypeName = "datetime")]
    public DateTime FechaEvaluacion { get; set; }

    [Column("estado", TypeName = "varchar")]
    public string Estado { get; set; }  // PROCESADO (PR) NOTIFICADO (NO) RECHAZADO (RE)

    [Column("tipoRecordatorio", TypeName = "varchar")]
    public string TipoRecordatorio { get; set; }  
    //RECORDATORIO (RC) -- ENTRE INICIO DE RECORDATORIO Y ANTES DE FECHA LIMITE
    //DIA LIMITE (LM)
    //ALERTA (AL) -- PASADO EL DIA LIMITE

    [Column("diasRecordatorio", TypeName = "int")]
    public int DiasRecordatorio { get; set; }


    public virtual ICollection<NovedadRecordatorioDet> NovedadRecordatorioDets { get; set; }
}
