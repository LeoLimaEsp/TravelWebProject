using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;
namespace TravelWeb.Data.Repository
{
    public class ViajeRepository
    {
        TravelContext context;

        //Inyección del objeto
        public ViajeRepository(TravelContext context)
        {
            this.context = context;
        }

        //Métodos del CRUD:
        public void Create(Viaje viaje)
        {
            context.Viajes.Add(viaje);
            context.SaveChanges();
        }

        public Viaje Get(int id)
        {
            Viaje viaje = context.Viajes.FirstOrDefault(e => e.Viaje_ID == id);
            return viaje;
        }

        public IList<Viaje> GetAll()
        {
            IList<Viaje> viajeList = context.Viajes.ToList();
            return viajeList;
        }

        public void Update(Viaje nuevo)
        {
            Viaje oldProducto = Get(nuevo.Viaje_ID);
            oldProducto.Nombre = nuevo.Nombre;
            oldProducto.Ciudad_ID = nuevo.Ciudad_ID;
            oldProducto.Foto = nuevo.Foto;
            oldProducto.Precio = nuevo.Precio;
            oldProducto.Descripcion = nuevo.Descripcion;
            oldProducto.Cupo_maximo = nuevo.Cupo_maximo;
            oldProducto.Disponibilidad = nuevo.Disponibilidad;
            oldProducto.Fecha_inicio = nuevo.Fecha_inicio;
            oldProducto.Fecha_fin = nuevo.Fecha_fin;
            //oldProducto.Fecha_alta_viaje = nuevo.Fecha_alta_viaje;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Viaje viajeBorrar = Get(id);
            context.Viajes.Remove(viajeBorrar);
            context.SaveChanges();
        }
    }
}
