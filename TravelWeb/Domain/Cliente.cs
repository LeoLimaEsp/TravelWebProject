using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Domain
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key, Required]
        public int Cliente_ID { get; set; }
        [ForeignKey("estado_ID")]
        public int Estado_ID { get; set; }
        [ForeignKey("municipio_ID")]
        public int Municipio_ID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        //Propiedad de navegación de información requerida: catalogo de estados.
        public virtual CatalogoEstado estado_ID { get; set; }
        //Propiedad de navegación de información requerida: catalogo de municipios.
        public virtual CatalogoMunicipio municipio_ID { get; set; }
        //-----------------------------------------------------------------------.

        //Propiedad de navegación donde se requiere información de esta clase.
        public virtual ICollection<Venta> VentasCliente { get; set; }

        //La propiedad de navegación donde se requiere información de esta clase se debe iniciar en un constructor:
        public Cliente()
        {
            VentasCliente = new List<Venta>();
        }
    }
}
