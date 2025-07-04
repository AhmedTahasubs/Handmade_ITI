using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HandmadeITI.Core.Const;

namespace HandmadeITI.Core.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required, MaxLength(14)]
        public string NationalId { get; set; }

        [Required, Phone]
        public string MobileNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; } // Store hashed
        [NotMapped,Compare("Passowrd")]
        public string confirmPassword { get; set; } // For confirmation only, not stored    

        public bool IsBlackListed { get; set; }
        public Cart? Cart { get; set; }
        public List<Review> Reviews { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
    }
}
