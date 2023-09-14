using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;

namespace TravelWeb.Data.Repository
{
    public class VentaRepository
    {
        TravelContext context;

        //Inyección del objeto
        public VentaRepository(TravelContext context)
        {
            this.context = context;
        }

        //Métodos del CRUD:
        public void Create(Venta venta)
        {
            context.Ventas.Add(venta);
            context.SaveChanges();
        }

        public Venta Get(int id)
        {
            Venta venta = context.Ventas.FirstOrDefault(e => e.Venta_ID == id);
            return venta;
        }

        public IList<Venta> GetAll()
        {
            IList<Venta> ventaList = context.Ventas.ToList();
            return ventaList;
        }
    }
}
