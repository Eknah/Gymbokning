using Gymbokning.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gymbokning.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<GymClass>? GymClass { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ApplicationUserGymClass>().HasKey(table => new { table.ApplicationUserId, table.GymClassId });
		}
	}
}