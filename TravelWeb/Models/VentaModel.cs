using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Models
{
    public class VentaModel
    {
        public int Venta_ID { get; set; }

        public int Viaje_ID { get; set; }

        [Display(Name = "Nombre del cliente")]
        public string Cliente_nombre { get; set; }

        [Display(Name = "Nombre del viaje")]
        public string Viaje_nombre { get; set; }

        public int Cliente_ID { get; set; }

        [Display(Name = "Número de pasajeros")]
        public int Cantidad { get; set; }

        [Display(Name = "Monto total")]
        public decimal Monto_venta { get; set; }

        [Display(Name = "Fecha de venta")]
        public string Fecha_venta { get; set; }
        public decimal adeudo { get; set; }

        // Lista de Items (parejas: valor-texto)
        public IEnumerable<SelectListItem> Viajes { get; set; }
        public IEnumerable<SelectListItem> Clientes { get; set; }
    }
}
