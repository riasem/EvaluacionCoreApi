using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations.ApplicationDbGRiasem
{
    public partial class monitoriasem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "acc_monito_log_riasem",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    change_operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    change_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    create_operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    delete_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    log_tag = table.Column<int>(type: "int", nullable: true),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    card_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    device_id = table.Column<int>(type: "int", nullable: true),
                    device_sn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    device_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    verified = table.Column<int>(type: "int", nullable: true),
                    state = table.Column<int>(type: "int", nullable: false),
                    event_type = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acc_monito_log_riasem", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "acc_monitor_log",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    change_operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    change_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    create_operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    delete_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    log_tag = table.Column<int>(type: "int", nullable: true),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    card_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    device_id = table.Column<int>(type: "int", nullable: true),
                    device_sn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    device_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    verified = table.Column<int>(type: "int", nullable: true),
                    state = table.Column<int>(type: "int", nullable: true),
                    event_type = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_point_type = table.Column<int>(type: "int", nullable: true),
                    event_point_id = table.Column<int>(type: "int", nullable: true),
                    event_point_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acc_monitor_log", x => x.id);
                });

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
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Localidad", x => x.id);
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
                name: "CHECKINOUT",
                schema: "dbo",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CHECKTIME = table.Column<DateTime>(type: "datetime", nullable: false),
                    CHECKTYPE = table.Column<string>(type: "varchar", nullable: true),
                    VERIFYCODE = table.Column<int>(type: "int", nullable: true),
                    SENSORID = table.Column<string>(type: "varchar", nullable: true),
                    LOGID = table.Column<int>(type: "int", nullable: true),
                    MachineId = table.Column<int>(type: "int", nullable: true),
                    UserExtFmt = table.Column<int>(type: "int", nullable: true),
                    WorkCode = table.Column<int>(type: "int", nullable: true),
                    Memoinfo = table.Column<string>(type: "varchar", nullable: true),
                    sn = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHECKINOUT", x => x.USERID);
                });

            migrationBuilder.CreateTable(
                name: "USERINFO",
                schema: "dbo",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Badgenumber = table.Column<string>(type: "nvarchar", nullable: true),
                    SSN = table.Column<string>(type: "nvarchar", nullable: true),
                    Name = table.Column<string>(type: "nvarchar", nullable: true),
                    create_operator = table.Column<string>(type: "varchar", nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastname = table.Column<string>(type: "nvarchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERINFO", x => x.USERID);
                });

            migrationBuilder.CreateTable(
                name: "WF_Cargo",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "varchar", nullable: true),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    cargoPadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_Cargo", x => x.id);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acc_monito_log_riasem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "acc_monitor_log",
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
                name: "CHECKINOUT",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "USERINFO",
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
        }
    }
}
