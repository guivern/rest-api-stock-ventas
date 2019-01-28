using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rest_api_sistema_compra_venta.Models
{
    public class EntityBase
    {
        public const int DESCRIPCION_MAX_LENGTH = 256;

        [Key]
        public long Id {get; set;}

        [JsonIgnore]
        public DateTime FechaCreacion {get; set;} = DateTime.Now;
        [JsonIgnore]
        public DateTime? FechaModificacion {get; set;}
        public string Descripcion {get; set;}
    }
}