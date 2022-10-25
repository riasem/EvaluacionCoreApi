using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class ajusteTurno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "margenSalida",
                schema: "dbo",
                table: "AS_Turno",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(48)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "margenSalida",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(48)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
