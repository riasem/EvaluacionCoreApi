using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionCore.Domain.Entities.ControlAsistencia;

[Table("V_CASISTENCIA", Schema = "dbo")]
public class ControlAsistencia_V
{
    [Key]
    [Column("codigoBiometrico")]
    public string CodigoBiometrico { get; set; }

    [Column("idTurno")]
    public Guid? IdTurno { get; set; }

    [Column("codigoTurno")]
    public string CodigoTurno { get; set; }

    [Column("claseTurno")]
    public string ClaseTurno { get; set; }

    [Column("fechaAsginacion")]
    public DateTime? FechaAsginacion { get; set; }

    [Column("fechaTurno")]
    public DateTime? FechaTurno { get; set; }

    [Column("entrada")]
    public DateTime? Entrada { get; set; }

    [Column("salida")]
    public DateTime? Salida { get; set; }

    [Column("totalHoras")]
    public string TotalHoras { get; set; }

    [Column("fechaHoraIngreso")]
    public DateTime? FechaHoraIngreso { get; set; }

    [Column("minutosNovedadIngreso")]
    public int? MinutosNovedadIngreso { get; set; }

    [Column("novedadIngreso")]
    public string NovedadIngreso { get; set; }

    [Column("estadoIngreso")]
    public string EstadoIngreso { get; set; }

    [Column("idSolicitudIngreso")]
    public Guid? IdSolicitudIngreso { get; set; }

    [Column("idFeatureIngreso")]
    public Guid? IdFeatureIngreso { get; set; }

    [Column("fechaSolicitudIngreso")]
    public DateTime? FechaSolicitudIngreso { get; set; }

    [Column("usuarioSolicitudIngreso")]
    public string UsuarioSolicitudIngreso { get; set; }

    [Column("estadoSolicitudIngreso")]
    public Guid? EstadoSolicitudIngreso { get; set; }

    [Column("fechaHoraSalida")]
    public DateTime? FechaHoraSalida { get; set; }

    [Column("minutosNovedadSalida")]
    public int? MinutosNovedadSalida { get; set; }

    [Column("novedadSalida")]
    public string NovedadSalida { get; set; }

    [Column("estadoSalida")]
    public string EstadoSalida { get; set; }

    [Column("idSolicitudSalida")]
    public Guid? IdSolicitudSalida { get; set; }

    [Column("idFeatureSalida")]
    public Guid? IdFeatureSalida { get; set; }

    [Column("fechaSolicitudSalida")]
    public DateTime? FechaSolicitudSalida { get; set; }

    [Column("usuarioSolicitudSalida")]
    public string UsuarioSolicitudSalida { get; set; }

    [Column("estadoSolicitudSalida")]
    public Guid? EstadoSolicitudSalida { get; set; }

    [Column("fechaHoraEntradaReceso")]
    public DateTime? FechaHoraEntradaReceso { get; set; }

    [Column("minutosNovedadEntradaReceso")]
    public int? MinutosNovedadEntradaReceso { get; set; }

    [Column("novedadEntradaReceso")]
    public string NovedadEntradaReceso { get; set; }

    [Column("fechaHoraSalidaReceso")]
    public DateTime? FechaHoraSalidaReceso { get; set; }

    [Column("minutosNovedadSalidaReceso")]
    public int? MinutosNovedadSalidaReceso { get; set; }

    [Column("novedadSalidaReceso")]
    public string NovedadSalidaReceso { get; set; }
}