using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using rest_api_sistema_compra_venta.Annotations;

namespace rest_api_sistema_compra_venta.Models
{
    public class Usuario: PersonaEntityBase
    {
        public const int USERNAME_MAXLENGTH = 50;

        [Required]
        [MaxLength(USERNAME_MAXLENGTH)]
        public string Username {get; set;}
        [Required]
        [JsonIgnore]
        public byte[] HashPassword {get; set;}
        
        [ForeignKey("IdRol")]
        [JsonIgnore]
        public Rol Rol {get; set;}
        public long IdRol {get; set;}

        [NotMapped]
        public string NombreRol => Rol?.Nombre;
    }
}