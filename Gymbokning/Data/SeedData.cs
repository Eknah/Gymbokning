using Gymbokning.Models;
using Microsoft.AspNetCore.Identity;

namespace Gymbokning.Data
{
	public class SeedData
	{
		public static async Task InitAsync(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });

			var adminUser = new ApplicationUser()
			{
				Email = "admin@Gymbokning.se",
				UserName = "admin@Gymbokning.se",
				EmailConfirmed = true
			};

			await userManager.CreateAsync(adminUser, "Zxcvbn3#");

			await userManager.AddToRoleAsync(adminUser, "Admin");
		}
	}
}
