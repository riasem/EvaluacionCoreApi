using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Entities.Common;

[Table("Machines", Schema = "dbo")]
public class Machines
{
    [Key]
    [Column("ID", TypeName = "int")]
    public int ID { get; set; }

    [Column("MachineAlias", TypeName = "varchar")]
    public string MachineAlias { get; set; }

    [Column("ConnectType", TypeName = "int")]
    public int? ConnectType { get; set; }

    [Column("IP", TypeName = "varchar")]
    public string IP { get; set; }

    [NotMapped]
    public int SerialPort { get; set; }
    [NotMapped]
    public int Port { get; set; }
    [NotMapped]
    public int Baudrate { get; set; }
    [NotMapped]
    public int MachineNumber { get; set; }
    [NotMapped]
    public bool IsHost { get; set; }
    [NotMapped]
    public bool Enabled { get; set; }
    [NotMapped]
    public string CommPassword { get; set; }
    [NotMapped]
    public int UILanguage { get; set; }
    [NotMapped]
    public int DateFormat { get; set; }
    [NotMapped]
    public int InOutRecordWarn { get; set; }
    [NotMapped]
    public int Idle { get; set; }
    [NotMapped]
    public int Voice { get; set; }
    [NotMapped]
    public int managercount { get; set; }
    [NotMapped]
    public int usercount { get; set; }
    [NotMapped]
    public int fingercount { get; set; }
    [NotMapped]
    public int SecretCount { get; set; }
    [NotMapped]
    public string FirmwareVersion { get; set; }
    [NotMapped]
    public int LockControl { get; set; }
    [NotMapped]
    public int Purpose { get; set; }
    [NotMapped]
    public int ProduceKind { get; set; }
    [NotMapped]
    public string sn { get; set; }
    [NotMapped]
    public string PhotoStamp { get; set; }
    [NotMapped]
    public int IsIfChangeConfigServer2 { get; set; }
    [NotMapped]
    public int pushver { get; set; }
    [NotMapped]
    public string create_operator { get; set; }
    [NotMapped]
    public int status { get; set; }
    [NotMapped]
    public int device_type { get; set; }
    [NotMapped]
    public int TransInterval { get; set; }
    [NotMapped]
    public string UpdateDB { get; set; }
    [NotMapped]
    public string device_name { get; set; }
    [NotMapped]
    public int transaction_count { get; set; }
    [NotMapped]
    public int max_user_count { get; set; }
    [NotMapped]
    public int max_finger_count { get; set; }
    [NotMapped]
    public int max_attlog_count { get; set; }
    [NotMapped]
    public string lng_encode { get; set; }
    [NotMapped]
    public string platform { get; set; }
    [NotMapped]
    public int AccFun { get; set; }
    [NotMapped]
    public int TZAdj { get; set; }
    [NotMapped]
    public int comm_type { get; set; }
    [NotMapped]
    public string subnet_mask { get; set; }
    [NotMapped]
    public string gateway { get; set; }
    [NotMapped]
    public int area_id { get; set; }
    [NotMapped]
    public int acpanel_type { get; set; }
    [NotMapped]
    public bool sync_time { get; set; }
    [NotMapped]
    public bool four_to_two { get; set; }
    [NotMapped]
    public int fp_mthreshold { get; set; }
    [NotMapped]
    public int Fpversion { get; set; }
    [NotMapped]
    public int max_comm_size { get; set; }
    [NotMapped]
    public int max_comm_count { get; set; }
    [NotMapped]
    public bool realtime { get; set; }
    [NotMapped]
    public int delay { get; set; }
    [NotMapped]
    public bool encrypt { get; set; }
    [NotMapped]
    public int dstime_id { get; set; }
    [NotMapped]
    public int door_count { get; set; }
    [NotMapped]
    public int reader_count { get; set; }
    [NotMapped]
    public int aux_in_count { get; set; }
    [NotMapped]
    public int aux_out_count { get; set; }
    [NotMapped]
    public int IsOnlyRFMachine { get; set; }
    [NotMapped]
    public int com_port { get; set; }
    [NotMapped]
    public int com_address { get; set; }
    [NotMapped]
    public int SimpleEventType { get; set; }
    [NotMapped]
    public int FvFunOn { get; set; }
    [NotMapped]
    public int fvcount { get; set; }
    [NotMapped]
    public int DevSDKType { get; set; }
    [NotMapped]
    public bool IsTFTMachine { get; set; }
    [NotMapped]
    public int PinWidth { get; set; }
    [NotMapped]
    public int UserExtFmt { get; set; }
    [NotMapped]
    public int FP1_NThreshold { get; set; }
    [NotMapped]
    public int FP1_1Threshold { get; set; }
    [NotMapped]
    public int Face1_NThreshold { get; set; }
    [NotMapped]
    public int Face1_1Threshold { get; set; }
    [NotMapped]
    public int Only1_1Mode { get; set; }
    [NotMapped]
    public int OnlyCheckCard { get; set; }
    [NotMapped]
    public int MifireMustRegistered { get; set; }
    [NotMapped]
    public int RFCardOn { get; set; }
    [NotMapped]
    public int Mifire { get; set; }
    [NotMapped]
    public int MifireId { get; set; }
    [NotMapped]
    public int NetOn { get; set; }
    [NotMapped]
    public int RS232On { get; set; }
    [NotMapped]
    public int RS485On { get; set; }
    [NotMapped]
    public int FreeType { get; set; }
    [NotMapped]
    public int FreeTime { get; set; }
    [NotMapped]
    public int NoDisplayFun { get; set; }
    [NotMapped]
    public int VoiceTipsOn { get; set; }
    [NotMapped]
    public int TOMenu { get; set; }
    [NotMapped]
    public int StdVolume { get; set; }
    [NotMapped]
    public int VRYVH { get; set; }
    [NotMapped]
    public int KeyPadBeep { get; set; }
    [NotMapped]
    public int BatchUpdate { get; set; }
    [NotMapped]
    public int CardFun { get; set; }
    [NotMapped]
    public int FaceFunOn { get; set; }
    [NotMapped]
    public int FaceCount { get; set; }
    [NotMapped]
    public string FingerFunOn { get; set; }
    [NotMapped]
    public string CompatOldFirmware { get; set; }
    [NotMapped]
    public string ParamValues { get; set; }

}
