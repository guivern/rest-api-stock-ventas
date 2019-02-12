using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace rest_api_sistema_compra_venta.Migrations
{
    public partial class Ingreso_Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingresos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdProveedor = table.Column<long>(nullable: false),
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
                    table.PrimaryKey("PK_Ingresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingresos_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingresos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesIngresos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdIngreso = table.Column<long>(nullable: false),
                    IdArticulo = table.Column<long>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesIngresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesIngresos_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesIngresos_Ingresos_IdIngreso",
                        column: x => x.IdIngreso,
                        principalTable: "Ingresos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DetallesIngresos_IdArticulo",
                table: "DetallesIngresos",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesIngresos_IdIngreso",
                table: "DetallesIngresos",
                column: "IdIngreso");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_IdProveedor",
                table: "Ingresos",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_IdUsuario",
                table: "Ingresos",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesIngresos");

            migrationBuilder.DropTable(
                name: "Ingresos");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 2, 15, 3, 3, 331, DateTimeKind.Local).AddTicks(5305));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 2, 15, 3, 3, 332, DateTimeKind.Local).AddTicks(3735));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2019, 2, 2, 15, 3, 3, 332, DateTimeKind.Local).AddTicks(3757));
        }
    }
}
