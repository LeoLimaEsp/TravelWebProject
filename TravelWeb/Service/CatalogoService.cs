using TravelWeb.Data.Infrastructure;
using TravelWeb.Domain;
using TravelWeb.Models;

namespace TravelWeb.Service
{
    public class CatalogoService : ICatalogoService
    {
        // 1.- Objecto que lo conecte con el repository: UnitOfWork
        IUnitOfWork uow;

        // Inyectarlo
        public CatalogoService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        //Operaciones CRUD
        public void Create(CatalogoCiudadModel ciudadesModel)
        {
            CatalogoCiudad ciudadesDomain = new CatalogoCiudad();
            // Transformar el Model en Domain
            ciudadesDomain.Nombre = ciudadesModel.Nombre;
            ciudadesDomain.Temperatura = ciudadesModel.Temperatura;

            // Hacer transacciones
            try
            {
                // Guardar el cliente
                uow.CatalogoCiudadRepository.Create(ciudadesDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public void Delete(int IdCiudades)
        {
            try
            {
                //Verificar si la ciudad participa en alguna venta, siendo asi no se puede borrar.
                CatalogoCiudad ciudad = uow.CatalogoCiudadRepository.Get(IdCiudades);
                IList<Viaje> ventasList = ciudad.Viajes.ToList();
                if (ventasList.Count > 0)
                {
                    throw new ApplicationException("¡Advertencia! la ciudad   " + ciudad.Nombre + " no se puede borrar porque se encuentra asociada a al menos un viaje.");
                }

                uow.CatalogoCiudadRepository.Delete(IdCiudades);
                uow.Commit();

            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
           
        }

        public CatalogoCiudadModel GetCity(int id)
        {
            try
            {
                CatalogoCiudad ciudadesDomain = uow.CatalogoCiudadRepository.Get(id);
                // Transformamos el Domain en Model (para poder retornarselo al controller)
                CatalogoCiudadModel ciudadesModel = new CatalogoCiudadModel()
                {
                    Ciudad_ID = ciudadesDomain.Ciudad_ID,
                    Nombre = ciudadesDomain.Nombre,
                    Temperatura = ciudadesDomain.Temperatura
                };

                return ciudadesModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public IList<CatalogoCiudadModel> GetAllCity()
        {
            try
            {
                var query = uow.CatalogoCiudadRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<CatalogoCiudadModel> listaCiudades = query.Select(c => new CatalogoCiudadModel()
                {
                    Ciudad_ID = c.Ciudad_ID,
                    Nombre = c.Nombre,
                    Temperatura = c.Temperatura
                }).ToList();

                return listaCiudades;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }



        public void Update(CatalogoCiudadModel ciudadeModel)
        {
            CatalogoCiudad ciudadeDomain = new CatalogoCiudad
            {
                // Transformar el Model en Domain
                Ciudad_ID = ciudadeModel.Ciudad_ID,
                Nombre = ciudadeModel.Nombre,
                Temperatura = ciudadeModel.Temperatura
            };
            // Hacer transacciones
            try
            {
                // Actualizar el cliente por medio del Repository
                uow.CatalogoCiudadRepository.Update(ciudadeDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        //--------------------------------------------------------------------------------------
        public IList<CatalogoEstadoModel> GetAllStates()
        {
            try
            {
                var query = uow.CatalogoEstadoRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<CatalogoEstadoModel> listaEstados = query.Select(c => new CatalogoEstadoModel()
                {
                    id_estado = c.id_estado,
                    nombre = c.nombre,
                }).ToList();

                return listaEstados;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public CatalogoEstadoModel GetState(int id)
        {
            try
            {
                CatalogoEstado estadoDomain = uow.CatalogoEstadoRepository.Get(id);

                // Transformamos el Domain en Model (para poder retornarselo al controller)
                CatalogoEstadoModel estadoModel = new CatalogoEstadoModel()
                {
                    id_estado = estadoDomain.id_estado,
                    nombre = estadoDomain.nombre
                };

                return estadoModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        //--------------------------------------------------------------------------------------

        public IList<CatalogoMunicipioModel> GetAllTown()
        {
            try
            {
                var query = uow.CatalogoMunicipioRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<CatalogoMunicipioModel> listaMunicipios = query.Select(c => new CatalogoMunicipioModel()
                {
                    id_municipio = c.id_municipio,
                    nombre = c.nombre,
                    id_estado = c.id_estado
                }).ToList();

                return listaMunicipios;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public CatalogoMunicipioModel GetTown(int id)
        {
            try
            {
                CatalogoMunicipio municipioDomain = uow.CatalogoMunicipioRepository.Get(id);

                // Transformamos el Domain en Model (para poder retornarselo al controller)
                CatalogoMunicipioModel municipioModel = new CatalogoMunicipioModel()
                {
                    id_estado = municipioDomain.id_estado,
                    nombre = municipioDomain.nombre,
                    id_municipio = municipioDomain.id_municipio
                };

                return municipioModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }
    }
}
