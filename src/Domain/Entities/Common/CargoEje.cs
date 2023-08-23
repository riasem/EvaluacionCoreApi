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

        [Column("apiLuxand", Order = 11, TypeName = "bit")]
        public bool ApiLuxand { get; set; }

        [Column("similarityOnline", Order = 12, TypeName = "numeric")]
        public float SimilarityOnline { get; set; }

        [Column("similarityOffline", Order = 13, TypeName = "numeric")]
        public float SimilarityOffline { get; set; }

        [Column("sdkLuxandOnline", Order = 14, TypeName = "bit")]
        public bool SdkLuxandOnline { get; set; }

        [Column("sdkLuxandOffline", Order = 15, TypeName = "bit")]
        public bool SdkLuxandOffline { get; set; }

        [Column("cronUnoStatus", Order = 16, TypeName = "bit")]
        public bool CronUnoStatus { get; set; }

        [Column("cronUnoHorario", Order = 17, TypeName = "datetime")]
        public DateTime? CronUnoHorario { get; set; }

        [Column("cronDosStatus", Order = 18, TypeName = "bit")]
        public bool CronDosStatus { get; set; }

        [Column("cronDosHorario", Order = 19, TypeName = "datetime")]
        public DateTime? CronDosHorario { get; set; }

        [Column("cronTresStatus", Order = 20, TypeName = "bit")]
        public bool CronTresStatus { get; set; }

        [Column("cronTresHorario", Order = 21, TypeName = "datetime")]
        public DateTime? CronTresHorario { get; set; }

        [Column("cronCuatroStatus", Order = 22, TypeName = "bit")]
        public bool CronCuatroStatus { get; set; }

        [Column("cronCuatroHorario", Order = 23, TypeName = "datetime")]
        public DateTime? CronCuatroHorario { get; set; }

        [Column("cronCincoStatus", Order = 24, TypeName = "bit")]
        public bool CronCincoStatus { get; set; }

        [Column("cronCincoHorario", Order = 25, TypeName = "datetime")]
        public DateTime? CronCincoHorario { get; set; }

        [Column("cronSeisStatus", Order = 26, TypeName = "bit")]
        public bool CronSeisStatus { get; set; }

        [Column("cronSeisHorario", Order = 27, TypeName = "datetime")]
        public DateTime? CronSeisHorario { get; set; }

        [Column("cronSieteStatus", Order = 28, TypeName = "bit")]
        public bool CronSieteStatus { get; set; }

        [Column("cronSieteHorario", Order = 29, TypeName = "datetime")]
        public DateTime? CronSieteHorario { get; set; }

        [Column("cronOchoStatus", Order = 30, TypeName = "bit")]
        public bool CronOchoStatus { get; set; }

        [Column("cronOchoHorario", Order = 31, TypeName = "datetime")]
        public DateTime? CronOchoHorario { get; set; }

        [Column("cronNueveStatus", Order = 32, TypeName = "bit")]
        public bool CronNueveStatus { get; set; }

        [Column("cronNueveHorario", Order = 33, TypeName = "datetime")]
        public DateTime? CronNueveHorario { get; set; }

        [Column("cronDiezStatus", Order = 34, TypeName = "bit")]
        public bool CronDiezStatus { get; set; }

        [Column("cronDiezHorario", Order = 35, TypeName = "datetime")]
        public DateTime? CronDiezHorario { get; set; }
    }
}
