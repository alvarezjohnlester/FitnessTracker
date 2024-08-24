using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FitnessTracker.Data;
using FitnessTracker.Interface;
using FitnessTracker.Repository;
using FitnessTracker.Middleware;
namespace FitnessTracker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<FitnessTrackerDBContext>(options =>
			    options.UseSqlServer(builder.Configuration.GetConnectionString("FitnessTrackerDBContext") ?? throw new InvalidOperationException("Connection string 'FitnessTrackerDBContext' not found.")));

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Configure logging
			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();
			builder.Logging.AddDebug();

			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IRunningActivityRepository, RunningActivityRepository>();

			// Register AutoMapper
			builder.Services.AddAutoMapper(typeof(Program));

			var app = builder.Build();



			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			else
			{
				app.UseExceptionHandler("/error");
			}

			app.UseMiddleware<CustomExceptionMiddleware>();

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
