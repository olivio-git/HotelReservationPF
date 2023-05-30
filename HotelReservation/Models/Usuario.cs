using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Net.Mail;

namespace HotelReservation.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [DisplayName("Correo")]
        [EmailAddress(ErrorMessage = "El campo Correo debe ser una dirección de correo electrónico válida.")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
        [DisplayName("Imagen")]
        public string Imagen { get; set; }
        public int ReservaExitosa { get; set; }
        [Required(ErrorMessage = "El campo Idpersona es obligatorio.")]
        [DisplayName("Persona")]
        public int Idpersona { get; set; }
        [ForeignKey("Idpersona")]
        public Persona Persona { get; set; }
        [Required(ErrorMessage = "El campo Idrol es obligatorio.")]
        [DisplayName("Rol")]
        public int Idrol { get; set; }
        [ForeignKey("Idrol")]
        public Rol Rol { get; set; }
        [NotMapped]
        [Display(Name = "Imagen")]
        public IFormFile IMGarchivo { get; set; }
    }

}
