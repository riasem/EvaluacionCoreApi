using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class regularizacionEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AS_MarcacionCliente_AS_Localidad_LocalidadId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_AS_MarcacionCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

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

            migrationBuilder.DropIndex(
                name: "IX_AS_MarcacionCliente_ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.DropColumn(
                name: "LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente");

            migrationBuilder.DropColumn(
                name: "LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.DropColumn(
                name: "idLocalidad",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.RenameColumn(
                name: "LocalidadId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "LocalidadSubturnoClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_AS_MarcacionCliente_LocalidadId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "IX_AS_MarcacionCliente_LocalidadSubturnoClienteId");

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Subturno",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Subturno",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<string>(
                name: "totalHoras",
                schema: "dbo",
                table: "AS_Subturno",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_Subturno",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_Subturno",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AddColumn<bool>(
                name: "esSubturnoPrincipal",
                schema: "dbo",
                table: "AS_Subturno",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "totalAtraso",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "marcacionSalida",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "marcacionEntrada",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "idCliente",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<bool>(
                name: "estadoProcesado",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "estadoMarcacionSalida",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "estadoMarcacionEntrada",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<Guid>(
                name: "idLocalidad",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "idCliente",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "idEmpresa",
                schema: "dbo",
                table: "AS_Localidad",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldMaxLength: 5,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Canal",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Canal",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AS_LocalidadSubturnoCliente",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idLocalidad = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idSubturnoCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocalidadClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubTurnoClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_LocalidadSubturnoCliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_LocalidadSubturnoCliente_AS_LocalidadCliente_LocalidadClienteId",
                        column: x => x.LocalidadClienteId,
                        principalSchema: "dbo",
                        principalTable: "AS_LocalidadCliente",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AS_LocalidadSubturnoCliente_AS_SubTurnoCliente_SubTurnoClienteId",
                        column: x => x.SubTurnoClienteId,
                        principalSchema: "dbo",
                        principalTable: "AS_SubTurnoCliente",
                        principalColumn: "id");
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_AS_MarcacionCliente_AS_LocalidadSubturnoCliente_LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "LocalidadSubturnoClienteId",
                principalSchema: "dbo",
                principalTable: "AS_LocalidadSubturnoCliente",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AS_MarcacionCliente_AS_LocalidadSubturnoCliente_LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente");

            migrationBuilder.DropTable(
                name: "AS_LocalidadSubturnoCliente",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "esSubturnoPrincipal",
                schema: "dbo",
                table: "AS_Subturno");

            migrationBuilder.RenameColumn(
                name: "LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "LocalidadId");

            migrationBuilder.RenameIndex(
                name: "IX_AS_MarcacionCliente_LocalidadSubturnoClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                newName: "IX_AS_MarcacionCliente_LocalidadId");

            migrationBuilder.AddColumn<Guid>(
                name: "LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_SubTurnoCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Subturno",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Subturno",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<string>(
                name: "totalHoras",
                schema: "dbo",
                table: "AS_Subturno",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_Subturno",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_Subturno",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalidadTurnoClienteId",
                schema: "dbo",
                table: "AS_Subturno",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<DateTime>(
                name: "totalAtraso",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "marcacionSalida",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "marcacionEntrada",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "idCliente",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaModificacion",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<bool>(
                name: "estadoProcesado",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<string>(
                name: "estadoMarcacionSalida",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "estadoMarcacionEntrada",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "idLocalidad",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "idLocalidad",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "idCliente",
                schema: "dbo",
                table: "AS_LocalidadCliente",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "idEmpresa",
                schema: "dbo",
                table: "AS_Localidad",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                schema: "dbo",
                table: "AS_Localidad",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioModificacion",
                schema: "dbo",
                table: "AS_Canal",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "usuarioCreacion",
                schema: "dbo",
                table: "AS_Canal",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

           

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
                name: "IX_AS_MarcacionCliente_ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "ClienteId");

           

            migrationBuilder.AddForeignKey(
                name: "FK_AS_MarcacionCliente_AS_Localidad_LocalidadId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "LocalidadId",
                principalSchema: "dbo",
                principalTable: "AS_Localidad",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AS_MarcacionCliente_Clientes_ClienteId",
                schema: "dbo",
                table: "AS_MarcacionCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "id");

            
        }
    }
}
