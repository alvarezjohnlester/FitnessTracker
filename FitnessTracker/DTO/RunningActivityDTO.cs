namespace FitnessTracker.DTO
{
	public class RunningActivityDTO
	{
		public string Location { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public double Distance { get; set; }
		public string DistanceUnitType { get; set; }
		public int UserID { get; set; }
	}
}
