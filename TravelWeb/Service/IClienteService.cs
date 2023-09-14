using TravelWeb.Models;
namespace TravelWeb.Service
{
    public interface IClienteService
    {
        //Definir operaciones de servicio para clientes:
        void Create(ClienteModel cliente);
        void Update(ClienteModel cliente);
        void Delete(int idCliente);
        IList<ClienteModel> GetAll();
        ClienteModel Get(int id);
        

        //Se define: tipo de retorno, nombre y parametros.
    }
}
