using AVITAT.AccesoDatos.Data;
using AVITAT.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVITAT.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IReservaNaturalRepositorio ReservaNatural {  get; private set; }
        public IServicioRepositorio Servicio {  get; private set; }
        public IAveRepositorio Ave {  get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
                _db = db;
            ReservaNatural = new ReservaNaturalRepositorio(_db);
            Servicio = new ServicioRepositorio(_db);
            Ave = new AveRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
