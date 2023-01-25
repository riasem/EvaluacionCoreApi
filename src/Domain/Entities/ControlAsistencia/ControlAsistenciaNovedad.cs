using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.ControlAsistencia;

[Table("ControlAsistenciaNovedad", Schema = "dbo")]
public class ControlAsistenciaNovedad
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idControlAsistenciaDet")] 
    public int IdControlAsistenciaDet { get; set; }

    [Column("fecha")]
    public DateTime Fecha { get; set; }

    [Column("colaborador")]
    public string Colaborador { get; set; }

    [Column("descripcion")]
    public string Descripcion { get; set; }

    [Column("minutosNovedad")]
    public string MinutosNovedad { get; set; }

    [Column("estadoMarcacion")]
    public string EstadoMarcacion { get; set; }


}
