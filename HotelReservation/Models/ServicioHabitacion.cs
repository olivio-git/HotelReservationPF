using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

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

        [Required(ErrorMessage = "El campo Idtipo es obligatorio.")]
        [DisplayName("Idtipo")]
        public int Idtipo { get; set; }
        [ForeignKey("Idtipo")]
        public Tipo tipo { get; set; }
    }
}
