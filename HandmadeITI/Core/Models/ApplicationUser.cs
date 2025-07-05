using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HandmadeITI.Core.Models
{
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(100)]
		public string FullName { get; set; } = null!;
		public bool IsDeleted { get; set; }
		public string? CreatedById { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public string? LastUpdatedByid { get; set; }
		public DateTime LastUpdatedOn { get; set; }
	}
}
