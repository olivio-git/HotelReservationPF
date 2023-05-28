using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservation.Models
{
    public class Habitacion
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Codigo es obligatorio.")]
        [DisplayName("Código")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El campo Codigo debe tener entre 3 y 20 caracteres.")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo Imagen es obligatorio.")]
        [DisplayName("Imagen")]
        public string Imagen { get; set; }
        public bool Estado { get; set; }
        [Range(1, 5, ErrorMessage = "El campo Calificacion debe estar entre 1 y 5.")]
        public int Calificacion { get; set; }
        [Required(ErrorMessage = "El campo Idtipo es obligatorio.")]
        [DisplayName("Tipo")]
        public int Idtipo { get; set; }
        [ForeignKey("Idtipo")]
        public Tipo tipo { get; set; }
        [NotMapped]
        [Display(Name = "Imagen")]
        public IFormFile IMGarchivo { get; set; }
    }
}