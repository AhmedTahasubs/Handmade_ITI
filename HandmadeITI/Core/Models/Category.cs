using System.ComponentModel.DataAnnotations;

namespace HandmadeITI.Core.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
