using FitnessTracker.Data;
using FitnessTracker.Interface;
using FitnessTracker.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace FitnessTracker.Repository
{
	public class RunningActivityRepository : IRunningActivityRepository
	{
		private readonly FitnessTrackerDBContext _context;

		public RunningActivityRepository(FitnessTrackerDBContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<RunningActivity>> GetAllAsync()
		{
			return await _context.RunningActivity.ToListAsync();
		}
		public async Task<RunningActivity> GetByIdAsync(int id)
		{
			return await _context.RunningActivity.FirstOrDefaultAsync(r => r.Id == id);
		}
		public async Task AddAsync(RunningActivity runningActivity)
		{
			await _context.RunningActivity.AddAsync(runningActivity);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(RunningActivity runningActivity)
		{
			_context.Entry(runningActivity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var runningActivity = await _context.RunningActivity.FindAsync(id);
			if (runningActivity != null)
			{
				runningActivity.IsDeleted = true;
				await _context.SaveChangesAsync();
			}
		}
	}
}
