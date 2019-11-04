using ProMSC.Areas.Servidores.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProMSC.Areas.Clientes.Models
{
    public class Cliente
    {
        public int idcliente { get; set; }
        [Required(ErrorMessage = "Ingrese Razón Social.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        [Display(Name = "Razón Social")]
        public string razonsocial { get; set; }
        [StringLength(255, ErrorMessage = "La ubicación no debe tener más de 255 caracteres.")]
        [Display(Name = "Ubicación")]
        public string ubicacion { get; set; }
        [Display(Name = "Contacto")]
        public string contacto { get; set; }
        [Required(ErrorMessage = "Ingrese Email.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        [Display(Name = "Estado")]
        public bool? estado { get; set; }
        public virtual ICollection<Servidor> servidores { get; set; }
    }
}
