using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.ControlAsistencia;

[Table("ControlAsistenciaCab", Schema = "dbo")]
public class ControlAsistenciaCab
{
    [Key]
    [Column("id")]
    public int id { get; set; }
    
    [Column("periodo")]
    public string Periodo { get; set; }

    [Column("fechaDesde")]
    public DateTime FechaDesde { get; set; }

    [Column("fechaHasta")]
    public DateTime FechaHasta { get; set; }

    [Column("identificacion")]
    public string Identificacion { get; set; }

    [Column("idColaborador")]
    public Guid IdColaborador { get; set; }

    [Column("udn")]
    public string Udn { get; set; }

    [Column("area")]
    public string Area { get; set; }

    [Column("subcentroCosto")]
    public string SubcentroCosto { get; set; }

    [Column("fechaRegistro")]
    public DateTime FechaRegistro { get; set; }

    [Column("usuarioRegistro")]
    public string UsuarioRegistro { get; set; }

}
