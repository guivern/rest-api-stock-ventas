using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Annotations
{
    public class DecimalNoNegativoAttribute: RangeAttribute
    {
        static double minDouble = 0;
        static double maxDouble = double.MaxValue;

        DecimalNoNegativoAttribute():base(minDouble, maxDouble)
        {
            ErrorMessage = "No admite valores negativos";
        }
    }
}