using AVITAT.AccesoDatos.Repositorio.IRepositorio;
using AVITAT.Modelos;
using AVITAT.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace AVITAT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ServicioController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Servicio servicio = new Servicio();

            if(id == null)
            {
                //crear nueva
                return View(servicio);
            }
            //Actualizamos 
            servicio = await _unidadTrabajo.Servicio.Obtener(id.GetValueOrDefault());
            if(servicio == null)
            {
                return NotFound();
            }
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                if(servicio.Id == 0)
                {
                    await _unidadTrabajo.Servicio.Agregar(servicio);
                    TempData[DS.Exitosa] = "Servicio Creado Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Servicio.Actualizar(servicio);
                    TempData[DS.Exitosa] = "Servicio Actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar el servicio";
            return View(servicio);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Servicio.ObtenerTodos();
            return Json(new {data = todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var serviciolDb = await _unidadTrabajo.Servicio.Obtener(id);
            if(serviciolDb == null)
            {
                return Json(new { success = false, message = "Error al borrar el servicio" });
            }
            _unidadTrabajo.Servicio.Remover(serviciolDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Servicio borrado exitosamente" });

        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Servicio.ObtenerTodos();
            if(id== 0)
            {
                valor = lista.Any(b => b.NombreLocal.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NombreLocal.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id );
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
