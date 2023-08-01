using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Seguridad;

[Table("SG_ServicioTercero", Schema = "dbo")]
public class ServicioTerceroSG
{
    [Key]
    [Column("idServicioTercero", TypeName = "uniqueidentifier")]
    public Guid idServicioTercero { get; set; }

    [Column("nombreTercero",TypeName = "varchar")]
    public string NombreTercero { get; set; }

    [Column("nombreServicio",TypeName = "varchar")]
    public string NombreServicio { get; set; }

    [Column("descripcionServicio",TypeName = "varchar")]
    public string DescripcionServicio { get; set; }

    [Column("fechaInicioServicio",TypeName = "datetime2")]
    public DateTime FechaInicioServicio { get; set; }

    [Column("fechaFinalizacionServicio",TypeName = "datetime2")]
    public DateTime? FechaFinalizacionServicio { get; set; }

    [Column("referenciaTecnica", TypeName = "varchar")]
    public string ReferenciaTecnica { get; set; }

    [Column("estado", TypeName = "varchar")]
    public string Estado { get; set; }

    [Column("usuarioCreacion", TypeName = "varchar")]
    public string UsuarioCreacion { get; set; }

    [Column("fechaCreacion", TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; }

    [Column("usuarioModificacion", TypeName = "varchar")]
    public string UsuarioModificacion { get; set; }

    [Column("fechaModificacion", TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<LicenciaTerceroSG> LicenciaTerceroSG { get; set; }

}
