using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;
namespace TravelWeb.Data.Repository
{
    public class ClienteRepository
    {
        TravelContext context;

        //Inyección del objeto
        public ClienteRepository(TravelContext context)
        {
            this.context = context;
        }

        //Métodos del CRUD:
        public void Create(Cliente cliente)
        {
            //Agregar de domain a Clientes(context)
            context.Clientes.Add(cliente);
            context.SaveChanges();
        }

        public IQueryable<Cliente> GetAll()
        {
            var clienteList = context.Clientes.Select(c => c);
            return clienteList;
        }

        public Cliente Get(int id)
        {
            Cliente cliente = context.Clientes.FirstOrDefault(e => e.Cliente_ID == id);
            return cliente;
        }

        public void Update(Cliente nuevo)
        {
            Cliente oldCliente = Get(nuevo.Cliente_ID);
            oldCliente.Nombre = nuevo.Nombre;
            oldCliente.Estado_ID = nuevo.Estado_ID;
            oldCliente.Municipio_ID = nuevo.Municipio_ID;
            oldCliente.Edad = nuevo.Edad;
            oldCliente.Direccion = nuevo.Direccion;
            oldCliente.Telefono = nuevo.Telefono;
            oldCliente.Email = nuevo.Email;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Cliente clienteBorrar = Get(id);
            context.Clientes.Remove(clienteBorrar);
        }
    }
}
