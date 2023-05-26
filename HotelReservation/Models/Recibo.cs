using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace HotelReservation.Models
{
    public class Recibo
    {
        [Key]
        public int Id { get; set; }
        public float Total { get; set; }
        [Required(ErrorMessage = "El campo Idreserva es obligatorio.")]
        public int Idreserva { get; set; }
        [ForeignKey("Idreserva")]
        public Reserva Reserva { get; set; }
    }

}
