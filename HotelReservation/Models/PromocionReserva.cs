using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservation.Models
{
    public class PromocionReserva
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Idpromocion es obligatorio.")]
        public int Idpromocion { get; set; }
        [ForeignKey("Idpromocion")]
        public Promocion Promocion { get; set; }
        [Required(ErrorMessage = "El campo Idreserva es obligatorio.")]
        public int Idreserva { get; set; }
        [ForeignKey("Idreserva")]
        public Reserva Reserva { get; set; }
    }

}
