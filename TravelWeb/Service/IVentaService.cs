using TravelWeb.Models;

namespace TravelWeb.Service
{
    public interface IVentaService
    {
        // Definir las operaciones de Servicio:
        void Create(VentaModel venta);
        IList<VentaModel> GetAll();
        VentaModel Get(int id);
        VentaModel Get();
    }
}
