using FitnessTracker.Model;

namespace FitnessTracker.Interface
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAllAsync();
		Task<User> GetByIdAsync(int id);
		Task AddAsync(User userProfile);
		Task UpdateAsync(User userProfile);
		Task DeleteAsync(int id);
	}
}
