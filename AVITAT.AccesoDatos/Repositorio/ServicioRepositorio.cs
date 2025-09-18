using AVITAT.AccesoDatos.Data;
using AVITAT.AccesoDatos.Repositorio.IRepositorio;
using AVITAT.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVITAT.AccesoDatos.Repositorio
{
    public class ServicioRepositorio : Repositorio<Servicio>, IServicioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ServicioRepositorio(ApplicationDbContext db):base(db)
        {
                _db = db;
        }
        public void Actualizar(Servicio servicio)
        {
            var servicioBD = _db.Servicios.FirstOrDefault(b => b.Id == servicio.Id);
            if(servicioBD != null)
            {
                servicioBD.NombreLocal = servicio.NombreLocal;
                servicioBD.Tipo = servicio.Tipo;
                servicioBD.Precio = servicio.Precio;
                servicioBD.HorarioAtencion = servicio.HorarioAtencion;   
                _db.SaveChanges();
            }
        }
    }
}
