using TravelWeb.Models;

namespace TravelWeb.Service
{
    public interface ICatalogoService
    {
        // Definir las operaciones de Servicio para ciudades:
        void Create(CatalogoCiudadModel ciudadesModel);
        void Update(CatalogoCiudadModel ciudadesModel);
        void Delete(int IdCiudades);
        IList<CatalogoCiudadModel> GetAllCity();
        CatalogoCiudadModel GetCity(int id);
        // Definir las operaciones de Servicio para estados:
        IList<CatalogoEstadoModel> GetAllStates();
        CatalogoEstadoModel GetState(int id);
        // Definir las operaciones de Servicio para municipios:
        IList<CatalogoMunicipioModel> GetAllTown();
        CatalogoMunicipioModel GetTown(int id);
    }
}
