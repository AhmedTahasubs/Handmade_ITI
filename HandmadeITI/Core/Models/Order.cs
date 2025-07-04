using HandmadeITI.Core.Const;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeITI.Core.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ValidateNever]

        public User? User { get; set; }

        [Required, MaxLength(250)]
        public string ShippingAddress { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
