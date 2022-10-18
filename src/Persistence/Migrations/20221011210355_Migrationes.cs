using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Persistence.Migrations
{
    public partial class Migrationes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AD_Adjuntos",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identificacion = table.Column<string>(type: "varchar", nullable: true),
                    idFeature = table.Column<string>(type: "varchar", nullable: true),
                    idSolicitud = table.Column<string>(type: "varchar", nullable: true),
                    idTipoSolicitud = table.Column<string>(type: "varchar", nullable: true),
                    nombreArchivo = table.Column<string>(type: "varchar", nullable: true),
                    rutaAcceso = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AD_Adjuntos", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AD_Adjuntos",
                schema: "dbo");
        }
    }
}
