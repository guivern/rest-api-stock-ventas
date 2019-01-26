using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Models
{
    public class Categoria: SoftDeleteEntityBase
    {
        public const int NOMBRE_MAX_LENGTH = 50;

        [Required]
        public string Nombre {get; set;}
    }
}