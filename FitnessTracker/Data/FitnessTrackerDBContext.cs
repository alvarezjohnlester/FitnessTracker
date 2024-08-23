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
        public FitnessTrackerDBContext (DbContextOptions<FitnessTrackerDBContext> options)
            : base(options)
        {
		}

        public DbSet<User> User { get; set; } = default!;
    }
}
