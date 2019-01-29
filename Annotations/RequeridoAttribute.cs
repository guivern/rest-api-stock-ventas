using System.ComponentModel.DataAnnotations;

namespace rest_api_sistema_compra_venta.Annotations
{
    public class RequeridoAttribute: RequiredAttribute 
    {
        public RequeridoAttribute()
        {
            //ErrorMessage = "{0} es requerido";
            ErrorMessage = "Es requerido";
        }
    }
}