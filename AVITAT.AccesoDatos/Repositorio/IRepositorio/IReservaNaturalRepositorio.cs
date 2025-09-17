using AVITAT.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVITAT.AccesoDatos.Repositorio.IRepositorio
{
    public interface IReservaNaturalRepositorio:IRepositorio<ReservaNatural>
    {
        void Actualizar(ReservaNatural reservaNatural);

    }
}
