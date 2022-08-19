namespace Gymbokning.Models
{
	public class ApplicationUserGymClass
	{
		public int ApplicationUserId { get; set; }
		public int GymClassId { get; set; }

		// Navigation properties
		public ApplicationUser ApplicationUse { get; set; }
		public GymClass GymClass { get; set; }
	}
}
