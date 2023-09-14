using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;
namespace TravelWeb.Data.Repository
{
    public class CatalogoMunicipioRepository
    {
        TravelContext context;

        //Inyección del objeto
        public CatalogoMunicipioRepository(TravelContext context)
        {
            this.context = context;
        }

        //Creación de CRUD

        public CatalogoMunicipio Get(int id)
        {

            CatalogoMunicipio catalogoMunicipios = context.CatalogoDeMunicipios.FirstOrDefault(e => e.id_municipio == id);
            return catalogoMunicipios;
        }

        public IList<CatalogoMunicipio> GetAll()
        {
            IList<CatalogoMunicipio> catalogoMunicipiosList = context.CatalogoDeMunicipios.ToList();
            return catalogoMunicipiosList;
        }
    }
}
