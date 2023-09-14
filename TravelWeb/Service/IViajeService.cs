using TravelWeb.Models;

namespace TravelWeb.Service
{
    public interface IViajeService
    {
        // Definir las operaciones de Servicio:
        void Create(ViajeModel viaje);
        void Update(ViajeModel viaje);
        void Delete(int idviaje);
        IList<ViajeModel> GetAllViajes();
        ViajeModel Get(int id);
        ViajeModel Get();
        IList<ViajeModel> Filtrar(DateTime ini, DateTime fin);

    }
}
