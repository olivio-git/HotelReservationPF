using Microsoft.Build.Graph;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace HotelReservation.Models
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo 50 caracteres.")]
        public string Nombre { get; set; }
        [StringLength(200, ErrorMessage = "El campo Descripcion debe tener como máximo 200 caracteres.")]
        public string Descripcion { get; set; }
        [StringLength(50, ErrorMessage = "El campo Categoría debe tener como máximo 50 caracteres.")]
        public string Categoria { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "El campo Precio debe ser un valor numérico válido.")]
        public float Precio { get; set; }
        [Required(ErrorMessage = "El campo Disponibilidad es obligatorio.")]
        public int Disponibilidad { get; set; }
    }
}
