using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionCore.Domain.Common
{
    [Table("AS_Periodo", Schema = "dbo")]
    public class Periodo
    {
        [Key]        
        [Column("id", TypeName= "int")]    
        public int Id { get; set; }
        
        [Column("udn", TypeName= "varchar")]    
        public string Udn { get; set; }
        
        [Column("anioPeriodo", TypeName= "varchar")]    
        public string AnioPeriodo { get; set; }
        
        [Column("mesPeriodo", TypeName= "varchar")]    
        public string MesPeriodo { get; set; }
        
        [Column("desPeriodo", TypeName= "varchar")]    
        public string DesPeriodo { get; set; }
        
        [Column("fechaDesdePeriodo", TypeName= "datetime")]    
        public DateTime FechaDesdePeriodo { get; set; }
        
        [Column("fechaHastaPeriodo", TypeName= "datetime")]    
        public DateTime FechaHastaPeriodo { get; set; }
        
        [Column("fechaDesdeCorte", TypeName= "datetime")]    
        public DateTime FechaDesdeCorte { get; set; }
        
        [Column("fechaHastaCorte", TypeName= "datetime")]    
        public DateTime FechaHastaCorte { get; set; }
        
        [Column("idPeriodoPadre", TypeName= "int")]    
        public int IdPeriodoPadre { get; set; }
        
        [Column("estado", TypeName= "varchar")]    
        public string Estado { get; set; }
        
        [Column("fechaCierreControlAsistencia", TypeName= "datetime")]    
        public DateTime FechaCierreControlAsistencia { get; set; }
        
        [Column("horaCierreControlAsistencia", TypeName= "varchar")]    
        public string HoraCierreControlAsistencia { get; set; }

    }
}
