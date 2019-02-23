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
                    IdArticulo = (long)detalleDto.IdArticulo,
                    Cantidad = (int)detalleDto.Cantidad,
                    Precio = (decimal)detalleDto.Precio,
                    FechaCreacion = DateTime.Now
                };
                _context.DetallesIngresos.Add(detalle);

                //se actualiza el stock
                var articulo = await _context.Articulos.FindAsync(detalleDto.IdArticulo);
                articulo.Stock += (int)detalleDto.Cantidad;
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

        [HttpGet("linq")]
        public async Task<ActionResult<IEnumerable>> GetLinq()
        {
            // puedo mandar un object con los sgtes atributos: anio, mes y dia
            DateTime date = new DateTime(2019,2,10);
            var ingresos = await (from i in _context.Ingresos 
                        join p in _context.Proveedores
                        on i.IdProveedor equals p.Id
                        join u in _context.Usuarios
                        on i.IdUsuario equals u.Id
                        where i.FechaHora > date
                        select new{
                            i.Id,
                            idProveedor = p.Id,
                            p.RazonSocial,
                            i.FechaHora,
                            usuario = u.Username 
                        })
                        .OrderByDescending(i => i.Id)
                        .ToListAsync();
            return Ok(ingresos); 
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery] string filtro)
        {
            if (filtro == null)
            {
                var query = _context.Ingresos.AsQueryable();
                return Ok(await IncludeListFields(query).ToListAsync());
            }

            var ingresos = await _context.Ingresos
            .Include(i => i.Proveedor)
            .Include(i => i.Usuario)
            .Where(i => i.NroComprobante.Contains(filtro)
            || i.Proveedor.RazonSocial.ToLower().Contains(filtro.ToLower())
            || i.Usuario.Username.ToLower().Contains(filtro.ToLower())
            || i.FechaHora.ToString().Contains(filtro))
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
        [NoNegativo]
        public decimal? Total { get; set; }
        [Requerido]
        public List<DetalleDto> Detalles { get; set; }
    }

    public class DetalleDto : DtoBase
    {
        [Requerido]
        public long? IdArticulo { get; set; }
        [Requerido]
        [NoNegativo]
        public int? Cantidad { get; set; }
        [Requerido]
        [NoNegativo]
        public decimal? Precio { get; set; }
    }
}