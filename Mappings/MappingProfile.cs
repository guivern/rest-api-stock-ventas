using AutoMapper;
using rest_api_sistema_compra_venta.Controllers;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Mappings
{
    public class MappingProfile: Profile
    {
        
        public MappingProfile()
        {
            /* Ejemplos:
                Mapper.CreateMap<Employee, EmployeeDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

                CreateMap<CategoriaDto, Categoria>()
                .ForMember(d => d.FechaCreacion, opt => opt.Ignore());
             */
            CreateMap<CategoriaDto, Categoria>();
            CreateMap<ArticuloDto, Articulo>();
            CreateMap<ClienteDto, Cliente>();
            CreateMap<ProveedorDto, Proveedor>();
        }
    }
}