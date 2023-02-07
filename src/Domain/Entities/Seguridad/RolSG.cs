using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Seguridad;

[Table("SG_Rol", Schema = "dbo")]
public class RolSG
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("canalId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid CanalSGId { get; set; }
    public virtual CanalSG CanalSG { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    public string Codigo { get; set; }

    [Column("nombre", Order = 3, TypeName = "varchar")]
    public string Nombre { get; set; }

    [Column("descripcion", Order = 4, TypeName = "varchar")]
    public string Descripcion { get; set; }

    [Column("rolPadreId", Order = 5, TypeName = "uniqueidentifier")]
    public Guid? RolPadreId { get; set; }

    [Column("estado", Order = 6, TypeName = "varchar")]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; }

    [Column("fechaCreacion", Order = 8, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; }

    [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; }

    [Column("fechaModificacion", Order = 10, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }
}