using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_api_sistema_compra_venta.Annotations;
using rest_api_sistema_compra_venta.Data;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Controllers
{
    [Authorize(Roles = "Almacenero, Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : CrudControllerBase<Articulo, ArticuloDto>
    {
        public ArticulosController(DataContext context, IMapper mapper)
        : base(context, mapper) { }

        [HttpGet("[action]")]
        public async Task<IActionResult> Buscar([FromQuery] string nombre, [FromQuery] string codigo)
        {
            if (nombre != null)
            {
                var articulos = await _context.Articulos
                .Include(a => a.Categoria)
                .Where(a => a.Nombre.ToLower().Contains(nombre.ToLower()))
                .Where(a => a.Activo)
                .ToListAsync();

                return Ok(articulos);
            }

            else if (codigo != null)
            {
                var articulo = await _context.Articulos
               .Include(a => a.Categoria)
               .FirstOrDefaultAsync(a => a.Codigo.Equals(codigo) && a.Activo);

                if (articulo == null)
                    return NotFound();

                return Ok(articulo);
            }
            return BadRequest(new {Error = "Debe especificar un parametro de busqueda"});

        }

        protected override IQueryable<Articulo> IncludeListFields(IQueryable<Articulo> query)
        {
            return base.IncludeListFields(query).Include(a => a.Categoria);
        }
    }

    public class ArticuloDto : DtoBase
    {
        public string Codigo { get; set; }
        [Requerido]
        [LongMax(Articulo.NOMBRE_MAX_LENGTH)]
        public string Nombre { get; set; }
        [LongMax(EntityBase.DESCRIPCION_MAX_LENGTH)]
        public string Descripcion { get; set; }
        [Requerido]
        [NoNegativo]
        public decimal? PrecioVenta { get; set; }
        [Requerido]
        [NoNegativo]
        public int? Stock { get; set; }
        [Requerido]
        public long? IdCategoria { get; set; }
        public bool? Activo { get; set; } = true;
    }
}