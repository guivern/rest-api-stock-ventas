using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rest_api_sistema_compra_venta.Migrations
{
    public partial class PoblandoRoles2_Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Descripcion", "FechaCreacion" },
                values: new object[] { "Rol que posee todos los permisos del sistema", new DateTime(2019, 1, 31, 14, 18, 50, 334, DateTimeKind.Local).AddTicks(1016) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Activo", "Descripcion", "FechaCreacion", "FechaModificacion", "Nombre" },
                values: new object[,]
                {
                    { 2L, true, "Rol que posee los permisos del módulo almacén", new DateTime(2019, 1, 31, 14, 18, 50, 337, DateTimeKind.Local).AddTicks(1398), null, "Almacenero" },
                    { 3L, true, "Rol que posee los permisos del módulo ventas", new DateTime(2019, 1, 31, 14, 18, 50, 337, DateTimeKind.Local).AddTicks(1455), null, "Vendedor" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Descripcion", "FechaCreacion" },
                values: new object[] { "Rol que posee todos los permisos", new DateTime(2019, 1, 31, 11, 45, 25, 203, DateTimeKind.Local).AddTicks(30) });
        }
    }
}
