using EvaluacionCore.Domain.Entities.Justificacion;
using EvaluacionCore.Domain.Entities.Permisos;
using EvaluacionCore.Domain.Entities.Vacaciones;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities;


[Table("WF_EstadoTarea", Schema = "dbo")]
public class EstadoTarea
{
    [Key]
	[Column("idEstado")]
	public Guid IdEstado { get; set; }

	[Column("codigo", TypeName = "varchar(20)")]
	public string Codigo { get; set; }

	[Column("descripcion", TypeName = "varchar(100)")]
	public string Descripcion { get; set; }

    [Column("fechaCreacion")]
	public DateTime? FechaCreacion { get; set; }

    [Column("usuarioCreacion")]
	public string UsuarioCreacion { get; set; }

    [Column("fechaModificacion")]
	public DateTime? FechaModificacion { get; set; }

    [Column("usuarioModificacion")]
	public string UsuarioModificacion { get; set; }

	public virtual ICollection<SolicitudPermiso> SolicitudPermiso { get; set; }
	public virtual ICollection<SolicitudJustificacion> SolicitudJustificacion { get; set; }
	public virtual ICollection<SolicitudVacacion> SolicitudVacaion{ get; set; }
}