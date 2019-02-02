using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Models
{
    public class Proveedor: EntityBase
    {
        [Required]
        public string RazonSocial {get; set;}
        [Required]
        public string TipoDocumento {get; set;}
        [Required]
        public string NroDocumento {get; set;}
        public string Direccion {get; set;}
        public string Telefono {get; set;}
        public string Email {get; set;}
    }
}