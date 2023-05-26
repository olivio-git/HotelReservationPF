using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class Promocion
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Titulo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Titulo debe tener como máximo 50 caracteres.")]
        public string Titulo { get; set; }
        [StringLength(200, ErrorMessage = "El campo Descripcion debe tener como máximo 200 caracteres.")]
        public string Descripcion { get; set; }
    }

}
