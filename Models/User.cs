using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WePhone.Models
{
    public class ULogin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Full_Name")] // Map to the 'Full_Name' column in the table
        public string FullName { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(255)]
        [Column("Phone_Number")]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        [Column("Province_City")] // Map to the 'Province_City' column in the table
        public string ProvinceCity { get; set; }

        [MaxLength(255)]
        public string District { get; set; }

        [MaxLength(255)]
        public string Ward { get; set; }

        [MaxLength(255)]
        [Column("Specific_Address")] // Map to the 'Specific_Address' column in the table
        public string SpecificAddress { get; set; }


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