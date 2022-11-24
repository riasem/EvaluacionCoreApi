using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.Horario;
[Table("USERINFO", Schema = "dbo")]
public class UserInfo
{
    [Key]
    [Column("USERID", TypeName = "int")]
    public int UserId { get; set; }

    [Column("Badgenumber", TypeName = "nvarchar")]
    public string Badgenumber { get; set; }

    [Column("SSN", TypeName = "nvarchar")]
    public string Ssn { get; set; }

    [Column("Name", TypeName = "nvarchar")]
    public string Name { get; set; }

    [NotMapped]
    public string Gender { get; set; }

    [NotMapped]
    public string Title { get; set; }

    [NotMapped]
    public string Pager { get; set; }

    [NotMapped]
    public DateTime? Birthday { get; set; }

    [NotMapped]
    public DateTime? Hiredday { get; set; }

    [NotMapped]
    public string Street { get; set; }

    [NotMapped]
    public string City { get; set; }

    [NotMapped]
    public string State { get; set; }

    [NotMapped]
    public string Zip { get; set; }
    [NotMapped]
    public string OPHONE { get; set; }

    [NotMapped]
    public string FPHONE { get; set; }

    [NotMapped]
    public int? VERIFICATIONMETHOD { get; set; }

    [Column("DEFAULTDEPTID", TypeName = "varchar")]
    public string DefaultDeptId { get; set; }

    [NotMapped]
    public int? SECURITYFLAGS { get; set; }
    [NotMapped]
    public int? ATT { get; set; }
    [NotMapped]
    public int? INLATE { get; set; }
    [NotMapped]
    public int? OUTEARLY { get; set; }
    [NotMapped]
    public int? OVERTIME { get; set; }
    [NotMapped]
    public int? SEP { get; set; }
    [NotMapped]
    public int? HOLIDAY { get; set; }
    [NotMapped]
    public string MINZU { get; set; }
    [NotMapped]
    public string PASSWORD { get; set; }
    [NotMapped]
    public int? LUNCHDURATION { get; set; }
    [NotMapped]
    public string[] PHOTO { get; set; }

    [NotMapped]
    public string mverifypass { get; set; }
    [NotMapped]
    public string[] Notes { get; set; }

    [NotMapped]
    public int? privilege { get; set; }
    [NotMapped]
    public int? InheritDeptSch { get; set; }
    [NotMapped]
    public int? InheritDeptSchClass { get; set; }
    [NotMapped]
    public int? AutoSchPlan { get; set; }
    [NotMapped]
    public int? MinAutoSchInterval { get; set; }
    [NotMapped]
    public int? RegisterOT { get; set; }
    [NotMapped]
    public int? InheritDeptRule { get; set; }
    [NotMapped]
    public int? EMPRIVILEGE { get; set; }
    [NotMapped]
    public string CardNo { get; set; }
    [NotMapped]
    public string change_operator { get; set; }
    [NotMapped]
    public DateTime? change_time { get; set; }

    [Column("create_operator", TypeName = "varchar")]
    public string CreateOperator { get; set; }

    [Column("create_time", TypeName = "datetime2")]
    public DateTime? CreateTime { get; set; }
    [NotMapped]
    public string delete_operator { get; set; }
    [NotMapped]
    public DateTime? delete_time { get; set; }
    [NotMapped]
    public int? status { get; set; }

    [Column("lastname", TypeName = "nvarchar")]
    public string LastName { get; set; }
    [NotMapped]
    public int? AccGroup { get; set; }
    [NotMapped]
    public string TimeZones { get; set; }
    [NotMapped]
    public string identitycard { get; set; }
    [NotMapped]
    public DateTime? UTime { get; set; }
    [NotMapped]
    public string Education { get; set; }
    [NotMapped]
    public int? OffDuty { get; set; }
    [NotMapped]
    public int? DelTag { get; set; }
    [NotMapped]
    public int? morecard_group_id { get; set; }
    [NotMapped]
    public bool set_valid_time { get; set; }
    [NotMapped]
    public DateTime? acc_startdate { get; set; }
    [NotMapped]
    public DateTime? acc_enddate { get; set; }
    [NotMapped]
    public string birthplace { get; set; }
    [NotMapped]
    public string Political { get; set; }
    [NotMapped]
    public string contry { get; set; }
    [NotMapped]
    public int? hiretype { get; set; }
    [NotMapped]
    public string email { get; set; }
    [NotMapped]
    public DateTime? firedate { get; set; }
    [NotMapped]
    public bool? isatt { get; set; }
    [NotMapped]
    public string homeaddress { get; set; }
    [NotMapped]
    public int? emptype { get; set; }
    [NotMapped]
    public string bankcode1 { get; set; }
    [NotMapped]
    public string bankcode2 { get; set; }
    [NotMapped]
    public int? isblacklist { get; set; }
    [NotMapped]
    public int? Iuser1 { get; set; }
    [NotMapped]
    public int? Iuser2 { get; set; }
    [NotMapped]
    public int? Iuser3 { get; set; }
    [NotMapped]
    public int? Iuser4 { get; set; }
    [NotMapped]
    public int? Iuser5 { get; set; }
    [NotMapped]
    public string Cuser1 { get; set; }
    [NotMapped]
    public string Cuser2 { get; set; }
    [NotMapped]
    public string Cuser3 { get; set; }
    [NotMapped]
    public string Cuser4 { get; set; }
    [NotMapped]
    public string Cuser5 { get; set; }
    [NotMapped]
    public DateTime? Duser1 { get; set; }
    [NotMapped]
    public DateTime? Duser2 { get; set; }
    [NotMapped]
    public DateTime? Duser3 { get; set; }
    [NotMapped]
    public DateTime? Duser4 { get; set; }
    [NotMapped]
    public DateTime? Duser5 { get; set; }
    [NotMapped]
    public DateTime? OfflineBeginDate { get; set; }
    [NotMapped]
    public DateTime? OfflineEndDate { get; set; }
    [NotMapped]
    public string carNo { get; set; }
    [NotMapped]
    public string carType { get; set; }
    [NotMapped]
    public string carBrand { get; set; }
    [NotMapped]
    public string carColor { get; set; }


}
