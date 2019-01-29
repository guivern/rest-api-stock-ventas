using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Annotations
{
    public class NoNegativo: RangeAttribute
    {
        public NoNegativo():base(0, int.MaxValue)
        {
            ErrorMessage = "No admite valores negativos";
        }
    }
}