﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionCore.Domain.Entities.Asistencia;

[Table("AS_TipoTurno", Schema = "dbo")]
public class TipoTurno
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("codigoTurno", Order = 1, TypeName = "varchar")]
    [StringLength(10)] public string CodigoTurno { get; set; } = string.Empty;

    [Column("descripcion", Order = 2, TypeName = "varchar")]
    [StringLength(50)] public string Descripcion { get; set; } = string.Empty;

    [Column("estado", Order = 3, TypeName = "varchar")]
    public string Estado { get; set; } = "A";



    //AUDITORIA
    [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 5, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }


    public virtual ICollection<Turno> Turnos { get; set; }

}

