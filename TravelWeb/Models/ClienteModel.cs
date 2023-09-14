using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace TravelWeb.Models
{
    public class ClienteModel
    {
        [Display(Name = "ID:")]
        public int Cliente_ID { get; set; }


        [Display(Name = "Estado ID:")]
        public int Estado_ID { get; set; }


        [Display(Name = "Estado:")]
        public string nombreEstado { get; set; }



        [Display(Name = "Municipio ID:")]
        public int Municipio_ID { get; set; }



        [Display(Name = "Municipio:")]
        public string nombreMunicipio { get; set; }



        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }



        [Display(Name = "Dirección:")]
        public string Direccion { get; set; }



        [Display(Name = "Edad:")]
        public int Edad { get; set; }



        [Display(Name = "Telefono:")]
        public string Telefono { get; set; }



        [Display(Name = "Correo electronico:")]
        public string Email { get; set; }

        //Lista para mostrar historial de compras del cliente
        [Display(Name = "Compras del cliente:")]
        public IList<VentaModel> historialVentas { get; set; }


        public IEnumerable<SelectListItem> listaMunicipios { get; set; }
        public IEnumerable<SelectListItem> listaEstados { get; set; }
    }
}
