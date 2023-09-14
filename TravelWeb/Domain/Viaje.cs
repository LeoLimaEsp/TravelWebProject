
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Domain
{
    [Table("Viaje")]
    public class Viaje
    {
        [Key, Required]
        public int Viaje_ID { get; set; }

        public string Nombre { get; set; }

        [ForeignKey("ciudad_nombre")]
        public int Ciudad_ID { get; set; }
        
        public string Foto { get; set; }
        public string Descripcion { get; set; }
        public int Cupo_maximo { get; set; }
        public bool Disponibilidad { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha_inicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha_fin { get; set; }
        public decimal Precio { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha_alta_viaje { get; set; }

        //Propiedad de navegación de información requerida: catalogo de ciudades.
        public virtual CatalogoCiudad ciudad_nombre { get; set; }

        //________________________________________________________________________________.

        //Propiedad de navegación donde se requiere información de esta clase.
        public virtual ICollection<Venta> VentasViaje { get; set; }

        //La propiedad de navegación donde se requiere información de esta clase se debe iniciar en un constructor:
        public Viaje()
        {
            VentasViaje = new List<Venta>();
        }
    }
}
