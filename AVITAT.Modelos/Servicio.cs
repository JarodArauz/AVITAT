using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVITAT.Modelos
{
    public class Servicio
    {
        [Key]
        public int Id {  get; set; }


        [Required(ErrorMessage ="Tipo es Requerido")]
        public string Tipo {  get; set; }


        [Required(ErrorMessage = "Nombre es Requerido")]
        [MaxLength(60,ErrorMessage = "Nombre de ser maximo de 60 caracteres")]
        public string NombreLocal {  get; set; }


        [Required(ErrorMessage = "Precio es Requerido")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio {  get; set; }


        [Required(ErrorMessage = "Horario es Requerido")]
        public string HorarioAtencion {  get; set; }
    }
}
