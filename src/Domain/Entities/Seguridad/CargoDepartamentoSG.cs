using EvaluacionCore.Domain.Entities.Organizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Seguridad;

[Table("SG_CargoDepartamento", Schema = "dbo")]
public class CargoDepartamentoSG
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("departamentoId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid DepartamentoId { get; set; }
    public virtual Departamento Departamento { get; set; }

    //[Column("nombre", Order = 2, TypeName = "varchar")]
    //public string Nombre { get; set; }

    //[Column("descripcion", Order = 3, TypeName = "varchar")]
    //public string Descripcion { get; set; }

    //[Column("codigoHomologacion", Order = 4, TypeName = "varchar")]
    //public string CodigoHomologacion { get; set; }

    //[Column("nombreHomologacion", Order = 5, TypeName = "varchar")]
    //public string NombreHomologacion { get; set; }

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

    [Column("idCargo", TypeName = "uniqueidentifier")]
    public Guid IdCargo { get; set; }

    public virtual CargoSG CargoSG { get; set; }

    //public virtual ICollection<CargoEje> CargoEje { get; set; }
}
