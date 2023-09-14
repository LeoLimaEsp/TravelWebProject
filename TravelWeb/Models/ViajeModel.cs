using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelWeb.Models
{
    public class ViajeModel
    {
        public int Viaje_ID { get; set; }

        [Display(Name = "Ciudades destino")]
        public int Ciudad_ID { get; set; }

        public string Ciudad { get; set; }

        [Display(Name = "Temperatura actual de la ciudad")]
        public int Temperatura { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Nombre del viaje")]
        public string Nombre { get; set; }

        public string Foto { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Lugares disponibles")]
        public int Cupo_maximo { get; set; }

        public bool Disponibilidad { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime Fecha_inicio { get; set; }

        [Display(Name = "Fecha de fin")]
        public DateTime Fecha_fin { get; set; }

        public decimal Precio { get; set; }

        [Display(Name = "Fecha alta de viaje")]
        public string Fecha_alta_viaje { get; set; }

        // Lista de Items (parejas: valor-texto)
        public IEnumerable<SelectListItem> ciudades_destino { get; set; }
    }
}
