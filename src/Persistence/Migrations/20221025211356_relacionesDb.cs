using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class relacionesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AS_MarcacionCliente_AS_Subturno_SubTurnoId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_Subturno_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_SubTurnoCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_SubTurnoCliente_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_SubTurnoCliente_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.RenameColumn(
                name: "subturnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                newName: "SubTurnoId");

            migrationBuilder.RenameColumn(
                name: "clienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "idSubTurno",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "idCliente");

            migrationBuilder.RenameColumn(
                name: "SubTurnoId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_AS_MarcacionCliente_SubTurnoId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "IX_AS_MarcacionCliente_ClienteId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<Guid>(
                name: "idCliente",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<Guid>(
                name: "idSubturno",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.CreateIndex(
                name: "IX_AS_SubTurnoCliente_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SubTurnoCliente_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "SubTurnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_MarcacionCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_Subturno_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "SubTurnoId",
                principalSchema: "dbo",
                principalTable: "AS_Subturno",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_SubTurnoCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AS_MarcacionCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_Subturno_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_SubTurnoCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_SubTurnoCliente_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_SubTurnoCliente_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "idCliente",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "idSubturno",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.RenameColumn(
                name: "SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                newName: "subturnoId");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                newName: "clienteId");

            migrationBuilder.RenameColumn(
                name: "idCliente",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "idSubTurno");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "SubTurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_AS_MarcacionCliente_ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "IX_AS_MarcacionCliente_SubTurnoId");

            migrationBuilder.AlterColumn<Guid>(
                name: "subturnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "clienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AS_SubTurnoCliente_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SubTurnoCliente_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "SubTurnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_MarcacionCliente_AS_Subturno_SubTurnoId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "SubTurnoId",
                principalSchema: "dbo",
                principalTable: "AS_Subturno",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_Subturno_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "SubTurnoId",
                principalSchema: "dbo",
                principalTable: "AS_Subturno",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_SubTurnoCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "id");
        }
    }
}
