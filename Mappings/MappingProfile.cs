using AutoMapper;
using rest_api_sistema_compra_venta.Controllers;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaDto, Categoria>();
        }
    }
}