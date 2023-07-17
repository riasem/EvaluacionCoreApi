using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Seguridad;

[Table("SG_Cargo", Schema = "dbo")]
public class CargoSG
{
    [Key]
    [Column("id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("nombre", TypeName = "varchar")]
    public string Nombre { get; set; }

    [Column("descripcion", TypeName = "varchar")]
    public string Descripcion { get; set; }

    [Column("codigoHomologacion", TypeName = "varchar")]
    public string CodigoHomologacion { get; set; }

    [Column("nombreHomologacion", TypeName = "varchar")]
    public string NombreHomologacion { get; set; }

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

    [Column("codigo", TypeName = "varchar")]
    public string Codigo { get; set; }

    public virtual ICollection<CargoDepartamentoSG> CargoDepartamentoSG { get; set; }


}
