using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservation.Models
{
    public class ServicioHabitacion
    {
        [Key]
        public int Id { get; set; }
        public bool Estado { get; set; }
        [Required(ErrorMessage = "El campo Idservicio es obligatorio.")]
        [DisplayName("Servicio")]
        public int Idservicio { get; set; }
        [ForeignKey("Idservicio")]
        public Servicio Servicio { get; set; }
    }
}
