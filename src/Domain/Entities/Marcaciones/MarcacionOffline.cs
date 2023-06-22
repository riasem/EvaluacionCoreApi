using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Marcaciones;

[Table("V_MARCACIONES_OFFLINE", Schema = "dbo")]
public class MarcacionOffline
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id",TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("codUdn", TypeName = "char")]
    public string CodUdn { get; set; }

    [Column("desUdn", TypeName = "varchar")]
    public string DesUdn { get; set; }

    [Column("deviceId",TypeName = "int")]

    public int? DeviceId { get; set; }

    [Column("dispositivo", TypeName = "varchar")]
    public string Dispositivo { get; set; }

 
    [Column("identificacion", TypeName = "varchar")]
    public string Identificacion { get; set; }


    [Column("empleado", TypeName = "varchar")]
    public string Empleado { get; set; }

    [Column("time", TypeName = "datetime2")]
    public DateTime? Time { get; set; }

    [Column("imagenColaborador", TypeName = "varchar")]
    public string ImagenColaborador { get; set; }

    [Column("imagenMarcacion", TypeName = "varchar")]
    public string ImagenMarcacion { get; set; }

    [Column("estadoValidacion", TypeName = "bit")]
    public bool? EstadoValidacion { get; set; }

    [Column("estadoReconocimiento", TypeName = "varchar")]
    public string EstadoReconocimiento { get; set; }




}
