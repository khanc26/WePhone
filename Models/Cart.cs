using System.ComponentModel.DataAnnotations.Schema;

namespace WePhone.Models
{
    [Table("cart")]
    public class Cart
    {
            public int Id { get; set; }

            public int User_Id { get; set; }
            [ForeignKey("User_Id")]
            public User? User { get; set; }

            public int Phone_Id { get; set; }
            [ForeignKey("Phone_Id")]
            public Smartphone? Smartphone { get; set; }

            public int Quantity { get; set; }
    }
}
