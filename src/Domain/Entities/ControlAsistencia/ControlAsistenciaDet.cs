using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.ControlAsistencia;

[Table("ControlAsistenciaDet", Schema = "dbo")]
public class ControlAsistenciaDet
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idControlAsistenciaCab")]
    public int IdControlAsistenciaCab { get; set; }
    
    [Column("fecha")]
    public DateTime Fecha { get; set; }
    
    [Column("colaborador")]
    public string Colaborador { get; set; }

    [Column("idTurnoLaboral")]
    public Guid IdTurnoLaboral { get; set; }

    [Column("entradaLaboral")] 
    public DateTime EntradaLaboral { get; set; }

    [Column("idMarcacionEntradaLaboral")]
    public int IdMarcacionEntradaLaboral { get; set; }

    [Column("marcacionEntradaLaboral")]
    public DateTime MarcacionEntradaLaboral { get; set; }

    [Column("salidaLaboral")]
    public DateTime SalidaLaboral { get; set; }

    [Column("idMarcacionSalidaLaboral")]
    public int IdMarcacionSalidaLaboral { get; set; }

    [Column("marcacionSalidaLaboral")]
    public DateTime MarcacionSalidaLaboral { get; set; }

    [Column("totalHorasLaboral")]
    public string TotalHorasLaboral { get; set; }

    [Column("tipoSolicitudEntradaLaboral")]
    public string TipoSolicitudEntradaLaboral { get; set; }

    [Column("idSolicitudEntradaLaboral")]
    public Guid IdSolicitudEntradaLaboral { get; set; }

    [Column("idFeatureEntradaLaboral")]
    public Guid IdFeatureEntradaLaboral { get; set; }

    [Column("estadoEntradaLaboral")]
    public string EstadoEntradaLaboral { get; set; }

    [Column("fechaSolicitudEntradaLaboral")]
    public DateTime FechaSolicitudEntradaLaboral { get; set; }

    [Column("usuarioSolicitudEntradaLaboral")]
    public string UsuarioSolicitudEntradaLaboral { get; set; }

    [Column("tipoSolicitudSalidaLaboral")]
    public string TipoSolicitudSalidaLaboral { get; set; }

    [Column("estadoSalidaLaboral")]
    public string EstadoSalidaLaboral { get; set; }

    [Column("usuarioSolicitudSalidaLaboral")]
    public string UsuarioSolicitudSalidaLaboral { get; set; }

    [Column("idTurnoReceso")]
    public Guid IdTurnoReceso { get; set; }

    [Column("entradaReceso")]
    public DateTime EntradaReceso { get; set; }

    [Column("idMarcacionEntradaReceso")]
    public int IdMarcacionEntradaReceso { get; set; }

    [Column("marcacionEntradaReceso")]
    public DateTime MarcacionEntradaReceso { get; set; }

    [Column("salidaReceso")]
    public DateTime SalidaReceso { get; set; }

    [Column("idMarcacionSalidaReceso")]
    public int IdMarcacionSalidaReceso { get; set; }

    [Column("marcacionSalidaReceso")]
    public DateTime MarcacionSalidaReceso { get; set; }

    [Column("totalHorasReceso")]
    public string TotalHorasReceso { get; set; }

    [Column("tipoSolicitudEntradaReceso")]
    public string TipoSolicitudEntradaReceso { get; set; }

    [Column("estadoEntradaReceso")]
    public string EstadoEntradaReceso { get; set; }

    [Column("usuarioSolicitudEntradaReceso")]
    public string UsuarioSolicitudEntradaReceso { get; set; }

    [Column("tipoSolicitudSalidaReceso")]
    public string TipoSolicitudSalidaReceso { get; set; }

    [Column("estadoSalidaReceso")]
    public string EstadoSalidaReceso { get; set; }

    [Column("usuarioSolicitudSalidaReceso")]
    public string UsuarioSolicitudSalidaReceso { get; set; }

    [Column("fechaSolicitudSalidaLaboral")]
    public DateTime? FechaSolicitudSalidaLaboral { get; set; }

    [Column("idSolicitudSalidaLaboral")]
    public Guid? IdSolicitudSalidaLaboral { get; set; }
    
    [Column("idFeatureSalidaLaboral")]
    public Guid? IdFeatureSalidaLaboral { get; set; }
}


