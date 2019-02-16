using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace rest_api_sistema_compra_venta.Models
{
    public class DetalleVenta: EntityBase
    {
        [ForeignKey("IdVenta")]
        [JsonIgnore]
        public Venta Venta {get; set;}
        [Required]
        public long IdVenta {get;set;}

        [ForeignKey("IdArticulo")]
        [JsonIgnore]
        public Articulo Articulo {get; set;}
        [Required]
        public long IdArticulo {get; set;}

        [Required]
        public int Cantidad {get; set;}

        [Required]
        public decimal Precio {get; set;}

        public decimal? Descuento {get; set;}

        [NotMapped]
        public string NombreArticulo => Articulo?.Nombre;

    }
}