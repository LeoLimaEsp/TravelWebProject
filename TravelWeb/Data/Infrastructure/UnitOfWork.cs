using Microsoft.EntityFrameworkCore;
using TravelWeb.Data.Repository;


namespace TravelWeb.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        // App Context.
        TravelContext context;

        //Inyección de context
        public UnitOfWork(TravelContext context)
        {
            this.context = context;
        }

        //Propiedades de acceso. 
        public ClienteRepository clienteRepository;
        public ViajeRepository viajeRepository;
        public VentaRepository ventaRepository;
        public CatalogoMunicipioRepository catalogoMunicipioRepository;
        public CatalogoEstadoRepository catalogoEstadoRepository;
        public CatalogoCiudadRepository catalogoCiudadRepository;


        //Repositories.
        public ClienteRepository ClienteRepository
        {
            get { return clienteRepository = clienteRepository ?? new ClienteRepository(context); }
        }

        public VentaRepository VentaRepository
        {
            get { return ventaRepository = ventaRepository ?? new VentaRepository(context); }
        }

        public ViajeRepository ViajeRepository
        {
            get { return viajeRepository = viajeRepository ?? new ViajeRepository(context); }
        }

        public CatalogoMunicipioRepository CatalogoMunicipioRepository
        {
            get { return catalogoMunicipioRepository = catalogoMunicipioRepository ?? new CatalogoMunicipioRepository(context); }
        }

        public CatalogoEstadoRepository CatalogoEstadoRepository
        {
            get { return catalogoEstadoRepository = catalogoEstadoRepository ?? new CatalogoEstadoRepository(context); }
        }

        public CatalogoCiudadRepository CatalogoCiudadRepository
        {
            get { return catalogoCiudadRepository = catalogoCiudadRepository ?? new CatalogoCiudadRepository(context); }
        }

        //Métodos de transacción.
        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {

                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                }
            }
            context.Dispose();
        }
    }
}
