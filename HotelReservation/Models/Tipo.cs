using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HotelReservation.Models
{
    public class Tipo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [DisplayName("Nombre")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo 50 caracteres.")]
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        [StringLength(200, ErrorMessage = "El campo Descripción debe tener como máximo 200 caracteres.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [DisplayName("Precio")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Precio debe ser un valor numérico válido.")]
        public float Precio { get; set; }
    }
}
