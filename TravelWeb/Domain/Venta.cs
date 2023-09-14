using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Domain
{
    [Table("Venta")]
    public class Venta
    {
        [Key, Required]
        public int Venta_ID { get; set; }
        [ForeignKey("viaje_ID")]
        public int Viaje_ID { get; set; }
        [ForeignKey("cliente_ID")]
        public int Cliente_ID { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto_venta { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Fecha_venta { get; set; }

        //Propiedad de navegación de información requerida: viaje.
        public virtual Viaje viaje_ID { get; set; }
        //Propiedad de navegación de información requerida: cliente.
        public virtual Cliente cliente_ID { get; set; }

        
        
    }
}
