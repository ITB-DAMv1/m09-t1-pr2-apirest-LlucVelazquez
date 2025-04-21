using Microsoft.AspNetCore.Identity;

namespace ServerWebAPI.Tools
{
	public static class RoleTools
	{
		public static async Task CreateRolesInitials(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			string[] roles = { "Admin", "User" };
			foreach ( var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}
}
