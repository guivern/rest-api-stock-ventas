using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using rest_api_sistema_compra_venta.Data;
using rest_api_stock_ventas.Data;

namespace rest_api_sistema_compra_venta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // esto se ejecuta cuando se inicia la aplicacion
                    // crea la bd
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();

                    // precargamos algunos datos
                    Seed.SeedUsuarios(context);
                    Seed.SeedCategorias(context);
                    Seed.SeedArticulos(context);
                    Seed.SeedClientes(context);
                    Seed.SeedProveedores(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5001")
                .UseStartup<Startup>();
    }
}
