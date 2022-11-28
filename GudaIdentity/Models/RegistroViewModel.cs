using System.ComponentModel.DataAnnotations;

namespace GudaIdentity.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage ="El email es obligatorio")]
        [EmailAddress]
        public string  Email { get; set; }


        [Required(ErrorMessage ="Pasword Obligatorio")]
        [StringLength(30,ErrorMessage ="el {0} debe estar  al menos {2} caracteres de longuitud",MinimumLength =5)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmacion de Contraseña  requerida")]
        [Compare("Password",ErrorMessage ="La contraseña y confirmacion  no coinciden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfrimPassword { get; set; }

        [Required(ErrorMessage ="Nombre es obligatorio")]
        public string Nombre { get; set; }

        public string Url { get; set; }
        public int CodigoPais { get; set; }
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Pais es obligatorio")]
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "la fecha  es obligatorio")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Estado obligatorio")]
        public bool Estado { get; set; }
    }

}
