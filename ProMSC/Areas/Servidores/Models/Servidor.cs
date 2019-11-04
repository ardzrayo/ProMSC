using ProMSC.Areas.Clientes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProMSC.Areas.Servidores.Models
{
    public class Servidor
    {
        public int idvps { get; set; }
        [Required(ErrorMessage = "Seleccione un cliente.")]
        [Display(Name = "Cliente")]
        public int idcliente { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre para el servidor.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        [Display(Name = "Nombre VPS")]
        public string nombrevps { get; set; }
        [Display(Name = "CPU")]
        public int cpu { get; set; }
        [Display(Name = "RAM")]
        public int ram { get; set; }
        [Display(Name = "IP's")]
        public int ips { get; set; }
        [Display(Name = "Disco Duro (GB)")]
        public int discoduro { get; set; }
        [Display(Name = "Sistema Operativo")]
        public string osystem { get; set; }
        [Display(Name = "Base de datos")]
        public string database { get; set; }
        [Display(Name = "Escritorios Remotos")]
        public int desktop { get; set; }
        [Display(Name = "Ancho de banda")]
        public int anchobanda { get; set; }
        [Required(ErrorMessage = "Ingrese IP Publica")]
        [Display(Name = "IP Publica")]
        public string ip_publica { get; set; }
        [Required(ErrorMessage = "Ingrese IP Privada")]
        [Display(Name = "IP Privada")]
        public string ip_privada { get; set; }
        [StringLength(255, ErrorMessage = "La descripción no debe tener más de 255 caracteres.")]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        [Display(Name = "Estado")]
        public bool? estado { get; set; }
        [Display(Name = "Cliente")]
        public virtual Cliente cliente { get; set; }
    }
}
