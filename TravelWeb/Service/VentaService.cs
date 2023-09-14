
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;
using TravelWeb.Models;
using TravelWeb.Utils;

namespace TravelWeb.Service
{
    public class VentaService : IVentaService
    {
        // 1.- Objecto que lo conecte con el repository: UnitOfWork
        IUnitOfWork uow;

        // Inyectarlo
        public VentaService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Create(VentaModel ventaModel)
        {
            // Hacer transacciones
            try
            {
                // 1.- Calcular el monto total de venta = Precio del producto X Cantidad
                Viaje viaje = uow.ViajeRepository.Get(ventaModel.Viaje_ID);
                decimal montoVenta = viaje.Precio * ventaModel.Cantidad;
                

                // 2.- Restar la Cantidad vendida al Stock del Producto vendido.
                if (ventaModel.Cantidad == 0)
                    throw new ApplicationException("No puedes comprar 0 lugares, intenta de nuevo por favor.");
                if (viaje.Cupo_maximo == 0)
                    throw new ApplicationException("Lo sentimos, este viaje esta completo, no hay disponibilidad por el momento.");
                if (viaje.Cupo_maximo < ventaModel.Cantidad)
                    throw new ApplicationException("No fue posible realizar la compra por cantidad insuficiente de lugares, intente con una cantidad menor. \nLugares disponibles: " + viaje.Cupo_maximo);
                if (viaje.Disponibilidad == false)
                    throw new ApplicationException("No fue posible realizar la compra, viaje inhabilitado por el momento.");
                

                viaje.Cupo_maximo -= ventaModel.Cantidad;

                    Venta ventaDomain = new Venta();
                    // Transformar el Model en Domain
                    ventaDomain.Cliente_ID = ventaModel.Cliente_ID;
                    ventaDomain.Cantidad = ventaModel.Cantidad;
                    ventaDomain.Monto_venta = montoVenta;
                    ventaDomain.Fecha_venta = DateTime.Now;
                    ventaDomain.Viaje_ID = ventaModel.Viaje_ID;
                    
                    
                    

                // 3.- Persistir (GUARDAR) el ticket de Venta y ACTUALIZAR: guardar el cambio en Base de datos para Producto

                    uow.ViajeRepository.Update(viaje); // Actualizar la BDD.
                    uow.VentaRepository.Create(ventaDomain); //Se guarda el registro de la venta.

                    //Confirmar transacción.
                    uow.Commit();            
            }
            catch (Exception ex)
            {
                uow.Rollback();
                throw ex;
            }
        }

        public VentaModel Get(int id)
        {
            try
            {
                Venta ventaDomain = uow.VentaRepository.Get(id);
                // Transformamos el Domain en Model (para poder retornarselo al controller)
                VentaModel VentaModel = new VentaModel()
                {
                    Venta_ID = ventaDomain.Venta_ID,
                    Cliente_ID = ventaDomain.Cliente_ID,
                    Cliente_nombre = ventaDomain.cliente_ID.Nombre,
                    Cantidad = ventaDomain.Cantidad,
                    Monto_venta = ventaDomain.Monto_venta,
                    Fecha_venta = Util.DateToString(ventaDomain.Fecha_venta),
                    Viaje_ID = ventaDomain.Viaje_ID,
                    Viaje_nombre = ventaDomain.viaje_ID.Nombre,
                    
                };

                return VentaModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public VentaModel Get()
        {
            try
            {
                VentaModel ventaModel = new VentaModel();

                var clienteTipos = uow.ClienteRepository.GetAll();

                ventaModel.Clientes = clienteTipos.Select(t => new SelectListItem()
                {
                    Value = t.Cliente_ID.ToString(),
                    Text = t.Nombre
                }).ToList();

                var viajes = uow.ViajeRepository.GetAll();

                ventaModel.Viajes = viajes.Select(t => new SelectListItem()
                {
                    Value = t.Viaje_ID.ToString(),
                    Text = t.Nombre
                });

                return ventaModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public IList<VentaModel> GetAll()
        {
            try
            {
                var query = uow.VentaRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<VentaModel> listaVentas = query.Select(c => new VentaModel()
                {
                    Venta_ID = c.Venta_ID,
                    Viaje_ID = c.Viaje_ID,
                    Viaje_nombre = c.viaje_ID.Nombre,
                    Cliente_ID = c.Cliente_ID,
                    Cliente_nombre = c.cliente_ID.Nombre,
                    Cantidad = c.Cantidad,
                    Monto_venta = c.Monto_venta,
                    Fecha_venta = Util.DateToString(c.Fecha_venta)
                }).ToList();

                return listaVentas;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }
    }
}
