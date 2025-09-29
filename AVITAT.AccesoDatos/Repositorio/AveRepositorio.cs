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
    public class AveRepositorio : Repositorio<Ave>, IAveRepositorio
    {
        private readonly ApplicationDbContext _db;

        public AveRepositorio(ApplicationDbContext db):base(db)
        {
                _db = db;
        }
        public void Actualizar(Ave ave)
        {
            var aveBD = _db.Aves.FirstOrDefault(b => b.Id == ave.Id);
            if(aveBD != null)
            {
                aveBD.NombreComun = ave.NombreComun;
                aveBD.NombreCientifico = ave.NombreCientifico;
                aveBD.Tipo = ave.Tipo;
                aveBD.Descripcion = ave.Descripcion;   
                _db.SaveChanges();
            }
        }
    }
}
