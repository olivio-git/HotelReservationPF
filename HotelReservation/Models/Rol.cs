using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo 50 caracteres.")]
        public string Nombre { get; set; }
    }

}
