using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;
namespace TravelWeb.Data.Repository
{
    public class CatalogoEstadoRepository
    {
        TravelContext context;

        //Inyección del objeto
        public CatalogoEstadoRepository(TravelContext context)
        {
            this.context = context;
        }

        //Creación de CRUD

        public CatalogoEstado Get(int id)
        {

            CatalogoEstado catalogoEstado = context.CatalogoDeEstados.FirstOrDefault(e => e.id_estado == id);
            return catalogoEstado;
        }

        public IList<CatalogoEstado> GetAll()
        {
            IList<CatalogoEstado> catalogoEstadosList = context.CatalogoDeEstados.ToList();
            return catalogoEstadosList;
        }
    }
}
