using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace rest_api_sistema_compra_venta.Migrations
{
    public partial class Venta_Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdCliente = table.Column<long>(nullable: false),
                    IdUsuario = table.Column<long>(nullable: false),
                    TipoComprobante = table.Column<string>(nullable: false),
                    NroComprobante = table.Column<string>(nullable: false),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    Impuesto = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Estado = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesVentas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdVenta = table.Column<long>(nullable: false),
                    IdArticulo = table.Column<long>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio = table.Column<decimal>(nullable: false),
                    Descuento = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesVentas_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesVentas_Ventas_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_IdArticulo",
                table: "DetallesVentas",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_IdVenta",
                table: "DetallesVentas",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdCliente",
                table: "Ventas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdUsuario",
                table: "Ventas",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesVentas");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 5, 10, 59, 22, 354, DateTimeKind.Local).AddTicks(7955));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 5, 10, 59, 22, 356, DateTimeKind.Local).AddTicks(6658));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 5, 10, 59, 22, 356, DateTimeKind.Local).AddTicks(6766));
        }
    }
}
