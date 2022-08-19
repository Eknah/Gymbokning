using Microsoft.AspNetCore.Identity;

namespace Gymbokning.Models
{
	public class ApplicationUser : IdentityUser
	{
		// Navigation properties

		public List<ApplicationUserGymClass> GymPasses { get; set; } = new();
	}
}
