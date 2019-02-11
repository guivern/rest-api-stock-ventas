using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_api_sistema_compra_venta.Annotations;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Controllers
{
    [Authorize(Roles = "Almacenero, Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : CrudControllerBase<Ingreso, IngresoDto>
    {
        public IngresosController(DataContext context, IMapper mapper) : base(context, mapper) { }

        protected override IQueryable<Ingreso> IncludeListFields(IQueryable<Ingreso> query)
        {
            return base.IncludeListFields(query)
            .Include(i => i.Proveedor)
            .Include(i => i.Usuario)
            .OrderByDescending(i => i.Id)
            .Take(100); // toma los ultimos 100 ingresos
        }

        [HttpPost]
        public override async Task<IActionResult> Create(IngresoDto dto)
        {
            if (!await _context.Usuarios.AnyAsync(u => u.Id == dto.IdUsuario))
                return BadRequest("No existe el usuario");
            if (!await _context.Proveedores.AnyAsync(u => u.Id == dto.IdProveedor))
                return BadRequest("No existe el proveedor");

            if (dto.Detalles.Count == 0)
                return BadRequest(new { detallesError = "Debe agregar artículos al detalle" });

            Ingreso ingreso = new Ingreso
            {
                IdProveedor = (long)dto.IdProveedor,
                IdUsuario = dto.IdUsuario,
                TipoComprobante = dto.TipoComprobante,
                NroComprobante = dto.NroComprobante,
                FechaHora = DateTime.Now,
                Impuesto = (decimal)dto.Impuesto,
                Total = (decimal)dto.Total,
                Estado = "Aceptado",
                FechaCreacion = DateTime.Now
            };

            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();

            foreach (var detalleDto in dto.Detalles)
            {
                //se registra el detalle
                DetalleIngreso detalle = new DetalleIngreso
                {
                    IdIngreso = ingreso.Id,
                    IdArticulo = detalleDto.IdArticulo,
                    Cantidad = detalleDto.Cantidad,
                    Precio = detalleDto.Precio,
                    FechaCreacion = DateTime.Now
                };
                _context.DetallesIngresos.Add(detalle);

                //se actualiza el stock
                var articulo = await _context.Articulos.FindAsync(detalleDto.IdArticulo);
                articulo.Stock += detalleDto.Cantidad;
                _context.Articulos.Update(articulo);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return Ok(new { id = ingreso.Id });
        }

        [HttpGet("detalle/{id:long}")]
        public async Task<ActionResult<IEnumerable>> GetDetalles(long id)
        {
            if (!await _context.Ingresos.AnyAsync(i => i.Id == id))
                return NotFound();

            var detalles = await _context.DetallesIngresos
            .Where(d => d.IdIngreso == id)
            .Include(d => d.Articulo)
            .Select(d => new
            {
                d.IdArticulo,
                d.Cantidad,
                d.Precio,
                nombre = d.Articulo.Nombre
            }).ToListAsync();

            return detalles;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery] string filtro)
        {
            var query = _context.Ingresos.AsQueryable();

            if (filtro == null)
            {
                return Ok(await IncludeListFields(query).ToListAsync());
            }
                
            var ingresos = await query
            .Include(i => i.Proveedor)
            .Where(i => i.NroComprobante.Contains(filtro)
            || i.Proveedor.RazonSocial.Contains(filtro))
            .ToListAsync();

            return Ok(ingresos);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular(long id)
        {
            var ingreso = await _context.Ingresos
            .Include(i => i.Detalles)
            .FirstOrDefaultAsync(i => i.Id == id);

            if (ingreso == null) return NotFound();

            // se anula el ingreso
            ingreso.Estado = "Anulado";

            // se actualiza el stock
            foreach (var det in ingreso.Detalles)
            {
                var articulo = await _context.Articulos.FindAsync(det.IdArticulo);

                articulo.Stock -= det.Cantidad;
                _context.Articulos.Update(articulo);
                await _context.SaveChangesAsync();
            }
            _context.Ingresos.Update(ingreso);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class IngresoDto : DtoBase
    {
        [Requerido]
        public long? IdProveedor { get; set; }
        [Requerido]
        public long IdUsuario { get; set; }
        [Requerido]
        public string TipoComprobante { get; set; }
        [Requerido]
        public string NroComprobante { get; set; }
        [Requerido]
        [NoNegativo]
        public decimal? Impuesto { get; set; }
        [Requerido]
        [NoNegativo]
        public decimal? Total { get; set; }
        [Requerido]
        public List<DetalleDto> Detalles { get; set; }
    }

    public class DetalleDto : DtoBase
    {
        [Requerido]
        public long IdArticulo { get; set; }
        [Requerido]
        public int Cantidad { get; set; }
        [Requerido]
        public decimal Precio { get; set; }
    }
}