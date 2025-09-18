using AVITAT.AccesoDatos.Repositorio.IRepositorio;
using AVITAT.Modelos;
using AVITAT.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace AVITAT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservaNaturalController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ReservaNaturalController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ReservaNatural reservaNatural = new ReservaNatural();

            if(id == null)
            {
                //crear nueva
                return View(reservaNatural);
            }
            reservaNatural = await _unidadTrabajo.ReservaNatural.Obtener(id.GetValueOrDefault());
            if(reservaNatural == null)
            {
                return NotFound();
            }
            return View(reservaNatural);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ReservaNatural reservaNatural)
        {
            if (ModelState.IsValid)
            {
                if(reservaNatural.Id == 0)
                {
                    await _unidadTrabajo.ReservaNatural.Agregar(reservaNatural);
                    TempData[DS.Exitosa] = "Reserva Natural Creada Exitosamente";
                }
                else
                {
                    _unidadTrabajo.ReservaNatural.Actualizar(reservaNatural);
                    TempData[DS.Exitosa] = "Reserva Natural Actualizada Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al graba la reserva Natural";
            return View(reservaNatural);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.ReservaNatural.ObtenerTodos();
            return Json(new {data = todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var reservaNaturalDb = await _unidadTrabajo.ReservaNatural.Obtener(id);
            if(reservaNaturalDb == null)
            {
                return Json(new { success = false, message = "Error al borrar la Reserva Natural" });
            }
            _unidadTrabajo.ReservaNatural.Remover(reservaNaturalDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Reserva Natural borrada exitosamente" });

        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.ReservaNatural.ObtenerTodos();
            if(id== 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id );
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });   
        }

        #endregion
    }
}
