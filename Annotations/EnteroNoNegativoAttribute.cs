using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Annotations
{
    public class EnteroNoNegativoAttribute: RangeAttribute
    {
        static int minInt = 0;
        static int maxInt = int.MaxValue;

        EnteroNoNegativoAttribute(): base(minInt, maxInt)
        {
            ErrorMessage = "No admite valores negativos";
        }
    }
}