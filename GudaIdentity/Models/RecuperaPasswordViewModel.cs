using System.ComponentModel.DataAnnotations;


namespace GudaIdentity.Models
{
    public class RecuperaPasswordViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Pasword Obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage ="La confirmacion es obligatoria")]
        [Compare("Password",ErrorMessage ="La contraseña y confirmacion no coinciden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }    
    }
}
