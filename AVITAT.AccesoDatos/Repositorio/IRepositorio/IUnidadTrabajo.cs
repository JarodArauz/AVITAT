using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVITAT.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IReservaNaturalRepositorio ReservaNatural {  get; }
        IServicioRepositorio Servicio {  get; }
        IAveRepositorio Ave {  get; }

        Task Guardar();
    }
}
