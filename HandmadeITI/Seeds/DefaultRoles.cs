using HandmadeITI.Core.Const;
using Microsoft.AspNetCore.Identity;

namespace HandmadeITI.Seeds
{
	public static class DefaultRoles
	{
		public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.Roles.Any())
			{
				await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
				await roleManager.CreateAsync(new IdentityRole(AppRoles.Seller));
				await roleManager.CreateAsync(new IdentityRole(AppRoles.Customer));
			}
		}
	}
}
