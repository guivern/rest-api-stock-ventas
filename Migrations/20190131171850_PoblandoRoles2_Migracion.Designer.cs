﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190131171850_PoblandoRoles2_Migracion")]
    partial class PoblandoRoles2_Migracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("rest_api_sistema_compra_venta.Models.Articulo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<string>("Codigo");

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime?>("FechaModificacion");

                    b.Property<long>("IdCategoria");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<decimal>("PrecioVenta");

                    b.Property<int>("Stock");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoria");

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("rest_api_sistema_compra_venta.Models.Categoria", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime?>("FechaModificacion");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("rest_api_sistema_compra_venta.Models.Rol", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime?>("FechaModificacion");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Activo = true,
                            Descripcion = "Rol que posee todos los permisos del sistema",
                            FechaCreacion = new DateTime(2019, 1, 31, 14, 18, 50, 334, DateTimeKind.Local).AddTicks(1016),
                            Nombre = "Administrador"
                        },
                        new
                        {
                            Id = 2L,
                            Activo = true,
                            Descripcion = "Rol que posee los permisos del módulo almacén",
                            FechaCreacion = new DateTime(2019, 1, 31, 14, 18, 50, 337, DateTimeKind.Local).AddTicks(1398),
                            Nombre = "Almacenero"
                        },
                        new
                        {
                            Id = 3L,
                            Activo = true,
                            Descripcion = "Rol que posee los permisos del módulo ventas",
                            FechaCreacion = new DateTime(2019, 1, 31, 14, 18, 50, 337, DateTimeKind.Local).AddTicks(1455),
                            Nombre = "Vendedor"
                        });
                });

            modelBuilder.Entity("rest_api_sistema_compra_venta.Models.Articulo", b =>
                {
                    b.HasOne("rest_api_sistema_compra_venta.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}