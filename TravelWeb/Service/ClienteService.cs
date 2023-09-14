using Microsoft.AspNetCore.Mvc.Rendering;
using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;
using TravelWeb.Models;
using TravelWeb.Utils;

namespace TravelWeb.Service
{
    public class ClienteService : IClienteService
    {
        //Contenido: objeto a conectar con repositories: UnitOfWork(uow).
        IUnitOfWork uow;

        //Inyección de uow:
        public ClienteService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Create(ClienteModel cliente)
        {
            Cliente clienteDomain = new Cliente();

            clienteDomain.Nombre = cliente.Nombre;
            clienteDomain.Estado_ID = cliente.Estado_ID;
            clienteDomain.Municipio_ID = cliente.Municipio_ID;
            clienteDomain.Direccion = cliente.Direccion;
            clienteDomain.Edad = cliente.Edad;
            clienteDomain.Email = cliente.Email;
            clienteDomain.Telefono = cliente.Telefono;

            //Transacción:
            try
            {
                //Guardar el cliente:
                uow.ClienteRepository.Create(clienteDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public void Delete(int idCliente)
        {
            try
            {
                //Verificar si el cliente participa en alguna venta, siendo asi no se puede borrar.
                Cliente cliente = uow.ClienteRepository.Get(idCliente);

                IList<Venta> ventasList = cliente.VentasCliente.ToList();
                if (ventasList.Count > 0)
                {
                    throw new ApplicationException("¡Advertencia!   " + cliente.Nombre + " no se puede borrar porque se encuentra asociado a al menos una venta.");
                }


                uow.ClienteRepository.Delete(idCliente);
                uow.Commit();

            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public ClienteModel Get(int id)
        {
            try
            {
                Cliente clienteDomain = uow.ClienteRepository.Get(id);
                // Transformamos el Domain en Model (para poder retornarselo al controller)
                ClienteModel clienteModel = new ClienteModel()
                {
                    Cliente_ID = clienteDomain.Cliente_ID,
                    Nombre = clienteDomain.Nombre,
                    Estado_ID = clienteDomain.Estado_ID,
                    Municipio_ID = clienteDomain.Municipio_ID,
                    Direccion = clienteDomain.Direccion,
                    Edad = clienteDomain.Edad,
                    Email = clienteDomain.Email,
                    Telefono = clienteDomain.Telefono,
                    nombreMunicipio = clienteDomain.municipio_ID.nombre,
                    nombreEstado = clienteDomain.estado_ID.nombre
                };

                //Se extrae y se asigna a una lista el historial de ventas hechas por un cliente.
                //Usando propiedades de navegación.
                clienteModel.historialVentas = clienteDomain.VentasCliente.Select(v => new VentaModel()
                {
                    Viaje_ID = v.Viaje_ID,
                    Monto_venta = v.Monto_venta,
                    Fecha_venta = Util.DateToString(v.Fecha_venta),
                    Cantidad = v.Cantidad,
                    Venta_ID = v.Venta_ID,
                    Viaje_nombre = v.viaje_ID.Nombre
                }).ToList();

                return clienteModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public IList<ClienteModel> GetAll()
        {
            try
            {
                var query = uow.ClienteRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<ClienteModel> listaClientes = query.Select(c => new ClienteModel()
                {
                    Cliente_ID = c.Cliente_ID,
                    Nombre = c.Nombre,
                    Estado_ID = c.Estado_ID,
                    Municipio_ID = c.Municipio_ID,
                    Direccion = c.Direccion,
                    Edad = c.Edad,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    nombreEstado = c.estado_ID.nombre,
                    nombreMunicipio = c.municipio_ID.nombre
                }).ToList();

                return listaClientes;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public void Update(ClienteModel cliente)
        {
            Cliente clienteDomain = new Cliente
            {
                // Transformar el Model en Domain
                Cliente_ID = cliente.Cliente_ID,   // <--- AQUI si se usa el Id
                Nombre = cliente.Nombre,
                Estado_ID = cliente.Estado_ID,
                Municipio_ID = cliente.Municipio_ID,
                Direccion = cliente.Direccion,
                Edad = cliente.Edad,
                Telefono = cliente.Telefono,
                Email = cliente.Email
            };
            // Hacer transacciones
            try
            {
                // Actualizar el cliente por medio del Repository
                uow.ClienteRepository.Update(clienteDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }
    }
}

