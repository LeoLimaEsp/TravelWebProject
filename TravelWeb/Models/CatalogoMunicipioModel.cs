using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Models
{
    public class CatalogoMunicipioModel
    {
        [Display(Name = "ID_Municipio")]
        public int id_municipio { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "ID_estado")]
        public int id_estado { get; set; }
    }
}
