using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDocumentosController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable> List()
        {
            return PersonaEntityBase.DOCUMENTOS.ToList();
        }
    }
}