using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace rest_api_stock_ventas.Controllers
{
    /// <summary>
    /// Sirve los archivos estaticos del frontend.
    /// </summary>
    public class HomeController: Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}