using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVITAT.Modelos
{
    public class Ave
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Nombre es requerido")]     
        public string NombreComun { get; set; }


        [Required(ErrorMessage = "Nombre Cientifico es requerido")]
        public string NombreCientifico { get; set; }


        [Required(ErrorMessage = "Tipo es requerido")]
        public string Tipo { get; set; }


        [Required(ErrorMessage = "Descripcion es requerido")]
        public string Descripcion { get; set; }

        public string ImagenUrl { get; set; }


    }
}
