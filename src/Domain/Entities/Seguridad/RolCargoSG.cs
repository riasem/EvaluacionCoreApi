using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Seguridad;

[Table("SG_RolCargo", Schema = "dbo")]
public class RolCargoSG
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("rolId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid RolSGId { get; set; }
    public virtual RolSG RolSG { get; set; }

    [Column("cargoId", Order = 2, TypeName = "uniqueidentifier")]
    public Guid CargoSGId { get; set; }
    public virtual CargoSG CargoSG { get; set; }

    [Column("estado", Order = 3, TypeName = "varchar")]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; }

    [Column("fechaCreacion", Order = 5, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; }

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; }

    [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }
}
