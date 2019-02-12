using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace rest_api_sistema_compra_venta.Models
{
    public class DetalleIngreso: EntityBase
    {
        [ForeignKey("IdIngreso")]
        [JsonIgnore]
        public Ingreso Ingreso {get; set;}
        [Required]
        public long IdIngreso {get; set;}

        [ForeignKey("IdArticulo")]
        [JsonIgnore]
        public Articulo Articulo {get; set;}
        [Required]
        public long IdArticulo {get; set;}

        [Required]
        public int Cantidad {get; set;}
        [Required]
        public decimal Precio {get; set;}
    }
}