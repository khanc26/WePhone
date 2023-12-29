using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WePhone.Models
{
    public class ULogin
    {
        [Key] 
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        public bool KeepLoggedIn { get; set; }

        public ULogin(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
            this.KeepLoggedIn = false;
        }

        public ULogin() { }
    }
}