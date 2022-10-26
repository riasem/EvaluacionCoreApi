using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class TablaLocacionTurnoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "idEmpresa",
                schema: "dbo",
                table: "AS_Locacion",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "AS_LocacionTurnoCliente",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubTurnoClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_LocacionTurnoCliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_LocacionTurnoCliente_AS_Locacion_LocacionId",
                        column: x => x.LocacionId,
                        principalSchema: "dbo",
                        principalTable: "AS_Locacion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AS_LocacionTurnoCliente_AS_SubTurnoCliente_SubTurnoClienteId",
                        column: x => x.SubTurnoClienteId,
                        principalSchema: "dbo",
                        principalTable: "AS_SubTurnoCliente",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AS_SubTurnoCliente_LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "LocacionTurnoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Subturno_LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno",
                column: "LocacionTurnoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocacionTurnoCliente_LocacionId",
                schema: "dbo",
                table: "AS_LocacionTurnoCliente",
                column: "LocacionId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocacionTurnoCliente_SubTurnoClienteId",
                schema: "dbo",
                table: "AS_LocacionTurnoCliente",
                column: "SubTurnoClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_Subturno_AS_LocacionTurnoCliente_LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno",
                column: "LocacionTurnoClienteId",
                principalSchema: "dbo",
                principalTable: "AS_LocacionTurnoCliente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_LocacionTurnoCliente_LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "LocacionTurnoClienteId",
                principalSchema: "dbo",
                principalTable: "AS_LocacionTurnoCliente",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AS_Subturno_AS_LocacionTurnoCliente_LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_LocacionTurnoCliente_LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropTable(
                name: "AS_LocacionTurnoCliente",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_AS_SubTurnoCliente_LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_Subturno_LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.DropColumn(
                name: "LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "LocacionTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.AlterColumn<string>(
                name: "idEmpresa",
                schema: "dbo",
                table: "AS_Locacion",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
