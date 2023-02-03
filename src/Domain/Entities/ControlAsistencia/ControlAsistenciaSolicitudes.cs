using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.ControlAsistencia;

[Table("controlAsistenciaSolicitudes", Schema = "dbo")]
public class ControlAsistenciaSolicitudes
{
    [Key]
    [Column("id")]
    public int Id { get; set; }    

    [Column("idControlAsistenciaDet")]
    public int IdControlAsistenciaDet { get; set; }

    [Column("idFeature")]
    public Guid IdFeature { get; set; }

    [Column("idSolicitud")]
    public Guid IdSolicitud { get; set; }

    [Column("colaborador")]
    public string Colaborador { get; set; }

    [Column("comentarios")]
    public string Comentarios { get; set; }
    
}
