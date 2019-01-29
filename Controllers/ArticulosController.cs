using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController: CrudControllerBase<Articulo, ArticuloDto>
    {
        public ArticulosController(DataContext context, IMapper mapper)
        :base(context, mapper){}

         protected override IQueryable<Articulo> IncludeListFields(IQueryable<Articulo> query)
        {
            return base.IncludeListFields(query).Include(a => a.Categoria);
        } 
    }

    public class ArticuloDto: DtoBase
    {
        public string Codigo {get; set;}
        [Required(ErrorMessage="El nombre del artículo es requerido.")]
        [MaxLength(Articulo.NOMBRE_MAX_LENGTH,
        ErrorMessage="El nombre del artículo no debe tener más de 50 caracteres.")]
        public string Nombre {get; set;}
        [MaxLength(EntityBase.DESCRIPCION_MAX_LENGTH,
        ErrorMessage="La descripción no debe tener más de 256 caracteres.")]
        public string Descripcion {get; set;}
        public decimal PrecioVenta {get; set;}
        [Range(0, int.MaxValue, ErrorMessage="No se admite valores negativos.")]
        public int Stock {get; set;}
        [Required(ErrorMessage="La categoría es requerida.")]
        public long IdCategoria {get; set;}
        public bool? Activo {get; set;}
    }
}