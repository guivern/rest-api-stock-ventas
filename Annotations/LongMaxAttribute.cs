using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Annotations
{
    public class LongMaxAttribute: MaxLengthAttribute
    {
        public LongMaxAttribute(int length): base(length)
        {
            //ErrorMessage = "{0} no debe tener más de {1} caracteres.";
            ErrorMessage = "No debe tener más de {1} caracteres";
        }
    }
}