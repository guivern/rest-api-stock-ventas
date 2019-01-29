using Microsoft.EntityFrameworkCore;

namespace rest_api_sistema_compra_venta.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        :base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aqui se configuran los OnDelete cascade o restrict
            modelBuilder.Entity<Articulo>()
            .HasOne(a => a.Categoria)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Categoria> Categorias {get; set;}
        public DbSet<Articulo> Articulos {get; set;}
    }
}