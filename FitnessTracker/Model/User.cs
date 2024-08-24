using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Model
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public double Weight { get; set; }
		public string WeightUnitType { get; set; } = string.Empty;
		public double Height { get; set; }
		public string HeightUnitType { get; set; }
		public DateTime BirthDate { get; set; }
		[NotMapped]
		public int Age => DateTime.Now.Year - BirthDate.Year;
		[NotMapped]
		public double BMI => Weight / Math.Pow(Height / 100, 2);

		public ICollection<RunningActivity> RunningActivities { get; set; }
		public bool IsDeleted { get; set; }
	}

}
