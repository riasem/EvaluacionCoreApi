using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class CalendarioNuevo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AS_ClaseTurno",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoClaseTurno = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValueSql: "A"),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_ClaseTurno", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_Pais",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Pais", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_SubclaseTurno",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoSubclase = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValueSql: "A"),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_SubclaseTurno", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_TipoTurno",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoTurno = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValueSql: "A"),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_TipoTurno", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OG_GrupoEmpresarial",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    logo = table.Column<byte[]>(type: "varbinary", nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OG_GrupoEmpresarial", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WF_EstadoTarea",
                schema: "dbo",
                columns: table => new
                {
                    idEstado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_EstadoTarea", x => x.idEstado);
                });

            migrationBuilder.CreateTable(
                name: "WF_TipoJustificacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValue: "A"),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_TipoJustificacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WF_TipoPermiso",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValue: "A")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_TipoPermiso", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_CalendarioNacional",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPais = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    fechaFestiva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaConmemorativa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    esRecuperable = table.Column<bool>(type: "bit", nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_CalendarioNacional", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_CalendarioNacional_AS_Pais_idPais",
                        column: x => x.idPais,
                        principalSchema: "dbo",
                        principalTable: "AS_Pais",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_Provincia",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPais = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Provincia", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_Provincia_AS_Pais_idPais",
                        column: x => x.idPais,
                        principalSchema: "dbo",
                        principalTable: "AS_Pais",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_Turno",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTurnoPadre = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idTipoTurno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idClaseTurno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idSubclaseTurno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTipoJornada = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idModalidadJornada = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoTurno = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    codigoIntegracion = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    entrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    salida = table.Column<DateTime>(type: "datetime", nullable: false),
                    totalHoras = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValueSql: "A"),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    margenEntradaPrevio = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    margenSalidaPosterior = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    margenEntradaGracia = table.Column<int>(type: "int", nullable: true),
                    margenSalidaGracia = table.Column<int>(type: "int", nullable: true),
                    codigoEntrada = table.Column<int>(type: "int", nullable: true),
                    codigoSalida = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Turno", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_Turno_AS_ClaseTurno_idClaseTurno",
                        column: x => x.idClaseTurno,
                        principalSchema: "dbo",
                        principalTable: "AS_ClaseTurno",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AS_Turno_AS_SubclaseTurno_idSubclaseTurno",
                        column: x => x.idSubclaseTurno,
                        principalSchema: "dbo",
                        principalTable: "AS_SubclaseTurno",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AS_Turno_AS_TipoTurno_idTipoTurno",
                        column: x => x.idTipoTurno,
                        principalSchema: "dbo",
                        principalTable: "AS_TipoTurno",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OG_Empresa",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    grupoEmpresarialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    ruc = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true),
                    nombreComercial = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    razonSocial = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    logo = table.Column<byte[]>(type: "varbinary", nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OG_Empresa", x => x.id);
                    table.ForeignKey(
                        name: "FK_OG_Empresa_OG_GrupoEmpresarial_grupoEmpresarialId",
                        column: x => x.grupoEmpresarialId,
                        principalSchema: "dbo",
                        principalTable: "OG_GrupoEmpresarial",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_SolicitudJustificacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codOrganizacion = table.Column<int>(type: "int", nullable: true),
                    idTipoJustificacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEstadoSolicitud = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identBeneficiario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    identificacionEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idMarcacion = table.Column<int>(type: "int", nullable: true),
                    idTurno = table.Column<int>(type: "int", nullable: true),
                    marcacionEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    turnoEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    marcacionSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    turnoSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comentarios = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_SolicitudJustificacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_SolicitudJustificacion_WF_EstadoTarea_idEstadoSolicitud",
                        column: x => x.idEstadoSolicitud,
                        principalSchema: "dbo",
                        principalTable: "WF_EstadoTarea",
                        principalColumn: "idEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AS_SolicitudJustificacion_WF_TipoJustificacion_idTipoJustificacion",
                        column: x => x.idTipoJustificacion,
                        principalSchema: "dbo",
                        principalTable: "WF_TipoJustificacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_SolicitudPermiso",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codOrganizacion = table.Column<int>(type: "int", nullable: true),
                    idTipoPermiso = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEstadoSolicitud = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numeroSolicitud = table.Column<int>(name: "numeroSolicitud ", type: "int", nullable: false),
                    idSolicitante = table.Column<int>(name: "idSolicitante ", type: "int", nullable: false),
                    idBeneficiario = table.Column<int>(name: "idBeneficiario ", type: "int", nullable: false),
                    identificacionEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    porHoras = table.Column<bool>(name: "porHoras ", type: "bit", nullable: true),
                    fechaDesde = table.Column<DateTime>(name: "fechaDesde ", type: "datetime2", nullable: false),
                    horaInicio = table.Column<string>(name: "horaInicio ", type: "nvarchar(10)", maxLength: 10, nullable: true),
                    fechaHasta = table.Column<DateTime>(name: "fechaHasta ", type: "datetime2", nullable: false),
                    horaFin = table.Column<string>(name: "horaFin ", type: "nvarchar(10)", maxLength: 10, nullable: true),
                    cantidadHoras = table.Column<DateTime>(name: "cantidadHoras ", type: "datetime2", nullable: true),
                    cantidadDias = table.Column<int>(name: "cantidadDias ", type: "int", nullable: true),
                    observacion = table.Column<string>(name: "observacion ", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    fechaCreacion = table.Column<DateTime>(name: "fechaCreacion ", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_SolicitudPermiso", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_SolicitudPermiso_WF_EstadoTarea_idEstadoSolicitud",
                        column: x => x.idEstadoSolicitud,
                        principalSchema: "dbo",
                        principalTable: "WF_EstadoTarea",
                        principalColumn: "idEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AS_SolicitudPermiso_WF_TipoPermiso_idTipoPermiso",
                        column: x => x.idTipoPermiso,
                        principalSchema: "dbo",
                        principalTable: "WF_TipoPermiso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_SolicitudVacacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codOrganizacion = table.Column<int>(type: "int", nullable: true),
                    idTipoSolicitud = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoPermisoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idEstadoSolicitud = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numeroSolicitud = table.Column<int>(type: "int", nullable: false),
                    idSolicitante = table.Column<int>(type: "int", nullable: false),
                    idBeneficiario = table.Column<int>(type: "int", nullable: false),
                    identificacionEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cantidadDias = table.Column<int>(type: "int", nullable: false),
                    codigoEmpleadoReemplazo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_SolicitudVacacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_SolicitudVacacion_WF_EstadoTarea_idEstadoSolicitud",
                        column: x => x.idEstadoSolicitud,
                        principalSchema: "dbo",
                        principalTable: "WF_EstadoTarea",
                        principalColumn: "idEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AS_SolicitudVacacion_WF_TipoPermiso_TipoPermisoId",
                        column: x => x.TipoPermisoId,
                        principalSchema: "dbo",
                        principalTable: "WF_TipoPermiso",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AS_Canton",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idProvincia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Canton", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_Canton_AS_Provincia_idProvincia",
                        column: x => x.idProvincia,
                        principalSchema: "dbo",
                        principalTable: "AS_Provincia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OG_Area",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    empresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OG_Area", x => x.id);
                    table.ForeignKey(
                        name: "FK_OG_Area_OG_Empresa_empresaId",
                        column: x => x.empresaId,
                        principalSchema: "dbo",
                        principalTable: "OG_Empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_CalendarioLocal",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idCanton = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    fechaFestiva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaConmemorativa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    esRecuperable = table.Column<bool>(type: "bit", nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_CalendarioLocal", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_CalendarioLocal_AS_Canton_idCanton",
                        column: x => x.idCanton,
                        principalSchema: "dbo",
                        principalTable: "AS_Canton",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_Localidad",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    latitud = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    longitud = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    radio = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idCanton = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Localidad", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_Localidad_AS_Canton_idCanton",
                        column: x => x.idCanton,
                        principalSchema: "dbo",
                        principalTable: "AS_Canton",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OG_Departamento",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    areaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OG_Departamento", x => x.id);
                    table.ForeignKey(
                        name: "FK_OG_Departamento_OG_Area_areaId",
                        column: x => x.areaId,
                        principalSchema: "dbo",
                        principalTable: "OG_Area",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WF_Cargo",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    departamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "varchar", nullable: true),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    cargoPadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_Cargo", x => x.id);
                    table.ForeignKey(
                        name: "FK_WF_Cargo_OG_Departamento_departamentoId",
                        column: x => x.departamentoId,
                        principalSchema: "dbo",
                        principalTable: "OG_Departamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CL_Cliente",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoIntegracion = table.Column<string>(type: "varchar", nullable: true),
                    codigoConvivencia = table.Column<string>(type: "varchar", nullable: true),
                    tipoIdentificacion = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    identificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    nombres = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    apellidos = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    latitud = table.Column<double>(type: "float", nullable: false),
                    longitud = table.Column<double>(type: "float", nullable: false),
                    direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    celular = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    tipoIdentificacionFamiliar = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    indentificacionFamiliar = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    correo = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    genero = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    servicioActivo = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    dispositivoId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    cargoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    clientePadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    nombreUsuario = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Cliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_CL_Cliente_WF_Cargo_cargoId",
                        column: x => x.cargoId,
                        principalSchema: "dbo",
                        principalTable: "WF_Cargo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_LocalidadColaborador",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idLocalidad = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idColaborador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_LocalidadColaborador", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_LocalidadColaborador_AS_Localidad_idLocalidad",
                        column: x => x.idLocalidad,
                        principalSchema: "dbo",
                        principalTable: "AS_Localidad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AS_LocalidadColaborador_CL_Cliente_idColaborador",
                        column: x => x.idColaborador,
                        principalSchema: "dbo",
                        principalTable: "CL_Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_TurnoColaborador",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTurno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idColaborador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaAsginacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_TurnoColaborador", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_TurnoColaborador_AS_Turno_idTurno",
                        column: x => x.idTurno,
                        principalSchema: "dbo",
                        principalTable: "AS_Turno",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AS_TurnoColaborador_CL_Cliente_idColaborador",
                        column: x => x.idColaborador,
                        principalSchema: "dbo",
                        principalTable: "CL_Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_MarcacionColaborador",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTurnoCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idLocalidadColaborador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    marcacionEntrada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    marcacionSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    estadoMarcacionEntrada = table.Column<string>(type: "varchar", nullable: true),
                    estadoMarcacionSalida = table.Column<string>(type: "varchar", nullable: true),
                    totalAtraso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    estadoProcesado = table.Column<bool>(type: "bit", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_MarcacionColaborador", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_MarcacionColaborador_AS_LocalidadColaborador_idLocalidadColaborador",
                        column: x => x.idLocalidadColaborador,
                        principalSchema: "dbo",
                        principalTable: "AS_LocalidadColaborador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AS_MarcacionColaborador_AS_TurnoColaborador_idTurnoCliente",
                        column: x => x.idTurnoCliente,
                        principalSchema: "dbo",
                        principalTable: "AS_TurnoColaborador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AS_CalendarioLocal_idCanton",
                schema: "dbo",
                table: "AS_CalendarioLocal",
                column: "idCanton");

            migrationBuilder.CreateIndex(
                name: "IX_AS_CalendarioNacional_idPais",
                schema: "dbo",
                table: "AS_CalendarioNacional",
                column: "idPais");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Canton_idProvincia",
                schema: "dbo",
                table: "AS_Canton",
                column: "idProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Localidad_idCanton",
                schema: "dbo",
                table: "AS_Localidad",
                column: "idCanton");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocalidadColaborador_idColaborador",
                schema: "dbo",
                table: "AS_LocalidadColaborador",
                column: "idColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocalidadColaborador_idLocalidad",
                schema: "dbo",
                table: "AS_LocalidadColaborador",
                column: "idLocalidad");

            migrationBuilder.CreateIndex(
                name: "IX_AS_MarcacionColaborador_idLocalidadColaborador",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                column: "idLocalidadColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_AS_MarcacionColaborador_idTurnoCliente",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                column: "idTurnoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Provincia_idPais",
                schema: "dbo",
                table: "AS_Provincia",
                column: "idPais");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SolicitudJustificacion_idEstadoSolicitud",
                schema: "dbo",
                table: "AS_SolicitudJustificacion",
                column: "idEstadoSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SolicitudJustificacion_idTipoJustificacion",
                schema: "dbo",
                table: "AS_SolicitudJustificacion",
                column: "idTipoJustificacion");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SolicitudPermiso_idEstadoSolicitud",
                schema: "dbo",
                table: "AS_SolicitudPermiso",
                column: "idEstadoSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SolicitudPermiso_idTipoPermiso",
                schema: "dbo",
                table: "AS_SolicitudPermiso",
                column: "idTipoPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SolicitudVacacion_idEstadoSolicitud",
                schema: "dbo",
                table: "AS_SolicitudVacacion",
                column: "idEstadoSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SolicitudVacacion_TipoPermisoId",
                schema: "dbo",
                table: "AS_SolicitudVacacion",
                column: "TipoPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Turno_idClaseTurno",
                schema: "dbo",
                table: "AS_Turno",
                column: "idClaseTurno");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Turno_idSubclaseTurno",
                schema: "dbo",
                table: "AS_Turno",
                column: "idSubclaseTurno");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Turno_idTipoTurno",
                schema: "dbo",
                table: "AS_Turno",
                column: "idTipoTurno");

            migrationBuilder.CreateIndex(
                name: "IX_AS_TurnoColaborador_idColaborador",
                schema: "dbo",
                table: "AS_TurnoColaborador",
                column: "idColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_AS_TurnoColaborador_idTurno",
                schema: "dbo",
                table: "AS_TurnoColaborador",
                column: "idTurno");

            migrationBuilder.CreateIndex(
                name: "IX_CL_Cliente_cargoId",
                schema: "dbo",
                table: "CL_Cliente",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_OG_Area_empresaId",
                schema: "dbo",
                table: "OG_Area",
                column: "empresaId");

            migrationBuilder.CreateIndex(
                name: "IX_OG_Departamento_areaId",
                schema: "dbo",
                table: "OG_Departamento",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_OG_Empresa_grupoEmpresarialId",
                schema: "dbo",
                table: "OG_Empresa",
                column: "grupoEmpresarialId");

            migrationBuilder.CreateIndex(
                name: "IX_WF_Cargo_departamentoId",
                schema: "dbo",
                table: "WF_Cargo",
                column: "departamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AS_CalendarioLocal",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_CalendarioNacional",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_MarcacionColaborador",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_SolicitudJustificacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_SolicitudPermiso",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_SolicitudVacacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_LocalidadColaborador",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_TurnoColaborador",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WF_TipoJustificacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WF_EstadoTarea",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WF_TipoPermiso",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_Localidad",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_Turno",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CL_Cliente",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_Canton",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_ClaseTurno",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_SubclaseTurno",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_TipoTurno",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WF_Cargo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_Provincia",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OG_Departamento",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_Pais",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OG_Area",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OG_Empresa",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OG_GrupoEmpresarial",
                schema: "dbo");
        }
    }
}
