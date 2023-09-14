using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;

namespace TravelWeb.Data.Repository
{
    public class CatalogoCiudadRepository
    {
        TravelContext context;

        //Inyección del objeto
        public CatalogoCiudadRepository(TravelContext context)
        {
            this.context = context;
        }

        //Creación de CRUD
        public CatalogoCiudad Get(int id)
        {

            CatalogoCiudad catalogoCiudad = context.CatalogoDeCiudades.FirstOrDefault(e => e.Ciudad_ID == id);
            return catalogoCiudad;
        }

        public IList<CatalogoCiudad> GetAll()
        {
            IList<CatalogoCiudad> catalogoCiudades = context.CatalogoDeCiudades.ToList();
            return catalogoCiudades;
        }

        public void Create(CatalogoCiudad ciudadesDomain)
        {
            context.CatalogoDeCiudades.Add(ciudadesDomain);
            context.SaveChanges();

        }

        public void Update(CatalogoCiudad nuevaCiudad)
        {
            CatalogoCiudad ciudadActual = Get(nuevaCiudad.Ciudad_ID);
            ciudadActual.Nombre = nuevaCiudad.Nombre;
            ciudadActual.Temperatura = nuevaCiudad.Temperatura;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            CatalogoCiudad ciudadBorrar = Get(id);
            context.CatalogoDeCiudades.Remove(ciudadBorrar);
            context.SaveChanges();
        }
    }
}
