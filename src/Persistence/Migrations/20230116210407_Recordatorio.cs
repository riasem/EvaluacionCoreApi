using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class Recordatorio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "esPrincipal",
                schema: "dbo",
                table: "AS_LocalidadColaborador",
                type: "bit",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.CreateTable(
                name: "AS_NovedadRecordatorioCab",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idJefe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fechaEvaluacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    tipoRecordatorio = table.Column<string>(type: "varchar", nullable: true),
                    diasRecordatorio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_NovedadRecordatorioCab", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_NovedadRecordatorioCab_CL_Cliente_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalSchema: "dbo",
                        principalTable: "CL_Cliente",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AS_Recordatorio",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    inicioRecordatorio = table.Column<DateTime>(type: "datetime", nullable: false),
                    fechaLimite = table.Column<DateTime>(type: "datetime", nullable: false),
                    finRecordatorio = table.Column<DateTime>(type: "datetime", nullable: false),
                    periodoRecordatorio = table.Column<string>(type: "varchar", nullable: true),
                    idPlantillaSms = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Recordatorio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_NovedadRecordatorioDet",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idNovedadRecordatorioCab = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NovedadRecordatorioCabId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    codBiometricoColaborador = table.Column<int>(type: "int", nullable: false),
                    identificacionColaborador = table.Column<string>(type: "varchar", nullable: true),
                    nombreColaborador = table.Column<string>(type: "varchar", nullable: true),
                    fechaNoAsignada = table.Column<DateTime>(type: "datetime", nullable: false),
                    fechaEvaluacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    udn = table.Column<string>(type: "varchar", nullable: true),
                    area = table.Column<string>(type: "varchar", nullable: true),
                    subCentroCosto = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_NovedadRecordatorioDet", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_NovedadRecordatorioDet_AS_NovedadRecordatorioCab_NovedadRecordatorioCabId",
                        column: x => x.NovedadRecordatorioCabId,
                        principalSchema: "dbo",
                        principalTable: "AS_NovedadRecordatorioCab",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AS_NovedadRecordatorioCab_ColaboradorId",
                schema: "dbo",
                table: "AS_NovedadRecordatorioCab",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_NovedadRecordatorioDet_NovedadRecordatorioCabId",
                schema: "dbo",
                table: "AS_NovedadRecordatorioDet",
                column: "NovedadRecordatorioCabId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AS_NovedadRecordatorioDet",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_Recordatorio",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_NovedadRecordatorioCab",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "esPrincipal",
                schema: "dbo",
                table: "AS_LocalidadColaborador");
        }
    }
}
