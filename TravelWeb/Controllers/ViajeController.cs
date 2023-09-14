using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWeb.Domain;
using TravelWeb.Models;
using TravelWeb.Service;

namespace TravelWeb.Controllers
{
    public class ViajeController : Controller
    {
        //Inyección del service:
        IViajeService viajeService; 

        public ViajeController(IViajeService viajeService)
        {
            this.viajeService = viajeService;
        }

        //Implememntación de métodos (CRUD):

        // GET: ViajeController
        public ActionResult Index(string mensaje = "NA")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                IList<ViajeModel> listaViajes = viajeService.GetAllViajes();
                return View(listaViajes);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: ViajeController/Details/5
        public ActionResult Details(int id, string mensaje = "")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                ViajeModel viaje = viajeService.Get(id);
                return View(viaje);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: ViajeController/Create
        public ActionResult Create()
        {
            try
            {
                return View(viajeService.Get());
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.StackTrace });
            }
        }

        // POST: ViajeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViajeModel nuevoViaje)
        {
            try
            {
                // Guardar el ProductoModel
                viajeService.Create(nuevoViaje);
                return RedirectToAction("Index", new { mensaje = "¡Nuevo viaje agregado con éxito!" });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                //return View("Error", new ErrorViewModel() { Message = ex.Message });
                return RedirectToAction("Index", new { mensaje = "No se pudo crear un nuevo viaje." });
            }
        }

        // GET: ViajeController/Edit/5
        public ActionResult Edit(int id)
        {


            try
            {
                ViajeModel viajeActual = viajeService.Get(id);
                return View(viajeActual);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo editar el viaje en este momento." });
            }
        }

        // POST: ViajeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViajeModel viajeEditado)
        {
            try
            {
                viajeService.Update(viajeEditado);
                return RedirectToAction("Details", new { id=viajeEditado.Viaje_ID, mensaje = "¡El viaje se editó con éxito!       Si el viaje ya tiene ventas entonces fecha de inicio/fin, ciudad, nombre de viaje y descripción ya no se pueden modificar." });
            }
            catch (ApplicationException ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = ex.Message});
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo editar el producto en este momento." });
            }
        }

        // GET: ViajeController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                viajeService.Delete(id);
                return RedirectToAction("Index", new { mensaje = "El viaje se eliminó con éxito." });
            }
            catch (ApplicationException ax)
            {
                return RedirectToAction("Index", new { mensaje = ax.Message });
            }
            catch (Exception ax)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo eliminar el viaje en este momento." });
            }
        }


        
        public JsonResult GetList(DateTime ini, DateTime fin)
        {
            IList<ViajeModel> myList = viajeService.Filtrar(ini, fin);
            return Json(myList);
        }


        //Obtener la vista de estados filtrados.
        public ActionResult Indexx(string mensaje = "NA")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                IList<ViajeModel> listaViajes = viajeService.GetAllViajes();
                return View(listaViajes);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }
    }
}
