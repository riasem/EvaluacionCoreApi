using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities;

[Table("AS_Subturno", Schema = "dbo")]
public class SubTurno
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("identificacion", Order = 1, TypeName = "varchar")]
    public string Identificacion { get; set; } = string.Empty;

    [Column("idFeature", Order = 1, TypeName = "varchar")]
    public string IdFeature { get; set; } = string.Empty;

    [Column("idSolicitud", Order = 2, TypeName = "varchar")]
    public string IdSolicitud { get; set; } = string.Empty;

    [Column("idTipoSolicitud", Order = 3, TypeName = "varchar")]
    public string IdTipoSolicitud { get; set; } = string.Empty;

    [NotMapped]
    public string ArchivoBase64 { get; set; } = string.Empty;

    [Column("nombreArchivo", Order = 4, TypeName = "varchar")]
    public string NombreArchivo { get; set; } = string.Empty;

    [Column("rutaAcceso", Order = 5, TypeName = "varchar")]
    public string RutaAcceso { get; set; } = string.Empty;

    [Column("estado", Order = 6, TypeName = "varchar")]
    public string Estado { get; set; } = string.Empty;

    //AUDITORIA
    [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 8, TypeName = "datetime")]
    public System.DateTime FechaCreacion { get; set; } = System.DateTime.Now;

    [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 10, TypeName = "datetime")]
    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

