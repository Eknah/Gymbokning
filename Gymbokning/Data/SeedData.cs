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

			//await userManager.AddToRoleAsync(adminUser, "Admin");


			//var roles = GenerateRoles();

			//await db.AddRangeAsync(roles);

			//await db.SaveChangesAsync();

			//var users = GenerateUsers();

			//await db.AddRangeAsync(users);

			//await db.SaveChangesAsync();
		}

		//private static IEnumerable<ApplicationUser> GenerateUsers()
		//{
		//	List<ApplicationUser> users = new();

		//	var adminUser = new ApplicationUser()
		//	{
		//		UserName = "admin@Gymbokning.se",
		//	};

		//	var password = new PasswordHasher<ApplicationUser>();
		//	var hashed = password.HashPassword(adminUser, "secret");


		//	users.Add(adminUser);

		//	return users;
		//}

		//private static IEnumerable<IdentityRole> GenerateRoles()
		//{
		//	List<IdentityRole> roles = new();

		//	roles.Add(new IdentityRole()
		//	{
		//		Name = "Admin"
		//	});

		//	return roles;
		//}
	}
}
