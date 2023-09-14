using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;
using TravelWeb.Service;

namespace TravelWeb.Controllers
{
    public class VentaController : Controller
    {
        IVentaService ventaService;

        public VentaController(IVentaService ventaService)
        {
            this.ventaService = ventaService;
        }
        // GET: HomeController1
        public ActionResult Index(string mensaje = "NA")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                IList<VentaModel> listaVentas = ventaService.GetAll();
                return View(listaVentas);

            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                VentaModel venta = ventaService.Get(id);
                return View(venta);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: HomeController1/Details/5
        public ActionResult DetailsVenta(int id, string mensaje = "NA")
        {
            ViewData["mensaje"] = mensaje;
            try
            {


                IList<VentaModel> listaVentas = ventaService.GetAll();
                return View(listaVentas.Where(m => m.Cliente_ID == id));

            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            try
            {
                return View(ventaService.Get());
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VentaModel ventaModel)
        {
            try
            {
                // Guardar el clienteModel
                ventaService.Create(ventaModel);
                return RedirectToAction("DetailsVenta", new { id=ventaModel.Cliente_ID ,mensaje = "¡Venta confirmada!" + "  " +
                    "Lugares comprados: " + ventaModel.Cantidad});
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("Index", new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                //return View("Error", new ErrorViewModel() { Message = ex.Message });
                return RedirectToAction("Index", new { mensaje = "No se pudo realizar la venta, intente mas tarde." });
            }
        }     
    }
}
