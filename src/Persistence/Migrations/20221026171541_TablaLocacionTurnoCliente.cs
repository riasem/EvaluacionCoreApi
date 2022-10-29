using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class TablaLocalidadTurnoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "idEmpresa",
                schema: "dbo",
                table: "AS_Localidad",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "AS_LocalidadTurnoCliente",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocalidadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubTurnoClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_LocalidadTurnoCliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_LocalidadTurnoCliente_AS_Localidad_LocalidadId",
                        column: x => x.LocalidadId,
                        principalSchema: "dbo",
                        principalTable: "AS_Localidad",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AS_LocalidadTurnoCliente_AS_SubTurnoCliente_SubTurnoClienteId",
                        column: x => x.SubTurnoClienteId,
                        principalSchema: "dbo",
                        principalTable: "AS_SubTurnoCliente",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AS_SubTurnoCliente_LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "LocalidadTurnoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Subturno_LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno",
                column: "LocalidadTurnoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocalidadTurnoCliente_LocalidadId",
                schema: "dbo",
                table: "AS_LocalidadTurnoCliente",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocalidadTurnoCliente_SubTurnoClienteId",
                schema: "dbo",
                table: "AS_LocalidadTurnoCliente",
                column: "SubTurnoClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_Subturno_AS_LocalidadTurnoCliente_LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno",
                column: "LocalidadTurnoClienteId",
                principalSchema: "dbo",
                principalTable: "AS_LocalidadTurnoCliente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_LocalidadTurnoCliente_LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "LocalidadTurnoClienteId",
                principalSchema: "dbo",
                principalTable: "AS_LocalidadTurnoCliente",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AS_Subturno_AS_LocalidadTurnoCliente_LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_LocalidadTurnoCliente_LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropTable(
                name: "AS_LocalidadTurnoCliente",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_AS_SubTurnoCliente_LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_Subturno_LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.DropColumn(
                name: "LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.AlterColumn<string>(
                name: "idEmpresa",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
