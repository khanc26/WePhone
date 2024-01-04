using System.ComponentModel.DataAnnotations;

namespace WePhone.Models
{
    public class ForgetPass
    {
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }

    
}
