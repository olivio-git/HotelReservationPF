using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace HotelReservation.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Ci es obligatorio.")]
        [StringLength(10, ErrorMessage = "El campo Ci debe tener como máximo 10 caracteres.")]
        public string Ci { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo 50 caracteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Apellido debe tener como máximo 50 caracteres.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        [StringLength(20, ErrorMessage = "El campo Teléfono debe tener como máximo 20 caracteres.")]
        [Phone(ErrorMessage = "El campo Teléfono debe ser un número de teléfono válido.")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo Nacionalidad es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nacionalidad debe tener como máximo 50 caracteres.")]
        public string Nacionalidad { get; set; }
    }
}
