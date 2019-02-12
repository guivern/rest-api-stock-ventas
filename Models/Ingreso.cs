using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace rest_api_sistema_compra_venta.Models
{
    public class Ingreso: EntityBase
    {
        [ForeignKey("IdProveedor")]
        [JsonIgnore]
        public Proveedor Proveedor {get; set;}
        [Required]
        public long IdProveedor {get; set;}

        [ForeignKey("IdUsuario")]
        [JsonIgnore]
        public Usuario Usuario  {get; set;}
        [Required]
        public long IdUsuario {get; set;}

        [Required]
        public string TipoComprobante {get; set;}
        [Required]
        public string NroComprobante {get; set;}
        [Required]
        public DateTime FechaHora {get; set;}
        [Required]
        public decimal Impuesto {get; set;}
        [Required]
        public decimal Total {get; set;}
        [Required]
        public string Estado {get; set;}

        [NotMapped]
        public ICollection<DetalleIngreso> Detalles {get; set;}
        [NotMapped]
        public string NombreUsuario => Usuario?.Username;
        [NotMapped]
        public string NombreProveedor => Proveedor?.RazonSocial; 
    }
}