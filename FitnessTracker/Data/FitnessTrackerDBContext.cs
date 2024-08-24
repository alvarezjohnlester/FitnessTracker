using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Model;

namespace FitnessTracker.Data
{
	public class FitnessTrackerDBContext : DbContext
	{
		public FitnessTrackerDBContext(DbContextOptions<FitnessTrackerDBContext> options)
			: base(options)
		{
		}

		public DbSet<User> User { get; set; } = default!;
		public DbSet<RunningActivity> RunningActivity { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
					 .HasMany(u => u.RunningActivities)
					 .WithOne()
					 .HasForeignKey(r => r.UserID);
			// Apply global query filters for soft delete
			modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
			modelBuilder.Entity<RunningActivity>().HasQueryFilter(r => !r.IsDeleted);
		}

	}
}
