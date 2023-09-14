using Microsoft.AspNetCore.Mvc;
using TravelWeb.Service;
using TravelWeb.Models;
using TravelWeb.Utils;
using TravelWeb.Domain;

namespace TravelWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeController : ControllerBase
    {
        //Inyección de service
        IViajeService viajeService;

        public ViajeController(IViajeService viajeService)
        {
            this.viajeService = viajeService;
        }

        // GET: api/<ViajeController>
        [HttpGet("{ini:DateTime}/{fin:DateTime}")]
        public IEnumerable<ViajeModel> Get(DateTime ini,DateTime fin)
        {
            IList<ViajeModel> viajesFiltrados = viajeService.GetAllViajes();

            return viajesFiltrados.Where(m => m.Fecha_inicio > ini && m.Fecha_fin < fin).ToList();
            
        }

        [HttpGet]
        public IEnumerable<ViajeModel> Get()
        {
            var viajesList = viajeService.GetAllViajes();

            return viajesList;

        }
    }
}
