using FitnessTracker.Data;
using FitnessTracker.Interface;
using FitnessTracker.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace FitnessTracker.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly FitnessTrackerDBContext _context;

		public UserRepository(FitnessTrackerDBContext context)
		{
			_context = context;
		}
		public async Task AddAsync(User userProfile)
		{
			await _context.User.AddAsync(userProfile);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var userProfile = await _context.User.FindAsync(id);
			if (userProfile != null)
			{
				userProfile.IsDeleted = true;
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			return await _context.User.Include(u => u.RunningActivities).ToListAsync();
		}

		public async Task<User> GetByIdAsync(int id)
		{
			return await _context.User.Include(u => u.RunningActivities).FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task UpdateAsync(User userProfile)
		{
			_context.Entry(userProfile).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
