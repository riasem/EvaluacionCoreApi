using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Calendario;

[Table("AS_Calendario", Schema = "dbo")]
public class Calendario
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [NotMapped]
    [Column("fecha", Order = 1, TypeName = "datetime2")]
    public DateTime Fecha { get; set; }

    [Column("idCiudadPais", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdCiudadPais { get; set; }
    public virtual CiudadPais CiudadPais { get; set; }

    [Column("diaDeLaSemana", Order = 3, TypeName = "varchar")]
    public string DiaDeLaSemana { get; set; }

    [Column("esLaborable", Order = 4, TypeName = "bit")]
    public bool Eslaborable { get; set; }

    [Column("esRecuperable", Order = 5, TypeName = "bit")]
    public bool EsRecuperable { get; set; }

    [Column("estado", Order = 6, TypeName = "varchar")]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 8, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 10, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

}
