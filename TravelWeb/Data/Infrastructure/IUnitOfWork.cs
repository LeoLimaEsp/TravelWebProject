using TravelWeb.Data.Repository;

namespace TravelWeb.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        //Registro de repositories en forma de propiedad GET:
        ClienteRepository ClienteRepository { get; }
        ViajeRepository ViajeRepository { get; }
        VentaRepository VentaRepository { get; }
        CatalogoMunicipioRepository CatalogoMunicipioRepository { get; }
        CatalogoEstadoRepository CatalogoEstadoRepository { get; }
        CatalogoCiudadRepository CatalogoCiudadRepository { get; }

        // 2.- Métodos para transacciones
        void Commit();
        void Rollback();
    }
}
