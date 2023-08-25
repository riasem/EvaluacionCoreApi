using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Marcaciones;

[Table("acc_log_marcacion_offline", Schema = "dbo")]
public class AccLogMarcacionOffline
{
    [Key]
    [Column("id", TypeName = "uniqueidentifier")]
	public Guid Id { get; set; } = Guid.NewGuid();

    [Column("fechaSincronizacion",TypeName = "datetime2")]
	public DateTime FechaSincronizacion { get; set; }

    [Column("fechaInicio",TypeName = "datetime2")]
	public DateTime? FechaInicio { get; set; }

    [Column("fechaFin",TypeName = "datetime2")]
	public DateTime? FechaFin { get; set; }

    [Column("identificacionInicio",TypeName = "varchar")]
	public string IdentificacionInicio { get; set; }

	[Column("identificacionFin", TypeName = "varchar")]
	public string IdentificacionFin { get; set; }

    [Column("totalMarcacion",TypeName = "int")]
	public int TotalMarcacion { get; set; }

	[Column("totalSincronizadas", TypeName = "int")]
	public int TotalSincronizadas { get; set; } = 0;

    [Column("fechaCreacion",TypeName = "datetime2")]	
	public DateTime FechaCreacion { get; set; }

    [Column("usuarioCreacion", TypeName = "varchar")]
	public string UsuarioCreacion { get; set; }

    [Column("fechaModificacion",TypeName = "datetime2")]
	public DateTime? FechaModificacion { get; set; }

    [Column("usuarioModificacion", TypeName = "varchar")]
	public string UsuarioModificacion { get; set; }

    [Column("estado", TypeName = "varchar")]
	public string Estado { get; set; }

	[Column("tipoCarga", TypeName = "varchar")]
	public string TipoCarga { get; set; }

    [Column("tipoComunicacion", TypeName = "int")]
    public int? TipoComunicacion{ get; set; }
}
