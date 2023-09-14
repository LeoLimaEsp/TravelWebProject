using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Domain;

namespace TravelWeb.Data.Infrastructure
{
    public class TravelContext : IdentityDbContext
    {
        //Constructor para la cadena de conexión.
        public TravelContext(DbContextOptions<TravelContext> options) : base(options)
        {

        }

        //DbSet para cada entidad del domain.
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<CatalogoMunicipio> CatalogoDeMunicipios { get; set; }
        public DbSet<CatalogoEstado> CatalogoDeEstados { get; set; }
        public DbSet<CatalogoCiudad> CatalogoDeCiudades { get; set; }

        // 4. Esta configuración nos permite extraer datos entre entidades (clases del dominio) que esten relacionadas
        //    mediante propiedades de navegación.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
