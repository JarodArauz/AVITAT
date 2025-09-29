using AVITAT.AccesoDatos.Repositorio.IRepositorio;
using AVITAT.Modelos;
using AVITAT.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace AVITAT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AveController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public AveController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Ave ave = new Ave();

            if(id == null)
            {
                //crear nueva
                return View(ave);
            }
            //Actualizamos 
            ave = await _unidadTrabajo.Ave.Obtener(id.GetValueOrDefault());
            if(ave == null)
            {
                return NotFound();
            }
            return View(ave);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Ave ave)
        {
            if (ModelState.IsValid)
            {
                if(ave.Id == 0)
                {
                    await _unidadTrabajo.Ave.Agregar(ave);
                    TempData[DS.Exitosa] = "Ave Creada Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Ave.Actualizar(ave);
                    TempData[DS.Exitosa] = "Ave Actualizada Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Ave";
            return View(ave);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Ave.ObtenerTodos();
            return Json(new {data = todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var aveDb = await _unidadTrabajo.Ave.Obtener(id);
            if(aveDb == null)
            {
                return Json(new { success = false, message = "Error al borrar el Ave" });
            }
            _unidadTrabajo.Ave.Remover(aveDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Ave borrada exitosamente" });

        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Ave.ObtenerTodos();
            if(id== 0)
            {
                valor = lista.Any(b => b.NombreComun.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NombreComun.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id );
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
