using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Common
{
    [Table("AD_Adjuntos", Schema = "dbo")]

    public class Adjunto
    {
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("identificacion", Order = 1, TypeName = "varchar")]
        public string Identificacion { get; set; } = string.Empty;

        [Column("idFeature", Order = 2, TypeName = "varchar")]
        public string IdFeature { get; set; } = string.Empty;

        [Column("idSolicitud", Order = 3, TypeName = "varchar")]
        public string IdSolicitud { get; set; } = string.Empty;

        [Column("idTipoSolicitud", Order = 3, TypeName = "varchar")]
        public string IdTipoSolicitud { get; set; } = string.Empty;

        [NotMapped]
        public string ArchivoBase64 { get; set; } = string.Empty;

        [Column("nombreArchivo", Order = 4, TypeName = "varchar")]
        public string NombreArchivo { get; set; } = string.Empty;

        [NotMapped]
        public string ExtensionArchivo { get; set; } = string.Empty;

        [Column("rutaAcceso", Order = 5, TypeName = "varchar")]
        public string RutaAcceso { get; set; } = string.Empty;

        [Column("estado", Order = 6, TypeName = "varchar")]
        public string Estado { get; set; } = string.Empty;

        [NotMapped]
        public string UsuarioCreacion { get; set; } = string.Empty;

        [NotMapped]
        public DateTime? FechaCreacion { get; set; }

        [NotMapped]
        public string UsuarioModificacion { get; set; } = string.Empty;

        [NotMapped]
        public DateTime? FechaModificacion { get; set; }
    }
}
