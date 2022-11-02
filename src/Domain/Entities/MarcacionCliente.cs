using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_MarcacionCliente", Schema = "dbo")]
public class MarcacionCliente
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idCliente", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdLocalidadSubturnoCliente { get; set; }
    //public virtual LocalidadSubturnoCliente LocalidadSubturnoCliente { get; set; }

    [Column("marcacionEntrada", Order = 2, TypeName = "datetime2")]
    public Nullable<System.DateTime> MarcacionEntrada { get; set; }

    [Column("marcacionSalida", Order = 3, TypeName = "datetime2")]
    public Nullable<System.DateTime> MarcacionSalida { get; set; }

    [Column("estadoMarcacionEntrada", Order = 4, TypeName = "varchar")] // J = JUSTIFICADA    I= INJUSTIFICADA   A=ATRASO   C= CORRECTO
    public string EstadoMarcacionEntrada { get; set; }

    [Column("estadoMarcacionSalida", Order = 5, TypeName = "varchar")] // J = JUSTIFICADA    I= INJUSTIFICADA   A=ATRASO   C= CORRECTO
    public string SalidaEntrada { get; set; }

    [Column("totalAtraso", Order = 6, TypeName = "datetime2")]
    public Nullable<System.DateTime> TotalAtraso { get; set; }

    [Column("estadoProcesado", Order = 7, TypeName = "bit")]
    public bool EstadoProcesado { get; set; }


    //AUDITORIA
    [Column("usuarioCreacion", Order = 8, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 9, TypeName = "datetime2")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 10, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 11, TypeName = "datetime2")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

