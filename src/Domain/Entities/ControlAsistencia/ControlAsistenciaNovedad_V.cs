using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.ControlAsistencia;

[Table("V_CASISTENCIA_NOV", Schema = "dbo")]
public class ControlAsistenciaNovedad_V
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idControlAsistenciaDet")] 
    public int IdControlAsistenciaDet { get; set; }

    [Column("fecha")]
    public DateTime Fecha { get; set; }

    [Column("colaborador")]
    public string Colaborador { get; set; }

    [Column("descripcion")]
    public string Descripcion { get; set; }

    [Column("minutosNovedad")]
    public string MinutosNovedad { get; set; }

    [Column("estadoMarcacion")]
    public string EstadoMarcacion { get; set; }

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
