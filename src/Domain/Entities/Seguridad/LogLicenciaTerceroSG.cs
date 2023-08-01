using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Seguridad;

[Table("SG_LogLicenciaTercero",Schema = "dbo")]
public class LogLicenciaTerceroSG
{
    [Key]
    [Column("idLogLicenciaTercero", TypeName ="uniqueidentifier")]
    public Guid IdLogLicenciaTercero { get; set; } = Guid.NewGuid();

    [Column("idLicenciaTercero", TypeName = "uniqueidentifier")]
    public Guid IdLicenciaTercero { get; set; }

    //[Column("idServicioTercero", TypeName = "uniqueidentifier")]
    //public Guid IdServicioTercero { get; set; }

    //[Column("tipoLicencia", TypeName = "varchar")]
    //public string TipoLicencia { get; set; }

    [Column("codigoLicencia", TypeName = "varchar")]
    public string CodigoLicencia { get; set; }

    [Column("fechaRenovacion", TypeName = "datetime2")]
    public DateTime FechaRenovacion { get; set; }

    [Column("fechaProximaRenovacion", TypeName = "datetime2")]
    public DateTime FechaProximaRenovacion { get; set; }

    //[Column("fechaProximaRenovacion", TypeName = "datetime2")]
    //public DateTime FechaProximaRenovacion { get; set; }

    [Column("referenciaTecnica", TypeName = "varchar")]
    public string ReferenciaTecnica { get; set; }

    [Column("costoLicencia", TypeName = "numeric")]
    public float CostoLicencia { get; set; }

    //[Column("estado", TypeName = "varchar")]
    //public string Estado { get; set; }

    //[Column("usuarioCreacion", TypeName = "varchar")]
    public string UsuarioCreacion { get; set; }

    [Column("fechaCreacion", TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; }

    [Column("usuarioModificacion", TypeName = "varchar")]
    public string UsuarioModificacion { get; set; }

    [Column("fechaModificacion", TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }







}
