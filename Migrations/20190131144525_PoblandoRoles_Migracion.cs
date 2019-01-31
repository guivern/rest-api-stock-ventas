using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rest_api_sistema_compra_venta.Migrations
{
    public partial class PoblandoRoles_Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Activo", "Descripcion", "FechaCreacion", "FechaModificacion", "Nombre" },
                values: new object[] { 1L, true, "Rol que posee todos los permisos", new DateTime(2019, 1, 31, 11, 45, 25, 203, DateTimeKind.Local).AddTicks(30), null, "Administrador" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
