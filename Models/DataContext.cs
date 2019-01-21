using Microsoft.EntityFrameworkCore;

namespace rest_api_sistema_compra_venta.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        //public DbSet<TodoItem> TodoItems { get; set; }
    }
}