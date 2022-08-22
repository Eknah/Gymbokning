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
				FirstName = "Admin",
				LastName = "Adminsson",
				EmailConfirmed = true
			};

			await userManager.CreateAsync(adminUser, "Zxcvbn3#");

			await userManager.AddToRoleAsync(adminUser, "Admin");

			var normalUser = new ApplicationUser()
			{
				Email = "sven@svenssion.se",
				UserName = "sven@svenssion.se",
				FirstName = "Sven",
				LastName = "Svensson",
				EmailConfirmed = true
			};

			await userManager.CreateAsync(normalUser, "Zxcvbn3#");

			var gymClasses = AddGymClasses();

			db.AddRange(gymClasses);

			await db.SaveChangesAsync();
		}

		private static List<GymClass> AddGymClasses()
		{
			var gymClasses = new List<GymClass>();

			gymClasses.Add(new GymClass()
			{
				Name = "Gympass A",
				Description = "Ett gympass",
				StartTime = DateTime.Now.AddDays(-2),
				Duration = new TimeSpan(1, 10, 0)
			});

			gymClasses.Add(new GymClass()
			{
				Name = "Gympass B",
				Description = "Ett till gympass",
				StartTime = DateTime.Now.AddDays(-1),
				Duration = new TimeSpan(1, 20, 0)
			});

			gymClasses.Add(new GymClass()
			{
				Name = "Gympass A",
				Description = "Ännu ett gympass",
				StartTime = DateTime.Now.AddDays(0),
				Duration = new TimeSpan(1, 30, 0)
			});

			gymClasses.Add(new GymClass()
			{
				Name = "Gympass C",
				Description = "Ytterligare ett gympass",
				StartTime = DateTime.Now.AddDays(1),
				Duration = new TimeSpan(0, 50, 0)
			});

			gymClasses.Add(new GymClass()
			{
				Name = "Gympass D",
				Description = "Ett sista gympass",
				StartTime = DateTime.Now.AddDays(2),
				Duration = new TimeSpan(1, 10, 0)
			});

			return gymClasses;
		}
	}
}
