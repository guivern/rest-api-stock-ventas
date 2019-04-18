using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rest_api_sistema_compra_venta.Migrations
{
    public partial class UsuarioDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2019, 4, 18, 16, 18, 37, 117, DateTimeKind.Local).AddTicks(6877));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2019, 4, 18, 16, 18, 37, 125, DateTimeKind.Local).AddTicks(2894));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2019, 4, 18, 16, 18, 37, 125, DateTimeKind.Local).AddTicks(3118));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Activo", "Apellido", "Descripcion", "Direccion", "Email", "FechaCreacion", "FechaModificacion", "FechaNacimiento", "HashPassword", "IdRol", "Nombre", "NumeroDocumento", "Telefono", "TipoDocumento", "Username" },
                values: new object[] { 1L, true, "admin", null, null, null, new DateTime(2019, 4, 18, 16, 18, 37, 126, DateTimeKind.Local).AddTicks(1455), null, null, new byte[] { 54, 39, 144, 154, 41, 195, 19, 129, 160, 113, 236, 39, 247, 201, 202, 151, 114, 97, 130, 174, 210, 154, 125, 221, 46, 84, 53, 51, 34, 207, 179, 10, 187, 158, 58, 109, 242, 172, 44, 32, 254, 35, 67, 99, 17, 214, 120, 86, 77, 12, 141, 48, 89, 48, 87, 95, 96, 226, 211, 208, 72, 24, 77, 121 }, 1L, "admin", null, null, null, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 12, 19, 22, 1, 280, DateTimeKind.Local).AddTicks(4497));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 12, 19, 22, 1, 282, DateTimeKind.Local).AddTicks(1288));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 12, 19, 22, 1, 282, DateTimeKind.Local).AddTicks(1350));
        }
    }
}
