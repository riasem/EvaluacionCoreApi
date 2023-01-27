using EvaluacionCore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Permisos;

[Table("AS_NovedadRecordatorioDet", Schema = "dbo")]
public class NovedadRecordatorioDet
{
    [Key]
    [Column("id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }
    
    [Column("idNovedadRecordatorioCab", TypeName = "uniqueidentifier")]
    public Guid IdNovedadRecordatorioCab { get; set; }
    //public virtual NovedadRecordatorioCab NovedadRecordatorioCab { get; set; }

    [Column("codBiometricoColaborador", TypeName = "varchar")]
    public string CodBiometricoColaborador { get; set; }

    [Column("identificacionColaborador", TypeName = "varchar")]
    public string IdentificacionColaborador { get; set; }

    [Column("nombreColaborador", TypeName = "varchar")]
    public string NombreColaborador { get; set; }

    [Column("fechaNoAsignada", TypeName = "datetime")]
    public DateTime FechaNoAsignada { get; set; }

    [Column("fechaEvaluacion", TypeName = "datetime")]
    public DateTime FechaEvaluacion { get; set; }

    [Column("udn", TypeName = "varchar")]
    public string Udn { get; set; } 

    [Column("area", TypeName = "varchar")]
    public string Area { get; set; } 

    [Column("subCentroCosto", TypeName = "varchar")]
    public string SubcentroCosto { get; set; } 
}
