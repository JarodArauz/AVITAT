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
    public class ReservaNaturalRepositorio : Repositorio<ReservaNatural>, IReservaNaturalRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ReservaNaturalRepositorio(ApplicationDbContext db):base(db)
        {
                _db = db;
        }
        public void Actualizar(ReservaNatural reservaNatural)
        {
            var reservaNaturalBD = _db.ReservasNaturales.FirstOrDefault(b => b.Id == reservaNatural.Id);
            if(reservaNaturalBD != null)
            {
                reservaNaturalBD.Nombre = reservaNatural.Nombre;
                reservaNaturalBD.Descripcion = reservaNatural.Descripcion;
                reservaNaturalBD.Ubicacion = reservaNatural.Ubicacion;
                reservaNaturalBD.Acceso = reservaNatural.Acceso;   
                _db.SaveChanges();
            }
        }
    }
}
