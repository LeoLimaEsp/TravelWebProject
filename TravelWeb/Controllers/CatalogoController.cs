using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWeb.Service;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class CatalogoController : Controller
    {
        //Inyección del servie:
        ICatalogoService ciudadesService;

        public CatalogoController (ICatalogoService ciudadesService)
        {
            this.ciudadesService = ciudadesService;
        }

        // GET: CatalogoController
        public ActionResult Index(string mensaje = "NA")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                IList<CatalogoCiudadModel> listaCiudades = ciudadesService.GetAllCity();
                return View(listaCiudades);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: CatalogoController/Details/5
        public ActionResult Details(int id, string mensaje = "NA")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                CatalogoCiudadModel ciudad = ciudadesService.GetCity(id);
                return View(ciudad);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: CatalogoController/Create
        public ActionResult Create()
        {
            try
            {
                CatalogoCiudadModel ciudadModel = new CatalogoCiudadModel();
                return View(ciudadModel);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // POST: CatalogoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatalogoCiudadModel ciudadModel)
        {
            try
            {
                // Guardar el clienteModel
                ciudadesService.Create(ciudadModel);
                return RedirectToAction("Index", new { mensaje = "¡Nueva ciudad destino se agrego con éxito!" });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                //return View("Error", new ErrorViewModel() { Message = ex.Message });
                return RedirectToAction("Index", new { mensaje = "No se pudo agregar una nueva ciudad destino en este momento." });
            }
        }

        // GET: CatalogoController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                CatalogoCiudadModel ciudadActual = ciudadesService.GetCity(id);
                return View(ciudadActual);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // POST: CatalogoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CatalogoCiudadModel ciudadModel)
        {
            try
            {
                ciudadesService.Update(ciudadModel);
                return RedirectToAction("Details", new { id = ciudadModel.Ciudad_ID, mensaje = "La ciudad se editó con éxito." });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo editar la ciudad en este momento." });
            }
        }

        // GET: CatalogoController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                ciudadesService.Delete(id);
                return RedirectToAction("Index", new { mensaje = "¡La ciudad se eliminó con éxito!" });
            }
            catch (ApplicationException ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "La ciudad no se puede borrar en este momento." });
            }
        }    
    }
}
