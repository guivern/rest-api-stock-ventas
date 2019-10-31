using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rest_api_sistema_compra_venta.Annotations;
using rest_api_sistema_compra_venta.Data;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Controllers
{
    [Authorize(Roles = "Almacenero, Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController: CrudControllerBase<Proveedor,ProveedorDto>
    {
        public ProveedoresController(DataContext context, IMapper mapper)
        :base(context,mapper){}    
    }

    public class ProveedorDto: DtoBase
    {
        [Requerido]
        public string RazonSocial {get; set;}
        [Requerido]
        public string TipoDocumento {get; set;}
        [Requerido]
        public string NroDocumento {get; set;}
        public string Direccion {get; set;}
        public string Telefono {get; set;}
        [EmailAddressAttribute(ErrorMessage="No es un email válido")]
        public string Email {get; set;}
    }
}