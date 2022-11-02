using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_LocalidadCliente", Schema = "dbo")]
public class LocalidadCliente
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idLocalidad", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdLocaliad { get; set; }
    //public virtual Localidad Localidad { get; set; } 

    [Column("idCliente", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdCliente { get; set; }
    //public virtual Cliente Cliente { get; set; }
   
    [Column("estado", Order = 3, TypeName = "varchar")]
    public string Estado { get; set; } = "A";


    //AUDITORIA
    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 5, TypeName = "datetime2")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

    //public virtual ICollection<LocalidadSubturnoCliente> LocalidadSubturnoClientes { get; set; }

}

