using System;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Vendedor, Administrador")]
    public class VentasController : CrudControllerBase<Venta, VentaDto>
    {
        public VentasController(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Venta> IncludeListFields(IQueryable<Venta> query)
        {
            return query
            .Include(v => v.Cliente)
            .Include(v => v.Usuario)
            .OrderByDescending(v => v.Id)
            .Take(100); // toma las ultimos 100 ventas
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Detail(long id, [FromQuery] bool Inactivo)
        {
            var venta = await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Usuario)
            .Include(v => v.Detalles) // incluye los detalles
            .ThenInclude(d => d.Articulo)
            .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null) return NotFound();

            return Ok(venta);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery] string filtro, [FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin)
        {
            if (filtro == null && (fechaInicio == null || fechaFin == null))
            {
                var query = _context.Ventas.AsQueryable();
                return Ok(await IncludeListFields(query).ToListAsync());
            }
            else if (filtro != null && (fechaInicio == null || fechaFin == null))
            {
                var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Where(v => v.NroComprobante.Contains(filtro)
                || v.Cliente.Nombre.ToLower().Contains(filtro.ToLower())
                || v.Cliente.Apellido.ToLower().Contains(filtro.ToLower())
                || v.Usuario.Username.ToLower().Contains(filtro.ToLower()))
                .ToListAsync();

                return Ok(ventas);
            }
            else if (filtro != null && fechaInicio != null && fechaFin != null)
            {
                var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Where(v => v.NroComprobante.Contains(filtro)
                || v.Cliente.Nombre.ToLower().Contains(filtro.ToLower())
                || v.Cliente.Apellido.ToLower().Contains(filtro.ToLower())
                || v.Usuario.Username.ToLower().Contains(filtro.ToLower()))
                .Where(v => v.FechaHora.Date >= fechaInicio.Value.Date && v.FechaHora.Date <= fechaFin.Value.Date)
                .ToListAsync();

                return Ok(ventas);
            }
            else
            {
                var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Where(v => v.FechaHora.Date >=  fechaInicio.Value.Date && v.FechaHora.Date <= fechaFin.Value.Date)
                .OrderByDescending(v => v.Id)
                .Take(100)
                .ToListAsync();

                return Ok(ventas);
            }


        }

        [HttpPost]
        public override async Task<IActionResult> Create(VentaDto dto)
        {
            if (!await _context.Usuarios.AnyAsync(u => u.Id == dto.IdUsuario))
                return BadRequest("No existe el usuario");
            if (!await _context.Clientes.AnyAsync(u => u.Id == dto.IdCliente))
                return BadRequest("No existe el cliente");

            if (dto.Detalles.Count == 0)
                return BadRequest(new { detallesError = "Debe agregar artículos al detalle" });

            Venta venta = new Venta
            {
                IdCliente = (long)dto.IdCliente,
                IdUsuario = (long)dto.IdUsuario,
                TipoComprobante = dto.TipoComprobante,
                NroComprobante = dto.NroComprobante,
                FechaHora = DateTime.Now,
                Impuesto = (decimal)dto.Impuesto,
                Total = (decimal)dto.Total,
                Estado = "Aceptado",
                FechaCreacion = DateTime.Now
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            foreach (var detalleDto in dto.Detalles)
            {
                //se registra el detalle
                DetalleVenta detalle = new DetalleVenta
                {
                    IdVenta = venta.Id,
                    IdArticulo = (long)detalleDto.IdArticulo,
                    Cantidad = (int)detalleDto.Cantidad,
                    Precio = (decimal)detalleDto.Precio,
                    FechaCreacion = DateTime.Now,
                    Descuento = detalleDto.Descuento
                };
                _context.DetallesVentas.Add(detalle);

                //se actualiza el stock
                var articulo = await _context.Articulos.FindAsync(detalleDto.IdArticulo);
                articulo.Stock -= (int)detalleDto.Cantidad;
                _context.Articulos.Update(articulo);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return Ok(new { id = venta.Id });
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular(long id)
        {
            var venta = await _context.Ventas
            .Include(v => v.Detalles)
            .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null) return NotFound();

            // se anula el ingreso
            venta.Estado = "Anulado";

            // se actualiza el stock
            foreach (var det in venta.Detalles)
            {
                var articulo = await _context.Articulos.FindAsync(det.IdArticulo);

                articulo.Stock += det.Cantidad;
                _context.Articulos.Update(articulo);
                await _context.SaveChangesAsync();
            }
            _context.Ventas.Update(venta);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }

    public class VentaDto : DtoBase
    {
        [Requerido]
        public long? IdCliente { get; set; }
        [Requerido]
        public long? IdUsuario { get; set; }
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
        public List<DetalleVentaDto> Detalles { get; set; }
    }

    public class DetalleVentaDto : DtoBase
    {
        [Requerido]
        public long? IdArticulo { get; set; }

        [Requerido]
        [NoNegativo]
        public int? Cantidad { get; set; }

        [Requerido]
        [NoNegativo]
        public decimal? Precio { get; set; }

        [NoNegativo]
        public decimal? Descuento { get; set; } = 0;


    }
}