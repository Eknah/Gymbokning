using System.ComponentModel.DataAnnotations;

namespace Gymbokning.Models.ViewModels
{
	public class GymClassesIndexViewModel
	{
		public List<Gymbokning.Models.GymClass> OldGymClasses { get; set; } = new();
		public List<Gymbokning.Models.GymClass> UpcomingGymClasses { get; set; } = new();
	}
}
