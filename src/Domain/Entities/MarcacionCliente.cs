using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_MarcacionCliente", Schema = "dbo")]
public class MarcacionCliente
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idLocacion", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdLocacion { get; set; }
    public virtual Locacion Locacion { get; set; }

    [Column("idSubTurno", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdSubTurno { get; set; }
    public virtual SubTurno SubTurno { get; set; }

    [Column("marcacionEntrada", Order = 3, TypeName = "datetime2")]
    public Nullable<System.DateTime> MarcacionEntrada { get; set; }

    [Column("marcacionSalida", Order = 4, TypeName = "datetime2")]
    public Nullable<System.DateTime> MarcacionSalida { get; set; }

    [Column("estadoMarcacionEntrada", Order = 5, TypeName = "varchar")] // J = JUSTIFICADA    I= INJUSTIFICADA   A=ATRASO   C= CORRECTO
    public string EstadoMarcacionEntrada { get; set; }

    [Column("estadoMarcacionSalida", Order = 6, TypeName = "varchar")] // J = JUSTIFICADA    I= INJUSTIFICADA   A=ATRASO   C= CORRECTO
    public string SalidaEntrada { get; set; }

    [Column("totalAtraso", Order = 7, TypeName = "datetime2")]
    public Nullable<System.DateTime> TotalAtraso { get; set; }

    [Column("estadoProcesado", Order = 8, TypeName = "bit")]
    public bool EstadoProcesado { get; set; }


    //AUDITORIA
    [Column("usuarioCreacion", Order = 9, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 10, TypeName = "datetime2")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 11, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 12, TypeName = "datetime2")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

