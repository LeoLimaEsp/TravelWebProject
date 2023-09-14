using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Domain
{
    [Table("Ciudad")]
    public class CatalogoCiudad
    {
        [Key, Required]
        public int Ciudad_ID { get; set; }
        public string Nombre { get; set; }
        public int Temperatura { get; set; }

        //Propiedad de navegación donde se requiere información de esta clase.
        public virtual ICollection<Viaje> Viajes { get; set; }

        //La propiedad de navegación donde se requiere información de esta clase se debe iniciar en un constructor:
        public CatalogoCiudad()
        {
            Viajes = new List<Viaje>();
        }
    }
}
