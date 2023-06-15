using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Marcaciones;

[Table("monitor_log_file_offline", Schema = "dbo")]
public class MonitorLogFileOffline
{
    [Key]
    [Column("id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("monitorId", TypeName = "varchar")]
    public string MonitorId { get; set; }

    [Column("rutaImagen", TypeName = "varchar")]
    public string RutaImagen { get; set; }

    [Column("estadoValidacion", TypeName ="bit")]
    public bool EstadoValidacion { get; set; }

    [Column("estadoReconocimiento", TypeName = "varchar")]
    public string EstadoReconocimiento { get; set; }

    [Column("fechaRegistro",TypeName = "datetime2")]
    public DateTime FechaRegistro { get; set; }

    [Column("identificacion",TypeName = "varchar")]
    public string Identificacion { get; set; }

    [Column("time",TypeName = "datetime2")]
    public DateTime Time { get; set; } 


}
