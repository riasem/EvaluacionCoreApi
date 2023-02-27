using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Marcaciones;

[Table("AlertasNovedadMarcacion", Schema = "dbo")]
public class AlertasNovedadMarcacion
{
    [Column("id", Order = 0, TypeName = "int")]
    public int Id { get; set; }
    
    [Column("idMarcacion", Order = 0, TypeName = "int")]
    public int IdMarcacion { get; set; }
    
    [Column("usuarioMarcacion", Order = 0, TypeName = "int")]
    public int UsuarioMarcacion { get; set; }
    
    [Column("fechaMarcacion", Order = 0, TypeName = "datetime")]
    public DateTime FechaMarcacion { get; set; }
    
    [Column("canal", Order = 0, TypeName = "varchar")]
    public string Canal { get; set; }
    
    [Column("dispositivo", Order = 0, TypeName = "varchar")]
    public string Dispositivo { get; set; }
    
    [Column("tipoNovedad", Order = 0, TypeName = "varchar")]
    public string TipoNovedad { get; set; }
    
    [Column("descripcionMensaje", Order = 0, TypeName = "varchar")]
    public string DescripcionMensaje { get; set; }
    
    [Column("fechaCreacion", Order = 0, TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }
    
    [Column("usuarioCreacion", Order = 0, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; }


}
