using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class Habitacion
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Descripcion { get; set; }
    }
}
