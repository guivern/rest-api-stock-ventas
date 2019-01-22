using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Models
{
    public class Categoria
    {
        public const int NOMBRE_MAX_LENGTH = 50;
        public const int DESCRIPCION_MAX_LENGTH = 256;

        public long Id {get; set;}
        [Required(ErrorMessage="El nombre de categoría es requerido")]
        [MaxLength(NOMBRE_MAX_LENGTH, 
        ErrorMessage="El nombre de categoría no debe tener más de 50 caracteres")]
        public string Nombre {get; set;}
        [MaxLength(DESCRIPCION_MAX_LENGTH,
        ErrorMessage="La descripción no debe tener más de 256 caracteres")]
        public string Descripcion {get; set;}
        public bool Activo {get; set;}
    }
}