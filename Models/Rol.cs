using rest_api_sistema_compra_venta.Annotations;

namespace rest_api_sistema_compra_venta.Models
{
    public class Rol: SoftDeleteEntityBase
    {
        [Requerido]
        public string Nombre{get; set;}
    }
}