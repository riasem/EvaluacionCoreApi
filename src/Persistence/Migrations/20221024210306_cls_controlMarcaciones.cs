using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class cls_controlMarcaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AS_Canal",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoCanal = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Canal", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_Localidad",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    idEmpresa = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    latitud = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    longitud = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(36)", nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Localidad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_TipoSubTurno",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoSubturno = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_TipoSubTurno", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_TipoTurno",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoTurno = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_TipoTurno", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoIntegracion = table.Column<string>(type: "varchar", nullable: true),
                    codigoConvivencia = table.Column<string>(type: "varchar", nullable: true),
                    tipoIdentificacion = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    identificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    nombres = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    latitud = table.Column<double>(type: "float", nullable: false),
                    longitud = table.Column<double>(type: "float", nullable: false),
                    direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    celular = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    tipoIdentificacionFamiliar = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    indentificacionFamiliar = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    correo = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    genero = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    servicioActivo = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    dispositivoId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    clientePadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    nombreUsuario = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_Turno",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTipoTurno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoTurno = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    codigoIntegracion = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    entrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    salida = table.Column<DateTime>(type: "datetime", nullable: false),
                    margenEntrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    margenSalida = table.Column<string>(type: "varchar(48)", nullable: false),
                    totalHoras = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    TipoSubTurnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Turno", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_Turno_AS_TipoTurno_TipoSubTurnoId",
                        column: x => x.TipoSubTurnoId,
                        principalSchema: "dbo",
                        principalTable: "AS_TipoTurno",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AS_LocalidadCliente",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idLocalidad = table.Column<string>(type: "varchar(36)", nullable: false),
                    idCliente = table.Column<string>(type: "varchar(36)", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    LocalidadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_LocalidadCliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_LocalidadCliente_AS_Localidad_LocalidadId",
                        column: x => x.LocalidadId,
                        principalSchema: "dbo",
                        principalTable: "AS_Localidad",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AS_LocalidadCliente_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AS_SubTurnoCliente",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTipoSubturno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    TipoSubTurnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_SubTurnoCliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_SubTurnoCliente_AS_TipoSubTurno_TipoSubTurnoId",
                        column: x => x.TipoSubTurnoId,
                        principalSchema: "dbo",
                        principalTable: "AS_TipoSubTurno",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AS_SubTurnoCliente_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AS_Subturno",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTipoSubturno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTurno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoSubturno = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    entrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    salida = table.Column<DateTime>(type: "datetime", nullable: false),
                    margenEntrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    margenSalida = table.Column<string>(type: "varchar(48)", nullable: false),
                    totalHoras = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    TipoSubTurnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TurnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Subturno", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_Subturno_AS_TipoSubTurno_TipoSubTurnoId",
                        column: x => x.TipoSubTurnoId,
                        principalSchema: "dbo",
                        principalTable: "AS_TipoSubTurno",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AS_Subturno_AS_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalSchema: "dbo",
                        principalTable: "AS_Turno",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AS_MarcacionCliente",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idLocalidad = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idSubTurno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    marcacionEntrada = table.Column<DateTime>(type: "datetime", nullable: true),
                    marcacionSalida = table.Column<DateTime>(type: "datetime", nullable: true),
                    estadoMarcacionEntrada = table.Column<string>(type: "varchar", nullable: true),
                    estadoMarcacionSalida = table.Column<string>(type: "varchar", nullable: true),
                    totalAtraso = table.Column<DateTime>(type: "datetime", nullable: true),
                    estadoProcesado = table.Column<bool>(type: "bit", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    LocalidadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubTurnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_MarcacionCliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_MarcacionCliente_AS_Localidad_LocalidadId",
                        column: x => x.LocalidadId,
                        principalSchema: "dbo",
                        principalTable: "AS_Localidad",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AS_MarcacionCliente_AS_Subturno_SubTurnoId",
                        column: x => x.SubTurnoId,
                        principalSchema: "dbo",
                        principalTable: "AS_Subturno",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocalidadCliente_ClienteId",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocalidadCliente_LocalidadId",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_MarcacionCliente_LocalidadId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_MarcacionCliente_SubTurnoId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "SubTurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Subturno_TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Subturno",
                column: "TipoSubTurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Subturno_TurnoId",
                schema: "dbo",
                table: "AS_Subturno",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SubTurnoCliente_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SubTurnoCliente_TipoSubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "TipoSubTurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Turno_TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Turno",
                column: "TipoSubTurnoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AS_Canal",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "AS_LocalidadCliente",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "AS_MarcacionCliente",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "AS_SubTurnoCliente",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "AS_Localidad",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "AS_Subturno",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "Clientes");

            //migrationBuilder.DropTable(
            //    name: "AS_TipoSubTurno",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "AS_Turno",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "AS_TipoTurno",
            //    schema: "dbo");
        }
    }
}
