using HandmadeITI.Core.Const;
using HandmadeITI.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeITI.Seeds
{
	public class DefaultUsers
	{
		public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
		{
			var admin = new ApplicationUser()
			{
				UserName = "admin",
				Email = "admin@handmade.com",
				FullName = "Admin",
				EmailConfirmed = true
			};

			var user = await userManager.FindByNameAsync(admin.UserName);

			if(user == null)
			{
				await userManager.CreateAsync(admin, "123456Aa@");
				await userManager.AddToRoleAsync(admin,AppRoles.Admin);
			}

		}
	}
}
