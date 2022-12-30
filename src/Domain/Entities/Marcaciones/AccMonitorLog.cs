using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Entities.Marcaciones;

[Table("acc_monitor_log", Schema = "dbo")]
public class AccMonitorLog
{
    [Column("id",Order = 0, TypeName = "int")]
    public int Id { get; set; }

    [Column("change_operator", Order = 1)]
    public string Change_Operator { get; set; }

    [Column("change_time", Order = 2)]
    public DateTime? Change_Time { get; set; }

    [Column("create_operator", Order = 3)]
    public string Create_Operator { get; set; }

    [Column("create_time", Order = 4 )]
    public DateTime? Create_Time { get; set; }

    [Column("delete_operator", Order = 5)]
    public string Delete_Operator { get; set; }

    [Column("delete_time", Order = 6)]
    public DateTime? Delete_Time { get; set; }

    [Column("status", Order = 7)]
    public int Status { get; set; }

    [Column("log_tag", Order = 8)]
    public int? Log_Tag { get; set; }

    [Column("time", Order = 9)]
    public DateTime Time { get; set; }

    [Column("pin", Order = 10)]
    public string Pin { get; set; }

    [Column("card_no", Order = 11)]
    public string Card_No { get; set; }

    [Column("device_id", Order = 12)]
    public int? Device_Id { get; set; }

    [Column("device_sn", Order = 13)]
    public string Device_Sn { get; set; }

    [Column("device_name", Order = 14)]
    public string Device_Name { get; set; }

    [Column("verified", Order = 15)]
    public int? Verified { get; set; }

    [Column("state", Order = 16)]
    public int? State { get; set; }

    [Column("event_type", Order = 17)]
    public int? Event_Type { get; set; }

    [Column("description", Order = 18)]
    public string Description { get; set; }

    [Column("event_point_type", Order = 19)]
    public int? Event_Point_Type { get; set; }

    [Column("event_point_id", Order = 20)]
    public int? Event_Point_Id { get; set; }

    [Column("event_point_name", Order = 21)]
    public string Event_Point_Name { get; set; }

}
