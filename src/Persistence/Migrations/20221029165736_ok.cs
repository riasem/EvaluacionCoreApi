using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class ok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AS_LocalidadCliente_AS_Localidad_LocalidadId",
                schema: "dbo",
                table: "AS_LocalidadCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_LocalidadCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_LocalidadCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_LocalidadSubturnoCliente_AS_LocalidadCliente_LocalidadClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_LocalidadSubturnoCliente_AS_SubTurnoCliente_SubTurnoClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_MarcacionCliente_AS_LocalidadSubturnoCliente_LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_Subturno_AS_TipoSubTurno_TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Subturno");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AS_Subturno_AS_Turno_TurnoId",
            //    schema: "dbo",
            //    table: "AS_Subturno");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_SubTurnoCliente_AS_Subturno_SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_SubTurnoCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AS_Turno_AS_TipoTurno_TipoSubTurnoId",
            //    schema: "dbo",
            //    table: "AS_Turno");

            //migrationBuilder.DropIndex(
            //    name: "IX_AS_Turno_TipoSubTurnoId",
            //    schema: "dbo",
            //    table: "AS_Turno");

            //migrationBuilder.DropIndex(
            //    name: "IX_AS_SubTurnoCliente_ClienteId",
            //    schema: "dbo",
            //    table: "AS_SubTurnoCliente");

            //migrationBuilder.DropIndex(
            //    name: "IX_AS_SubTurnoCliente_SubTurnoId",
            //    schema: "dbo",
            //    table: "AS_SubTurnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_Subturno_TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.DropIndex(
                name: "IX_AS_Subturno_TurnoId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.DropIndex(
                name: "IX_AS_MarcacionCliente_LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_LocalidadSubturnoCliente_LocalidadClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_LocalidadSubturnoCliente_SubTurnoClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_LocalidadCliente_ClienteId",
                schema: "dbo",
                table: "AS_LocalidadCliente");

            migrationBuilder.DropIndex(
                name: "IX_AS_LocalidadCliente_LocalidadId",
                schema: "dbo",
                table: "AS_LocalidadCliente");

            migrationBuilder.DropColumn(
                name: "TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "SubTurnoId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.DropColumn(
                name: "TurnoId",
                schema: "dbo",
                table: "AS_Subturno");

            //migrationBuilder.DropColumn(
            //    name: "LocalidadSubturnoClienteId",
            //    schema: "dbo",
            //    table: "AS_MarcacionCliente");

            migrationBuilder.DropColumn(
                name: "LocalidadClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente");

            migrationBuilder.DropColumn(
                name: "SubTurnoClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_LocalidadCliente");

            migrationBuilder.DropColumn(
                name: "LocalidadId",
                schema: "dbo",
                table: "AS_LocalidadCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Turno",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.AddColumn<Guid>(
                name: "TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Subturno",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TurnoId",
                schema: "dbo",
                table: "AS_Subturno",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalidadClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubTurnoClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalidadId",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AS_Turno_TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Turno",
                column: "TipoSubTurnoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AS_SubTurnoCliente_ClienteId",
            //    schema: "dbo",
            //    table: "AS_SubTurnoCliente",
            //    column: "ClienteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AS_SubTurnoCliente_SubTurnoId",
            //    schema: "dbo",
            //    table: "AS_SubTurnoCliente",
            //    column: "SubTurnoId");

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
                name: "IX_AS_MarcacionCliente_LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "LocalidadSubturnoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocalidadSubturnoCliente_LocalidadClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente",
                column: "LocalidadClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_LocalidadSubturnoCliente_SubTurnoClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente",
                column: "SubTurnoClienteId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AS_LocalidadCliente_AS_Localidad_LocalidadId",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                column: "LocalidadId",
                principalSchema: "dbo",
                principalTable: "AS_Localidad",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_LocalidadCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_LocalidadSubturnoCliente_AS_LocalidadCliente_LocalidadClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente",
                column: "LocalidadClienteId",
                principalSchema: "dbo",
                principalTable: "AS_LocalidadCliente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_LocalidadSubturnoCliente_AS_SubTurnoCliente_SubTurnoClienteId",
                schema: "dbo",
                table: "AS_LocalidadSubturnoCliente",
                column: "SubTurnoClienteId",
                principalSchema: "dbo",
                principalTable: "AS_SubTurnoCliente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_MarcacionCliente_AS_LocalidadSubturnoCliente_LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "LocalidadSubturnoClienteId",
                principalSchema: "dbo",
                principalTable: "AS_LocalidadSubturnoCliente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_Subturno_AS_TipoSubTurno_TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Subturno",
                column: "TipoSubTurnoId",
                principalSchema: "dbo",
                principalTable: "AS_TipoSubTurno",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_Subturno_AS_Turno_TurnoId",
                schema: "dbo",
                table: "AS_Subturno",
                column: "TurnoId",
                principalSchema: "dbo",
                principalTable: "AS_Turno",
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

            migrationBuilder.AddForeignKey(
                name: "FK_AS_Turno_AS_TipoTurno_TipoSubTurnoId",
                schema: "dbo",
                table: "AS_Turno",
                column: "TipoSubTurnoId",
                principalSchema: "dbo",
                principalTable: "AS_TipoTurno",
                principalColumn: "id");
        }
    }
}
