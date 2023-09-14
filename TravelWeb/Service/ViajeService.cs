using Microsoft.AspNetCore.Mvc.Rendering;
using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;
using TravelWeb.Models;
using TravelWeb.Utils;

namespace TravelWeb.Service
{
    public class ViajeService : IViajeService
    {
        // 1.- Objecto que lo conecte con el repository: UnitOfWork
        IUnitOfWork uow;

        // Inyectarlo
        public ViajeService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // 2.- Implementar todos los métodos descritos en la Interfaz
        public void Create(ViajeModel viaje)
        {
            Viaje ViajeDomain = new Viaje();
            // Transformar el Model en Domain
            //ViajeDomain.Viaje_ID = viaje.Viaje_ID;
            ViajeDomain.Ciudad_ID = viaje.Ciudad_ID;
            ViajeDomain.Nombre = viaje.Nombre;
            ViajeDomain.Foto = viaje.Foto;
            ViajeDomain.Descripcion = viaje.Descripcion;
            ViajeDomain.Cupo_maximo = viaje.Cupo_maximo;
            ViajeDomain.Disponibilidad = viaje.Disponibilidad;
            ViajeDomain.Fecha_inicio = viaje.Fecha_inicio;
            ViajeDomain.Fecha_fin = viaje.Fecha_fin;
            ViajeDomain.Precio = viaje.Precio;
            ViajeDomain.Fecha_alta_viaje = DateTime.Now;



            // Hacer transacciones
            try
            {
                // Guardar el cliente
                uow.ViajeRepository.Create(ViajeDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public void Delete(int idviaje)
        {
            try
            {
                //Verificar si el viaje participa en alguna venta, siendo asi no se puede borrar.
                Viaje viaje = uow.ViajeRepository.Get(idviaje);

                IList<Venta> ventasList = viaje.VentasViaje.ToList();
                if (ventasList.Count > 0)
                {
                    throw new ApplicationException("¡Advertencia! El viaje no se puede borrar porque se encuentra asociado a al menos una venta.");
                }

                uow.ViajeRepository.Delete(idviaje);
                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                throw ex;
            }
        }

        public ViajeModel Get(int id)
        {
            try
            {
                Viaje viajeDomain = uow.ViajeRepository.Get(id);
                // Transformamos el Domain en Model (para poder retornarselo al controller)
                ViajeModel viajeModel = new ViajeModel()
                {
                    Viaje_ID = viajeDomain.Viaje_ID,
                    Nombre = viajeDomain.Nombre,
                    Ciudad_ID = viajeDomain.Ciudad_ID,
                    Ciudad = viajeDomain.ciudad_nombre.Nombre,
                    Foto = viajeDomain.Foto,
                    Descripcion = viajeDomain.Descripcion,
                    Cupo_maximo = viajeDomain.Cupo_maximo,
                    Disponibilidad = viajeDomain.Disponibilidad,
                    Fecha_inicio = viajeDomain.Fecha_inicio,
                    Fecha_fin = viajeDomain.Fecha_fin,
                    Precio = viajeDomain.Precio,
                    Fecha_alta_viaje = Util.DateToString(viajeDomain.Fecha_alta_viaje),
                    Temperatura = viajeDomain.ciudad_nombre.Temperatura
                };


                //Lista de ciudades para editar se muestre la lista
                var listaciudades = uow.CatalogoCiudadRepository.GetAll();

                viajeModel.ciudades_destino = listaciudades.Select(t => new SelectListItem()
                {
                    Value = t.Ciudad_ID.ToString(),
                    Text = t.Nombre,
                    Selected = t.Ciudad_ID == viajeDomain.Ciudad_ID
                }).ToList();

                return viajeModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public ViajeModel Get()
        {
            try
            {
                ViajeModel viajeModel = new ViajeModel();

                var listaCiudades = uow.CatalogoCiudadRepository.GetAll();

                viajeModel.ciudades_destino = listaCiudades.Select(t => new SelectListItem()
                {
                    Value = t.Ciudad_ID.ToString(),
                    Text = t.Nombre
                }).ToList();

                return viajeModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public IList<ViajeModel> GetAllViajes()
        {
            try
            {
                IList<Viaje> viajesDomain = uow.ViajeRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<ViajeModel> listaViajes = viajesDomain.Select(p => new ViajeModel()
                {
                    Viaje_ID = p.Viaje_ID,
                    Ciudad_ID = p.Ciudad_ID,
                    Ciudad = p.ciudad_nombre.Nombre,
                    Nombre = p.Nombre,
                    Foto = p.Foto,
                    Descripcion = p.Descripcion,
                    Cupo_maximo = p.Cupo_maximo,
                    Disponibilidad = p.Disponibilidad,
                    Fecha_inicio = p.Fecha_inicio,
                    Fecha_fin = p.Fecha_fin,
                    Precio = p.Precio,
                    Fecha_alta_viaje = Util.DateToString(p.Fecha_alta_viaje),
                    Temperatura = p.ciudad_nombre.Temperatura
                }).ToList();

                return listaViajes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(ViajeModel viaje)
        {
            try
            {
                Viaje viajee = uow.ViajeRepository.Get(viaje.Viaje_ID);
                //Verificar si el viaje participa en alguna venta, siendo asi no se puede editar.
                IList<Venta> ventasList = viajee.VentasViaje.ToList();

                if (ventasList.Count > 0 && viaje.Cupo_maximo >= ventasList.Count)
                {
                    Viaje viajeDomain = new Viaje();
                    // Transformar el Model en Domain
                    viajeDomain.Viaje_ID = viaje.Viaje_ID;
                    viajeDomain.Ciudad_ID = viajee.Ciudad_ID;
                    viajeDomain.Nombre = viajee.Nombre;
                    viajeDomain.Foto = viaje.Foto;
                    viajeDomain.Descripcion = viajee.Descripcion;
                    viajeDomain.Cupo_maximo = viaje.Cupo_maximo;
                    viajeDomain.Disponibilidad = viaje.Disponibilidad;
                    viajeDomain.Fecha_inicio = viajee.Fecha_inicio;
                    viajeDomain.Fecha_fin = viajee.Fecha_fin;
                    viajeDomain.Precio = viaje.Precio;
                    //viajeDomain.Fecha_alta_viaje = DateTime.Now;

                    uow.ViajeRepository.Update(viajeDomain);
                    uow.Commit();
                }

                if (ventasList.Count == 0 && viaje.Cupo_maximo > ventasList.Count)
                {
                    Viaje viajeDomain = new Viaje();
                    // Transformar el Model en Domain
                    viajeDomain.Viaje_ID = viaje.Viaje_ID;
                    viajeDomain.Ciudad_ID = viaje.Ciudad_ID;
                    viajeDomain.Nombre = viaje.Nombre;
                    viajeDomain.Foto = viaje.Foto;
                    viajeDomain.Descripcion = viaje.Descripcion;
                    viajeDomain.Cupo_maximo = viaje.Cupo_maximo;
                    viajeDomain.Disponibilidad = viaje.Disponibilidad;
                    viajeDomain.Fecha_inicio = viaje.Fecha_inicio;
                    viajeDomain.Fecha_fin = viaje.Fecha_fin;
                    viajeDomain.Precio = viaje.Precio;
                    //viajeDomain.Fecha_alta_viaje = DateTime.Now;

                    uow.ViajeRepository.Update(viajeDomain);
                    uow.Commit();
                }

                if (viaje.Cupo_maximo < ventasList.Count && viaje.Disponibilidad == false)
                {
                    Viaje viajeDomain = new Viaje();
                    // Transformar el Model en Domain
                    viajeDomain.Viaje_ID = viaje.Viaje_ID;
                    viajeDomain.Ciudad_ID = viajee.Ciudad_ID;
                    viajeDomain.Nombre = viajee.Nombre;
                    viajeDomain.Foto = viajee.Foto;
                    viajeDomain.Descripcion = viajee.Descripcion;
                    viajeDomain.Cupo_maximo = viajee.Cupo_maximo;
                    viajeDomain.Disponibilidad = viaje.Disponibilidad;
                    viajeDomain.Fecha_inicio = viajee.Fecha_inicio;
                    viajeDomain.Fecha_fin = viajee.Fecha_fin;
                    viajeDomain.Precio = viajee.Precio;
                    //viajeDomain.Fecha_alta_viaje = DateTime.Now;

                    uow.ViajeRepository.Update(viajeDomain);
                    uow.Commit();
                }

                if (viaje.Cupo_maximo < ventasList.Count && viaje.Disponibilidad == true)
                {
                    throw new ApplicationException("El cupo máximo de personas no puede ser menor al número de personas que ya compraron el viaje, lugares comprados: " + ventasList.Count);
                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }

        }

        
        public IList<ViajeModel> Filtrar(DateTime ini, DateTime fin)
        {          
            //var fechaDomainIni = Util.StringToDate(ini);
            //var fechaDomainFin = Util.StringToDate(fin);

            IList<Viaje> viajesDomain = uow.ViajeRepository.GetAll();
            IList<Viaje> viajesFiltrados = viajesDomain.Where(m => m.Fecha_inicio >= ini && m.Fecha_fin <= fin).ToList();
   
            IList<ViajeModel> listaViajesFiltrada = viajesFiltrados.Select(p => new ViajeModel()
            {
                Viaje_ID = p.Viaje_ID,
                Ciudad_ID = p.Ciudad_ID,
                Ciudad = p.ciudad_nombre.Nombre,
                Nombre = p.Nombre,
                Foto = p.Foto,
                Descripcion = p.Descripcion,
                Cupo_maximo = p.Cupo_maximo,
                Disponibilidad = p.Disponibilidad,
                Fecha_inicio = p.Fecha_inicio,
                Fecha_fin = p.Fecha_fin,
                Precio = p.Precio,
                Fecha_alta_viaje = Util.DateToString(p.Fecha_alta_viaje),
                Temperatura = p.ciudad_nombre.Temperatura
            }).ToList();

            return listaViajesFiltrada;
        }
    }
}


//**************************************************************************************************************************