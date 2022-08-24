#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Gymbokning.Models
{
	public class GymClass
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Gymclass title must have a length between {2} and {1}")]
		[Display(Name = "Gym Class Title")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Start Time")]
		public DateTime StartTime { get; set; }

		[Required]
		[Display(Name = "Duration")]
		public TimeSpan Duration { get; set; }

		public DateTime EndTime { get { return StartTime + Duration; } }

		public string Description { get; set; }

		// Navigation properties

		public List<ApplicationUserGymClass> AttendingMembers { get; set; } = new();
	}
}
