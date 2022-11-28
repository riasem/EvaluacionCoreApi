using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Permisos;

[Table("AS_SolicitudPermiso", Schema = "dbo")]
public class SolicitudPermiso
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("codOrganizacion")]
    public int? CodOrganizacion { get; set; }

    [Column("idTipoPermiso")]
    public Guid IdTipoPermiso { get; set; }
    public virtual TipoPermiso TipoPermiso { get; set; }

    [Column("idEstadoSolicitud")]
    public Guid IdEstadoSolicitud { get; set; }
    public virtual EstadoTarea EstadoTarea { get; set; }

    [Column("numeroSolicitud ")]
    public int NumeroSolicitud { get; set; }

    [Column("idSolicitante ")]
    public int IdSolicitante { get; set; }

    [Column("idBeneficiario ")]
    public int IdBeneficiario { get; set; }

    [Column("identificacionEmpleado")]
    public string IdentificacionEmpleado { get; set; }

    [Column("porHoras ")]
    public bool? PorHoras { get; set; }

    [Column("fechaDesde ")]
    public DateTime FechaDesde { get; set; }

    [Column("horaInicio ")]
    public string HoraInicio { get; set; }

    [Column("fechaHasta ")]
    public DateTime FechaHasta { get; set; }

    [Column("horaFin ")]
    public string HoraFin { get; set; }

    [Column("cantidadHoras ")]
    public DateTime? CantidadHoras { get; set; }

    [Column("cantidadDias ")]
    public int? CantidadDias { get; set; }

    [Column("observacion ")]
    public string Observacion { get; set; }

    [Column("fechaCreacion ")]
    public DateTime FechaCreacion { get; set; }
}
