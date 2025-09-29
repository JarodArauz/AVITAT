using AVITAT.AccesoDatos.Repositorio.IRepositorio;
using AVITAT.Modelos;
using AVITAT.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;

namespace AVITAT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AveController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AveController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
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
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(ave.Id == 0)
                {
                    //Crear
                    string upload = webRootPath + DS.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                        ave.ImagenUrl = fileName + extension;
                        await _unidadTrabajo.Ave.Agregar(ave);
                        TempData[DS.Exitosa] = "Ave Creada Exitosamente";
                }
                else
                {
                    //Actualizar
                    var objAve = await _unidadTrabajo.Ave.ObtenerPrimero(p => p.Id == ave.Id, isTracking: false);
                    if (files.Count>0) //si se carga una nueva imagen
                    {
                        string upload = webRootPath+DS.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension (files[0].FileName);

                        //Borrar la imagen anterior
                        var anteriorFile = Path.Combine(upload, objAve.ImagenUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo (fileStream);
                        }
                        ave.ImagenUrl= fileName + extension;
                    }
                    else
                    {
                        ave.ImagenUrl = objAve.ImagenUrl;
                    }
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
            //Remover la imagen
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRuta;
            var anteriorFile = Path.Combine(upload, aveDb.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
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
