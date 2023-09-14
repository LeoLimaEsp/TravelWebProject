using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Domain
{
    [Table("estados")]
    public class CatalogoEstado
    {
        [Key, Required]
        [Column("id")]
        public int id_estado { get; set; }
        public string nombre { get; set; }

        //Propiedad de navegación donde se requiere información de esta clase.
        public virtual ICollection<Cliente> ClientesEstado { get; set; }

        //La propiedad de navegación donde se requiere información de esta clase se debe iniciar en un constructor:
        public CatalogoEstado()
        {
            ClientesEstado = new List<Cliente>();
        }
    }
}
