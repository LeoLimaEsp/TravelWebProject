using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;
using TravelWeb.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        ICatalogoService catalogoService;

        public MunicipioController(ICatalogoService _catalogoService)
        {
            catalogoService = _catalogoService;
        }

        // GET: api/<MunicipioController>
        [HttpGet("{id}")]
        public IEnumerable<CatalogoMunicipioModel> Get(int id)
        {
            var municipiosList = catalogoService.GetAllTown();
            return municipiosList.Where(m => m.id_estado == id).ToList();
                      
        }

        

       
    }
}
