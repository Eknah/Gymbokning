using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Gymbokning.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "{0} must have a length between {2} and {1}")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "{0} must have a length between {2} and {1}")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		public string FullName => $"{FirstName} {LastName}";

		[Required]
		public DateTime TimeOfRegistration { get; set; }

		// Navigation properties 

		public List<ApplicationUserGymClass> AttendedGymPasses { get; set; } = new();
	}
}
