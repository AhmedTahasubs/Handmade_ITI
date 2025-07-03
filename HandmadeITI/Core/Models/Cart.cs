using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeITI.Core.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

       
        public int UserId { get; set; }
        public User User { get; set; }

        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalPrice { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
