using AVITAT.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVITAT.AccesoDatos.Repositorio.IRepositorio
{
    public interface IAveRepositorio:IRepositorio<Ave>
    {
        void Actualizar(Ave ave);

    }
}
