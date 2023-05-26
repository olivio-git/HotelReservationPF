using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Subtotal")]
        public float Subtotal { get; set; }
        [Required(ErrorMessage = "El campo fecha_in es obligatorio.")]
        [DisplayName("Fecha de entrada")]
        public DateTime FechaIn { get; set; }
        [Required(ErrorMessage = "El campo fecha_out es obligatorio.")]
        [DisplayName("Fecha de salida")]
        public DateTime FechaOut { get; set; }
        [Required(ErrorMessage = "El campo Idusuario es obligatorio.")]
        [DisplayName("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [Required(ErrorMessage = "El campo idhabitacion es obligatorio.")]
        [DisplayName("Habitación")]
        public int IdHabitacion { get; set; }
        [ForeignKey("IdHabitacion")]
        public Habitacion Habitacion { get; set; }
    }

}
