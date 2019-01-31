using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_api_sistema_compra_venta.Annotations;
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
        [Requerido]
        [LongMax(Articulo.NOMBRE_MAX_LENGTH)]
        public string Nombre {get; set;}
        [LongMax(EntityBase.DESCRIPCION_MAX_LENGTH)]
        public string Descripcion {get; set;}
        [Requerido]
        [NoNegativo]
        public decimal? PrecioVenta {get; set;}
        [Requerido]
        [NoNegativo]
        public int? Stock {get; set;}
        [Requerido]
        public long? IdCategoria {get; set;}
        public bool? Activo {get; set;} = true;
    }
}