using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class calendarioCiudad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Calendario",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Calendario",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_Calendario",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_Calendario",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                schema: "dbo",
                table: "AS_Calendario",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<bool>(
                name: "esRecuperable",
                schema: "dbo",
                table: "AS_Calendario",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<bool>(
                name: "esLaborable",
                schema: "dbo",
                table: "AS_Calendario",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "diaDeLaSemana",
                schema: "dbo",
                table: "AS_Calendario",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<Guid>(
                name: "CiudadPaisId",
                schema: "dbo",
                table: "AS_Calendario",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "idCiudadPais",
                schema: "dbo",
                table: "AS_Calendario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.CreateTable(
                name: "AS_CiudadPais",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ciudad = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_CiudadPais", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AS_Calendario_CiudadPaisId",
                schema: "dbo",
                table: "AS_Calendario",
                column: "CiudadPaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_Calendario_AS_CiudadPais_CiudadPaisId",
                schema: "dbo",
                table: "AS_Calendario",
                column: "CiudadPaisId",
                principalSchema: "dbo",
                principalTable: "AS_CiudadPais",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AS_Calendario_AS_CiudadPais_CiudadPaisId",
                schema: "dbo",
                table: "AS_Calendario");

            migrationBuilder.DropTable(
                name: "AS_CiudadPais",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_AS_Calendario_CiudadPaisId",
                schema: "dbo",
                table: "AS_Calendario");

            migrationBuilder.DropColumn(
                name: "CiudadPaisId",
                schema: "dbo",
                table: "AS_Calendario");

            migrationBuilder.DropColumn(
                name: "idCiudadPais",
                schema: "dbo",
                table: "AS_Calendario");

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Calendario",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Calendario",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_Calendario",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_Calendario",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                schema: "dbo",
                table: "AS_Calendario",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "esRecuperable",
                schema: "dbo",
                table: "AS_Calendario",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<bool>(
                name: "esLaborable",
                schema: "dbo",
                table: "AS_Calendario",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "diaDeLaSemana",
                schema: "dbo",
                table: "AS_Calendario",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 3);
        }
    }
}
