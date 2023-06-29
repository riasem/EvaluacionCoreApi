using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Marcaciones;
[Table("V_DISPOSITIVOS_MARCACIONES", Schema = "dbo")]
public class DispositivoMarcacion
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Column("nombre", TypeName = "nvarchar")]
    public string Nombre { get; set; }
    [Column("identificacion", TypeName ="varchar")]
    public string Identificacion { get; set; }

    [Column("estado", TypeName ="varchar")]
    public string Estado { get; set; }
    [Column("apiLuxand", TypeName="bit")]

    public bool ApiLuxand { get; set; }

    [Column("desUdn", TypeName = "varchar")]
    public string DesUdn { get; set; }
}
