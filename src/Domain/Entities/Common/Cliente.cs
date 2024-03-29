﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Permisos;

namespace EvaluacionCore.Domain.Entities.Common;

[Table("CL_Cliente", Schema = "dbo")]
public class Cliente
{
    [Key]
    [Required]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("codigoIntegracion", Order = 1, TypeName = "varchar")]
    public string CodigoIntegracion { get; set; }

    [Column("codigoConvivencia", Order = 2, TypeName = "varchar")]
    public string CodigoConvivencia { get; set; }

    [Column("tipoIdentificacion", Order = 3, TypeName = "varchar")]
    [StringLength(1)]
    public string TipoIdentificacion { get; set; }

    [Column("identificacion", Order = 4, TypeName = "varchar")]
    [StringLength(20)]
    public string Identificacion { get; set; }

    [Column("nombres", Order = 5, TypeName = "varchar")]
    [StringLength(150)]
    public string Nombres { get; set; }

    [Column("apellidos", Order = 6, TypeName = "varchar")]
    [StringLength(150)]
    public string Apellidos { get; set; }

    [Column("alias", Order = 7, TypeName = "varchar")]
    [StringLength(50)]
    public string Alias { get; set; }

    [Column("latitud", Order = 8, TypeName = "float")]
    public float Latitud { get; set; }

    [Column("longitud", Order = 9, TypeName = "float")]
    public float Longitud { get; set; }

    [Column("direccion", Order = 10, TypeName = "varchar")]
    [StringLength(200)]
    public string Direccion { get; set; }

    [Column("celular", Order = 11, TypeName = "varchar")]
    [StringLength(10)]
    public string Celular { get; set; }

    [Column("tipoIdentificacionFamiliar", Order = 12, TypeName = "varchar")]
    [StringLength(1)]
    public string TipoIdentificacionFamiliar { get; set; }

    [Column("indentificacionFamiliar", Order = 13, TypeName = "varchar")]
    [StringLength(20)]
    public string IndentificacionFamiliar { get; set; }

    [Column("correo", Order = 14, TypeName = "varchar")]
    [StringLength(80)]
    public string Correo { get; set; }

    [Column("fechaNacimiento", Order = 15, TypeName = "datetime")]
    public DateTime? FechaNacimiento { get; set; }

    [Column("genero", Order = 16, TypeName = "varchar")]
    [StringLength(1)]
    public string Genero { get; set; }

    [Column("fechaRegistro", Order = 17, TypeName = "datetime2")]
    public DateTime? FechaRegistro { get; set; }

    [Column("servicioActivo", Order = 18, TypeName = "bit")]
    public bool ServicioActivo { get; set; }

    [Column("estado", Order = 19, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    [Column("dispositivoId", Order = 20, TypeName = "varchar")]
    [StringLength(100)]
    public string DispositivoId { get; set; }

    //[NotMapped]
    //[Column("cargoId", Order = 21, TypeName = "uniqueidentifier")]
    //public Guid CargoId { get; set; }
    //public virtual Cargo Cargo { get; set; }

    [Column("clientePadreId", Order = 21, TypeName = "uniqueidentifier")]
    public Guid? ClientePadreId { get; set; }

    [Column("nombreUsuario", Order = 22, TypeName = "varchar")]
    public string NombreUsuario { get; set; }

    [Column("imagenPerfilId", Order = 23, TypeName = "uniqueidentifier")]
    public Guid? ImagenPerfilId { get; set; }
    public virtual Adjunto ImagenPerfil { get; set; }

    [Column("colaboradorReemplazoId", Order = 24, TypeName = "uniqueidentifier")]
    public Guid? ColaboradorReemplazoId { get; set; }

    [Column("facialPersonId", Order = 25, TypeName = "uniqueidentifier")]
    public Guid? FacialPersonId { get; set; }

    public virtual ICollection<TurnoColaborador> TurnoColaboradores { get; set; }
    public virtual ICollection<LocalidadColaborador> LocalidadColaboradores { get; set; }
    //public virtual ICollection<NovedadRecordatorioCab> NovedadRecordatorioCabs { get; set; }

}

