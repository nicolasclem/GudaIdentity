using System.ComponentModel.DataAnnotations;

namespace GudaIdentity.Models
{
    public class AccesoViewModel
    {
        [Required(ErrorMessage ="El email es obligatorio")]
        [EmailAddress]
        public string  Email { get; set; }


        [Required(ErrorMessage ="Pasword Obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name ="Recordame")]
        public bool RemmemberMe { get; set; }


    }

}
