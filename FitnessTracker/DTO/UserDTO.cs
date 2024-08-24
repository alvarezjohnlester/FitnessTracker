namespace FitnessTracker.DTO
{
	public class UserDTO
	{
		public string Name { get; set; }
		public double Weight { get; set; }
		public string WeightUnitType { get; set; }
		public double Height { get; set; } 
		public string HeightUnitType { get; set; } 
		public DateTime BirthDate { get; set; }
	}
}
