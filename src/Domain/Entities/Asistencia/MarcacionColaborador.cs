using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities.Asistencia;

[Table("AS_MarcacionColaborador", Schema = "dbo")]
public class MarcacionColaborador
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idTurnoCliente", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdTurnoColaborador { get; set; }
    public virtual TurnoColaborador TurnoColaborador { get; set; }

    [Column("idLocalidadColaborador", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdLocalidadColaborador { get; set; }
    public virtual LocalidadColaborador LocalidadColaborador { get; set; }

    [Column("marcacionEntrada", Order = 3, TypeName = "datetime2")]
    public DateTime? MarcacionEntrada { get; set; }

    [Column("marcacionSalida", Order = 4, TypeName = "datetime2")]
    public DateTime? MarcacionSalida { get; set; }
    
    [Column("estadoMarcacionEntrada", Order = 5, TypeName = "varchar")] // AI=ATRASO INJUSTIFICADO   C= CORRECTO     AJ= ATRASO JUSTIFICADO      FJ = FALTA JUSTIFICADO      FI = FALTA INJUSTIFICADA    SJ = SALIDA JUSTIFICADA     SI = SALIDA INJUSTIFICADA
    public string EstadoMarcacionEntrada { get; set; }

    [Column("estadoMarcacionSalida", Order = 6, TypeName = "varchar")] // AI=ATRASO INJUSTIFICADO   C= CORRECTO     AJ= ATRASO JUSTIFICADO      FJ = FALTA JUSTIFICADO      FI = FALTA INJUSTIFICADA    SJ = SALIDA JUSTIFICADA     SI = SALIDA INJUSTIFICADA
    public string SalidaEntrada { get; set; }

    [Column("totalAtraso", Order = 7, TypeName = "datetime2")]
    public DateTime? TotalAtraso { get; set; }

    [Column("estadoProcesado", Order = 8, TypeName = "bit")]
    public bool EstadoProcesado { get; set; }


    //AUDITORIA
    [Column("usuarioCreacion", Order = 9, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 10, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 11, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 12, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

}

