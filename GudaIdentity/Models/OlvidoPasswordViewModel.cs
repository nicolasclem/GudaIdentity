using System.ComponentModel.DataAnnotations;

namespace GudaIdentity.Models
{
    public class OlvidoPasswordViewModel
    {
        [Required(ErrorMessage ="El email es obligatorio")]
        [EmailAddress]
        public string  Email { get; set; }
    }
}
