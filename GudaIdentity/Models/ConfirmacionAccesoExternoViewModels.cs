using System.ComponentModel.DataAnnotations;

namespace GudaIdentity.Models
{
    public class ConfirmacionAccesoExternoViewModels
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
