using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitnessTracker.Model
{
	public class RunningActivity 
	{
		[Key]
		public int Id { get; set; }
		public string Location { get; set; }
		public double Distance { get; set; }
		public string DistanceUnitType { get; set; } = string.Empty;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public TimeSpan Duration => EndTime - StartTime;
		public double AveragePace => Duration.TotalMinutes / Distance;
		public int UserID { get; set; }
		[JsonIgnore]
		public bool IsDeleted { get; set; }
	}
}
