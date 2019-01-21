using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Models
{
    public class Categoria
    {
        public const int NOMBRE_MAX_LENGTH = 50;
        public const int DESCRIPCION_MAX_LENGTH = 256;

        public long Id {get; set;}
        [Required]
        [MaxLength(NOMBRE_MAX_LENGTH, 
        ErrorMessage="El nombre de categoría no debe tener más de ${NOMBRE_MAX_LENGTH} caracteres")]
        public string Nombre {get; set;}
        [MaxLength(DESCRIPCION_MAX_LENGTH,
        ErrorMessage="La descripción no puede tener más de ${DESCRIPCION_MAX_LENGTH} caracteres")]
        public string Descripcion {get; set;}
        public bool Activo {get; set;}
    }
}