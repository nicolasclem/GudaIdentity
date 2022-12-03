using System.ComponentModel.DataAnnotations;

namespace GudaIdentity.Models
{
    public class AutenticacionDosFactoresViewModel
    {
        //para el login 
        [Required]
        [Display(Name = "Codigo del autenticador")]
        public string Code { get; set; }

        //para el token------- registro
        public string  Token { get; set; }
    }
}
