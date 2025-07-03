using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeITI.Core.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool IsApproved { get; set; }

        public List<string> ImageUrls { get; set; } = new();

        [Required]
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public User Seller { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Review> Reviews { get; set; } = new();
    }
}
