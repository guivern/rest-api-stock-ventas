using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using rest_api_sistema_compra_venta.Data;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_stock_ventas.Data
{
    /// <summary>
    /// Clase para sembrar datos semilla
    /// </summary>
    public static class Seed
    {
        public static void SeedUsuarios(DataContext context)
        {
            if (context.Usuarios.Any()) return;

            var data = File.ReadAllText("Data/UserSeedData.json");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(data);

            foreach (var usuario in usuarios)
            {
                usuario.HashPassword = GenerateHash("password");
                usuario.Username = usuario.Username.ToLower();

                context.Add(usuario);
            }

            context.SaveChanges();
        }

        public static void SeedCategorias(DataContext context)
        {
            if (context.Categorias.Any()) return;

            var data = File.ReadAllText("Data/CategoriasSeedData.json");
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(data);

            context.Categorias.AddRange(categorias);
            context.SaveChanges();
        }

        public static void SeedArticulos(DataContext context)
        {
            if (context.Articulos.Any()) return;

            var data = File.ReadAllText("Data/ArticulosSeedData.json");
            var articulos = JsonConvert.DeserializeObject<List<Articulo>>(data);

            context.Articulos.AddRange(articulos);
            context.SaveChanges();
        }

        public static void SeedClientes(DataContext context)
        {
            if (context.Clientes.Any()) return;

            var data = File.ReadAllText("Data/ClientesSeedData.json");
            var clientes = JsonConvert.DeserializeObject<List<Cliente>>(data);

            context.Clientes.AddRange(clientes);
            context.SaveChanges();
        }

        public static void SeedProveedores(DataContext context)
        {
            if (context.Proveedores.Any()) return;

            var data = File.ReadAllText("Data/ProveedoresSeedData.json");
            var proveedores = JsonConvert.DeserializeObject<List<Proveedor>>(data);

            context.Proveedores.AddRange(proveedores);
            context.SaveChanges();
        }

        private static byte[] GenerateHash(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;

            using (SHA512 shaEncrypter = new SHA512Managed())
            {
                return shaEncrypter.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}