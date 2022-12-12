using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class calendario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "margenEntrada",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "margenEntradaPosterior",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "margenSalida",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "margenSalidaPrevio",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.RenameColumn(
                name: "codigoTipoTurno",
                schema: "dbo",
                table: "AS_TipoTurno",
                newName: "codigoTurno");

            migrationBuilder.AddColumn<Guid>(
                name: "cargoId",
                schema: "dbo",
                table: "CL_Cliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 22);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 20);

            migrationBuilder.AlterColumn<string>(
                name: "totalHoras",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<int>(
                name: "margenSalidaPosterior",
                schema: "dbo",
                table: "AS_Turno",
                type: "int",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldMaxLength: 4)
                .Annotation("Relational:ColumnOrder", 19)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<int>(
                name: "margenEntradaPrevio",
                schema: "dbo",
                table: "AS_Turno",
                type: "int",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldMaxLength: 4)
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_Turno",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 23);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_Turno",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()")
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValueSql: "A",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldDefaultValueSql: "A")
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AddColumn<int>(
                name: "codigoEntrada",
                schema: "dbo",
                table: "AS_Turno",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 22);

            migrationBuilder.AddColumn<int>(
                name: "codigoSalida",
                schema: "dbo",
                table: "AS_Turno",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 23);

            migrationBuilder.AddColumn<int>(
                name: "margenEntradaGracia",
                schema: "dbo",
                table: "AS_Turno",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 20);

            migrationBuilder.AddColumn<int>(
                name: "margenSalidaGracia",
                schema: "dbo",
                table: "AS_Turno",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<double>(
                name: "radio",
                schema: "dbo",
                table: "AS_Localidad",
                type: "float",
                maxLength: 8,
                nullable: false,
                defaultValue: 0.0)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.CreateTable(
                name: "AS_Calendario",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    diaDeLaSemana = table.Column<string>(type: "varchar", nullable: true),
                    esLaborable = table.Column<bool>(type: "bit", nullable: false),
                    esRecuperable = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Calendario", x => x.id);
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

            migrationBuilder.CreateIndex(
                name: "IX_CL_Cliente_cargoId",
                schema: "dbo",
                table: "CL_Cliente",
                column: "cargoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CL_Cliente_WF_Cargo_cargoId",
                schema: "dbo",
                table: "CL_Cliente",
                column: "cargoId",
                principalSchema: "dbo",
                principalTable: "WF_Cargo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CL_Cliente_WF_Cargo_cargoId",
                schema: "dbo",
                table: "CL_Cliente");

            migrationBuilder.DropTable(
                name: "AS_Calendario",
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
                name: "WF_Cargo",
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

            migrationBuilder.DropIndex(
                name: "IX_CL_Cliente_cargoId",
                schema: "dbo",
                table: "CL_Cliente");

            migrationBuilder.DropColumn(
                name: "cargoId",
                schema: "dbo",
                table: "CL_Cliente");

            migrationBuilder.DropColumn(
                name: "codigoEntrada",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "codigoSalida",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "margenEntradaGracia",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "margenSalidaGracia",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "radio",
                schema: "dbo",
                table: "AS_Localidad");

            migrationBuilder.RenameColumn(
                name: "codigoTurno",
                schema: "dbo",
                table: "AS_TipoTurno",
                newName: "codigoTipoTurno");

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 22)
                .OldAnnotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 20)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<string>(
                name: "totalHoras",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<string>(
                name: "margenSalidaPosterior",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 4)
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<string>(
                name: "margenEntradaPrevio",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 4)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_Turno",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 23)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_Turno",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()")
                .Annotation("Relational:ColumnOrder", 21)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValueSql: "A",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldDefaultValueSql: "A")
                .Annotation("Relational:ColumnOrder", 19)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AddColumn<DateTime>(
                name: "margenEntrada",
                schema: "dbo",
                table: "AS_Turno",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AddColumn<string>(
                name: "margenEntradaPosterior",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 15);

            migrationBuilder.AddColumn<DateTime>(
                name: "margenSalida",
                schema: "dbo",
                table: "AS_Turno",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 13);

            migrationBuilder.AddColumn<string>(
                name: "margenSalidaPrevio",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);
        }
    }
}
