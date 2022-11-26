using EvaluacionCore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Justificacion;

[NotMapped]
[Table("AS_SolicitudJustificacion", Schema = "dbo")]
public class SolicitudJustificacion
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("codOrganizacion")]
    public int? CodOrganizacion { get; set; }

    [Column("idTipoJustificacion")]
    public Guid IdTipoJustificacion { get; set; }
    public virtual TipoJustificacion TipoJustificacion { get; set; }

    [Column("idEstadoSolicitud")]
    public Guid IdEstadoSolicitud { get; set; }
    public virtual EstadoTarea EstadoTarea { get; set; }

    [Column("identBeneficiario")]
    public string IdentBeneficiario { get; set; }

    [Column("identificacionEmpleado")]
    public string IdentificacionEmpleado { get; set; }

    [Column("idMarcacion")]
    public int? IdMarcacion { get; set; }

    [Column("idTurno")]
    public int? IdTurno { get; set; }
    [Column("marcacionEntrada")]
    public DateTime MarcacionEntrada { get; set; }

    [Column("turnoEntrada")]
    public DateTime TurnoEntrada { get; set; }

    [Column("marcacionSalida")]
    public DateTime MarcacionSalida { get; set; }

    [Column("turnoSalida")]
    public DateTime TurnoSalida { get; set; }

    [Column("comentarios",TypeName ="varchar(255)")]
    public string Comentarios { get; set; }

    [Column("fechaCreacion")]
    public DateTime FechaCreacion { get; set; }

    [Column("usuarioCreacion")]
    public string UsuarioCreacion { get; set; }

    [Column("fechaModificacion")]
    public DateTime? FechaModificacion { get; set; }

    [Column("usuarioModificacion")]
    public string UsuarioModificacion { get; set; }
}
