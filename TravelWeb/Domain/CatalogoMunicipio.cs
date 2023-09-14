using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Domain
{
    [Table("municipios")]
    public class CatalogoMunicipio
    {
        [Key, Required]
        [Column("id")]
        public int id_municipio { get; set; }
        public string nombre { get; set; }
        [ForeignKey("idEstado")]
        public int id_estado { get; set; }
        //Propiedad de navegación de información requerida: catalogo de estados.
        public virtual CatalogoEstado idEstado { get; set; }

        //________________________________________________________________________________.

        //Propiedad de navegación donde se requiere información de esta clase.
        public virtual ICollection<Cliente> ClientesMunicipio { get; set; }

        //La propiedad de navegación donde se requiere información de esta clase se debe iniciar en un constructor:
        public CatalogoMunicipio()
        {
            ClientesMunicipio = new List<Cliente>();
        }
    }
}
