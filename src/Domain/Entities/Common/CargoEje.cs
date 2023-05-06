using System.ComponentModel.DataAnnotations.Schema;


namespace EvaluacionCore.Domain.Entities.Common
{
    [Table("SG_CargoEjes", Schema = "dbo")]
    public class CargoEje
    {
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("identificacion", Order = 1, TypeName = "varchar")]
        public string Identificacion { get; set; }

        [Column("idUdn", Order = 2, TypeName = "uniqueidentifier")]
        public Guid IdUdn { get; set; }

        [Column("idCargo", Order = 3, TypeName = "uniqueidentifier")]
        public Guid IdCargo { get; set; }

        //public virtual CargoSG CargoSG { get; set; }

        [Column("estado", Order = 4, TypeName = "varchar")]
        public string Estado { get; set; }

        [Column("usuarioCreacion", Order = 5, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; } = string.Empty;

        [Column("fechaCreacion", Order = 6, TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Column("usuarioModificacion", Order = 7, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; } = string.Empty;

        [Column("fechaModificacion", Order = 8, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }

        [Column("deviceId", Order = 9, TypeName = "int")]
        public int? DeviceId { get; set; }

        [Column("deviceName", Order = 10, TypeName = "varchar")]
        public string DeviceName { get; set; }
    }
}
