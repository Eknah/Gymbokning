namespace Gymbokning.Models
{
	public class ApplicationUserGymClass
	{
		public string ApplicationUserId { get; set; }
		public int GymClassId { get; set; }

		// Navigation properties
		public ApplicationUser Member { get; set; }
		public GymClass GymClass { get; set; }
	}
}
