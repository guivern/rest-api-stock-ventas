using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace rest_api_sistema_compra_venta.Models
{
    public class Articulo: SoftDeleteEntityBase
    {
        public const int NOMBRE_MAX_LENGTH = 50;

        public string Codigo {get; set;}
        [Required]
        public string Nombre {get; set;}
        [Required]
        public decimal PrecioVenta {get; set;}
        public int Stock {get; set;}
        
        [ForeignKey("IdCategoria")]
        [JsonIgnore]
        public Categoria Categoria {get; set;}
        public long IdCategoria {get; set;}

        [NotMapped]
        public string nombreCategoria => Categoria?.Nombre; 
    }
}