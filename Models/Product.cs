using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WePhone.Models
{
    [Table("smartphones")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        public int? Ram { get; set; }
        [Required]
        public int? Rom { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public string? Color { get; set; }
    }
}
