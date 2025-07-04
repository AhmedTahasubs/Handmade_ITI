using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeITI.Core.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}
