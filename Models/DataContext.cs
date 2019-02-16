using Microsoft.EntityFrameworkCore;

namespace rest_api_sistema_compra_venta.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        :base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aqui se agregan los roles del sistema
            modelBuilder.Entity<Rol>().HasData(
                new Rol{Id = 1, Nombre="Administrador", Descripcion="Rol que posee todos los permisos del sistema", Activo = true},
                new Rol{Id = 2, Nombre="Almacenero", Descripcion="Rol que posee los permisos del módulo almacén", Activo = true},
                new Rol{Id = 3, Nombre="Vendedor", Descripcion="Rol que posee los permisos del módulo ventas", Activo = true}
            );

            // Aqui se configuran los OnDelete cascade o restrict
            modelBuilder.Entity<Articulo>()
            .HasOne(a => a.Categoria)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Rol)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ingreso>()
            .HasOne(i => i.Proveedor)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ingreso>()
            .HasOne(i => i.Usuario)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleIngreso>()
            .HasOne(d => d.Ingreso)
            .WithMany(i => i.Detalles)
            .HasForeignKey(d => d.IdIngreso)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleIngreso>()
            .HasOne(d => d.Articulo)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
            .HasOne(v => v.Cliente)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
            .HasOne(v => v.Usuario)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleVenta>()
            .HasOne(d => d.Venta)
            .WithMany(v => v.Detalles)
            .HasForeignKey(d => d.IdVenta)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleVenta>()
            .HasOne(d => d.Articulo)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<Categoria> Categorias {get; set;}
        public DbSet<Articulo> Articulos {get; set;}
        public DbSet<Rol> Roles {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Cliente> Clientes {get; set;}
        public DbSet<Proveedor> Proveedores {get; set;}
        public DbSet<Ingreso> Ingresos {get; set;}
        public DbSet<DetalleIngreso> DetallesIngresos {get; set;}
        public DbSet<Venta> Ventas {get; set;}
        public DbSet<DetalleVenta> DetallesVentas {get; set;}
    }
}