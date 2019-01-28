namespace rest_api_sistema_compra_venta.Models
{
    public class SoftDeleteEntityBase: EntityBase
    {
        public bool Activo {get; set;} = true;
    }
}