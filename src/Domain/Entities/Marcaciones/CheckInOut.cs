using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.Horario;

[Table("CHECKINOUT",Schema = "dbo")]
public class CheckInOut
{
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("USERID", TypeName = "int")]
    public int? UserId { get; set; }
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CHECKTIME", TypeName = "datetime")]
    public DateTime CheckTime { get; set; }

    [Column("CHECKTYPE", TypeName = "varchar")]
    public string CheckType { get; set; }

    [Column("VERIFYCODE", TypeName = "int")]
    public int? VerifyCode { get; set; }

    [Column("SENSORID", TypeName = "varchar")]
    public string SensorId { get; set; }

    [Column("LOGID", TypeName = "int")]
    public int? LogId { get; set; } = 0;

    [Column("MachineId", TypeName = "int")]
    public int? MachineId { get; set; }

    [Column("UserExtFmt", TypeName = "int")]
    public int? UserExtFmt { get; set; }

    [Column("WorkCode", TypeName = "int")]
    public int? WorkCode { get; set; }

    [Column("Memoinfo", TypeName = "varchar")]
    public string MemoInfo { get; set; }

    [Column("sn", TypeName = "varchar")]
    public string Sn { get; set; }
}
