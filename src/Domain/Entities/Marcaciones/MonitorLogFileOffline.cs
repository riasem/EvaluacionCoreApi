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

    [Column("idMonito", TypeName = "int")]
    public int IdMonito { get; set; }

    [Column("filePerfil", TypeName = "image")]
    public byte[] FilePerfil { get; set; }
}
