using EnrolApp.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.Common;

[Table("WF_Cargo", Schema = "dbo")]
public class Cargo
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [NotMapped]
    [Column("departamentoId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid DepartamentoId { get; set; }
    

    [Column("nombre", Order = 2, TypeName = "varchar")]
    public string Nombre { get; set; }

    [Column("descripcion", Order = 3, TypeName = "varchar")]
    public string Descripcion { get; set; }

    [Column("cargoPadreId", Order = 4, TypeName = "uniqueidentifier")]
    public Guid CargoPadreId { get; set; }

    [Column("estado", Order = 5, TypeName = "varchar")]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 6, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 7, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 8, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 9, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Cliente> Cliente { get; set; }
}
