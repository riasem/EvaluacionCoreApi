using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Domain.Common
{
    [Table("LG_TransaccionSms", Schema = "dbo")]
    public class TransaccionSms
    {
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }
        
        [Column("idTransaccion", Order = 1, TypeName = "varchar")]
        public string IdTransaccion { get; set; }

        [Column("idPlantillaMensaje", Order = 2, TypeName = "varchar")]
        public string IdPlantillaMensaje { get; set; }

        [Column("identificacionEmpleado", Order = 3, TypeName = "varchar")]
        public string IdentificacionEmpleado { get; set; }

        [Column("celularEmpleado", Order = 4, TypeName = "varchar")]
        public string CelularEmpleado { get; set; }

        [Column("codError", Order = 5, TypeName = "int")]
        public int CodError { get; set; }

        [Column("desError", Order = 6, TypeName = "varchar")]
        public string DesError { get; set; }

        [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; }

        [Column("fechaCreacion", Order = 8, TypeName = "datetime2")]
        public DateTime? FechaCreacion { get; set; }

        [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; }

        [Column("fechaModificacion", Order = 10, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }
    }
}
