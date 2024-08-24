using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Data;
using FitnessTracker.Model;
using AutoMapper;
using FitnessTracker.Interface;
using FitnessTracker.DTO;

namespace FitnessTracker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RunningActivityController : ControllerBase
    {
		private readonly IRunningActivityRepository _runningActivityRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<RunningActivityController> _logger;

		public RunningActivityController(IRunningActivityRepository runningActivityRepository, IMapper mapper, ILogger<RunningActivityController> logger)
		{
			_runningActivityRepository = runningActivityRepository;
			_mapper = mapper;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<RunningActivity>>> GetRunningActivities()
		{
			_logger.LogInformation("Getting all running activities");
			var runningActivities = await _runningActivityRepository.GetAllAsync();
			return Ok(runningActivities);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<RunningActivity>> GetRunningActivity(int id)
		{
			_logger.LogInformation($"Getting running activity with ID {id}");
			var runningActivity = await _runningActivityRepository.GetByIdAsync(id);

			if (runningActivity == null)
			{
				_logger.LogWarning($"Running activity with ID {id} not found");
				return NotFound();
			}

			return Ok(runningActivity);
		}

		[HttpPost]
		public async Task<ActionResult<RunningActivity>> PostRunningActivity(RunningActivityDTO runningActivityDto)
		{
			_logger.LogInformation("Adding a new running activity");
			var runningActivity = _mapper.Map<RunningActivity>(runningActivityDto);
			await _runningActivityRepository.AddAsync(runningActivity);
			return CreatedAtAction(nameof(GetRunningActivity), new { id = runningActivity.Id }, runningActivity);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutRunningActivity(int id, RunningActivity runningActivity)
		{
			if (id != runningActivity.Id)
			{
				_logger.LogWarning($"Running activity ID mismatch: {id} != {runningActivity.Id}");
				return BadRequest();
			}

			_logger.LogInformation($"Updating running activity with ID {id}");
			await _runningActivityRepository.UpdateAsync(runningActivity);
			_logger.LogInformation($"Updated");
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRunningActivity(int id)
		{
			_logger.LogInformation($"Deleting running activity with ID {id}");
			await _runningActivityRepository.DeleteAsync(id);
			_logger.LogInformation($"Deleted");

			return NoContent();
		}

	}
}
