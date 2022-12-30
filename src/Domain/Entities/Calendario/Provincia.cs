using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Calendario;

[Table("AS_Provincia", Schema = "dbo")]
public class Provincia
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("idPais", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdPais { get; set; }
    public virtual Pais Pais { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    [StringLength(10)]
    public string Codigo { get; set; }

    [Column("descripcion", Order = 3,TypeName = "varchar")]
    [StringLength(50)]
    public string Descripcion { get; set; }

    [Column("estado", Order = 4, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 5, TypeName = "varchar")]
    [StringLength(50)]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 6, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 7, TypeName = "varchar")]
    [StringLength(50)]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 8, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Canton> Cantones { get; set; }

}
