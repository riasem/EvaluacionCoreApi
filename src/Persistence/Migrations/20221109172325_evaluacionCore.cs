using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class evaluacionCore : Migration
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
                name: "AS_Localidad",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    latitud = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    longitud = table.Column<double>(type: "float", maxLength: 8, nullable: false),
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
                    clientePadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    nombreUsuario = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Cliente", x => x.id);
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
                    margenEntrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    margenSalida = table.Column<DateTime>(type: "datetime", nullable: false),
                    totalHoras = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValueSql: "A"),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    margenEntradaPrevio = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    margenEntradaPosterior = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    marcacionSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    margenSalidaPrevio = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    margenSalidaPosterior = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AS_MarcacionColaborador",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_LocalidadColaborador",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_TurnoColaborador",
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
        }
    }
}
