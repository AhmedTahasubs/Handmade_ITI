using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeITI.Core.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required, Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }
    }
}
