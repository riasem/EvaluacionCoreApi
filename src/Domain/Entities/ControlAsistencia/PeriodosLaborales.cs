using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.ControlAsistencia;

[Table("periodosLaborales", Schema = "dbo")]
public class PeriodosLaborales
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("udn")] 
    public string Udn { get; set; }

    [Column("anioPeriodo")]
    public string AnioPeriodo { get; set; }

    [Column("mesPeriodo")]
    public string MesPeriodo { get; set; }

    [Column("desPeriodo")]
    public string DesPeriodo { get; set; }

    [Column("fechaDesdePeriodo")]
    public DateTime FechaDesdePeriodo { get; set; }

    [Column("fechaHastaPeriodo")]
    public DateTime FechaHastaPeriodo { get; set; }

    [Column("fechaDesdeCorte")]
    public DateTime FechaDesdeCorte { get; set; }

    [Column("fechaHastaCorte")]
    public DateTime FechaHastaCorte { get; set; }

    [Column("idPeriodoPadre")]
    public int IdPeriodoPadre { get; set; }

    [Column("estado")]
    public string Estado { get; set; }

    [Column("fechaCierreControlAsistencia")]
    public DateTime FechaCierreControlAsistencia { get; set; }

    [Column("horaCierreControlAsistencia")]
    public string HoraCierreControlAsistencia { get; set; }
    
}
