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
    [Authorize(Roles = "Vendedor, Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController: CrudControllerBase<Cliente, ClienteDto>
    {

        public ClientesController(DataContext context, IMapper mapper)
        :base(context, mapper){}
        
    }

    public class ClienteDto: DtoBase
    {
        [Requerido]
        public string Nombre {get; set;}
        [Requerido]
        public string Apellido {get; set;}
        [Requerido]
        public string TipoDocumento {get; set;}
        [Requerido]
        public string NumeroDocumento {get; set;}
        public DateTime? FechaNacimiento {get; set;}
        public string Direccion {get; set;}
        public string Telefono {get; set;}
        [EmailAddressAttribute(ErrorMessage="No es un email válido")]
        public string Email {get; set;}
    }
}