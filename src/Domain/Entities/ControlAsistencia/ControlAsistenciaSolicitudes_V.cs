using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.ControlAsistencia;

[Table("V_CASISTENCIA_SOL", Schema = "dbo")]
public class ControlAsistenciaSolicitudes_V
{
    [Key]
    [Column("id")]
    public int Id { get; set; }    

    [Column("idControlAsistenciaDet")]
    public int IdControlAsistenciaDet { get; set; }

    [Column("idFeature")]
    public Guid IdFeature { get; set; }

    [Column("idSolicitud")]
    public Guid IdSolicitud { get; set; }

    [Column("colaborador")]
    public string Colaborador { get; set; }

    [Column("comentarios")]
    public string Comentarios { get; set; }

    #region FILTROS CABECERA

    [Column("periodo")]
    public string Periodo { get; set; }

    [Column("identificacion")]
    public string Identificacion { get; set; }

    [Column("udn")]
    public string Udn { get; set; }

    [Column("area")]
    public string Area { get; set; }

    [Column("subcentroCosto")]
    public string SubcentroCosto { get; set; }

    #endregion

}
