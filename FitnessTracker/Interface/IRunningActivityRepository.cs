using FitnessTracker.Model;

namespace FitnessTracker.Interface
{
	public interface IRunningActivityRepository
	{
		Task<IEnumerable<RunningActivity>> GetAllAsync();
		Task<RunningActivity> GetByIdAsync(int id);
		Task AddAsync(RunningActivity runningActivity);
		Task UpdateAsync(RunningActivity runningActivity);
		Task DeleteAsync(int id);
	}
}
