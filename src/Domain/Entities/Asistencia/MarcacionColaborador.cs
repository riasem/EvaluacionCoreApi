﻿using System.ComponentModel.DataAnnotations.Schema;
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

    [Column("margenEntradaPrevio", Order = 4, TypeName = "varchar")]
    [StringLength(4)] public string MargenEntradaPrevio { get; set; }

    [Column("margenEntradaPosterior", Order = 5, TypeName = "varchar")]
    [StringLength(4)] public string MargenEntradaPosterior { get; set; }

    [Column("marcacionSalida", Order = 6, TypeName = "datetime2")]
    public DateTime? MarcacionSalida { get; set; }

    [Column("margenSalidaPrevio", Order = 7, TypeName = "varchar")]
    [StringLength(4)] public string MargenSalidaPrevio { get; set; }

    [Column("margenSalidaPosterior", Order = 8, TypeName = "varchar")]
    [StringLength(4)] public string MargenSalidaPosterior { get; set; }

    [Column("estadoMarcacionEntrada", Order = 9, TypeName = "varchar")] // J = JUSTIFICADA    I= INJUSTIFICADA   A=ATRASO   C= CORRECTO
    public string EstadoMarcacionEntrada { get; set; }

    [Column("estadoMarcacionSalida", Order = 10, TypeName = "varchar")] // J = JUSTIFICADA    I= INJUSTIFICADA   A=ATRASO   C= CORRECTO
    public string SalidaEntrada { get; set; }

    [Column("totalAtraso", Order = 11, TypeName = "datetime2")]
    public DateTime? TotalAtraso { get; set; }

    [Column("estadoProcesado", Order = 12, TypeName = "bit")]
    public bool EstadoProcesado { get; set; }


    //AUDITORIA
    [Column("usuarioCreacion", Order = 13, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 14, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 15, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 16, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

}

