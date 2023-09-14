using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;
using TravelWeb.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        ICatalogoService catalogoService;

        public EstadosController(ICatalogoService _catalogoService) 
        {
            catalogoService = _catalogoService;
        }

        // GET: api/<EstadosApiController>
        [HttpGet]
        public IEnumerable<CatalogoEstadoModel> Get()
        {
            var estadosList = catalogoService.GetAllStates();
            return estadosList;
        }

        // GET api/<EstadosApiController>/5
        [HttpGet("{id}")]
        public CatalogoEstadoModel Get(int id)
        {
            var estadoUnico = catalogoService.GetState(id);
            return estadoUnico;
        }      
    }
}
