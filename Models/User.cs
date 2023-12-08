using System.ComponentModel.DataAnnotations;

namespace WePhone.Models
{
    public class User
    {
        [Key]
        public string? User_Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public string? SDT { get; set; }
        public string? GPLX { get; set; }
        public string? Password { get; set; }
    }
}
