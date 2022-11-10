using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class ajuste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "margenEntradaPosterior",
                schema: "dbo",
                table: "AS_MarcacionColaborador");

            migrationBuilder.DropColumn(
                name: "margenEntradaPrevio",
                schema: "dbo",
                table: "AS_MarcacionColaborador");

            migrationBuilder.DropColumn(
                name: "margenSalidaPosterior",
                schema: "dbo",
                table: "AS_MarcacionColaborador");

            migrationBuilder.DropColumn(
                name: "margenSalidaPrevio",
                schema: "dbo",
                table: "AS_MarcacionColaborador");

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
                .OldAnnotation("Relational:ColumnOrder", 18);

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
                .OldAnnotation("Relational:ColumnOrder", 16);

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
                .Annotation("Relational:ColumnOrder", 23)
                .OldAnnotation("Relational:ColumnOrder", 19);

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
                .OldAnnotation("Relational:ColumnOrder", 17);

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
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AddColumn<string>(
                name: "margenEntradaPosterior",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 15);

            migrationBuilder.AddColumn<string>(
                name: "margenEntradaPrevio",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 14);

            migrationBuilder.AddColumn<string>(
                name: "margenSalidaPosterior",
                schema: "dbo",
                table: "AS_Turno",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 17);

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
                table: "AS_MarcacionColaborador",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<DateTime>(
                name: "totalAtraso",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "marcacionSalida",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<bool>(
                name: "estadoProcesado",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<string>(
                name: "estadoMarcacionSalida",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<string>(
                name: "estadoMarcacionEntrada",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 9);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "margenEntradaPosterior",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "margenEntradaPrevio",
                schema: "dbo",
                table: "AS_Turno");

            migrationBuilder.DropColumn(
                name: "margenSalidaPosterior",
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
                .Annotation("Relational:ColumnOrder", 18)
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
                .Annotation("Relational:ColumnOrder", 16)
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
                .Annotation("Relational:ColumnOrder", 19)
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
                .Annotation("Relational:ColumnOrder", 17)
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
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "totalAtraso",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "marcacionSalida",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()")
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<bool>(
                name: "estadoProcesado",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "estadoMarcacionSalida",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "estadoMarcacionEntrada",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<string>(
                name: "margenEntradaPosterior",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<string>(
                name: "margenEntradaPrevio",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<string>(
                name: "margenSalidaPosterior",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<string>(
                name: "margenSalidaPrevio",
                schema: "dbo",
                table: "AS_MarcacionColaborador",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 7);
        }
    }
}
