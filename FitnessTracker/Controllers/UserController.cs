using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Data;
using FitnessTracker.Model;
using FitnessTracker.Interface;
using FitnessTracker.DTO;
using AutoMapper;

namespace FitnessTracker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
    {
		private readonly IUserRepository _userProfileRepository;
		private readonly ILogger<UserController> _logger;
		private readonly IMapper _mapper;
		public UserController(IUserRepository userProfileRepository, IMapper mapper, ILogger<UserController> logger)
        {
			_userProfileRepository = userProfileRepository;
			_mapper = mapper;
			_logger = logger;
		}

		// GET: User
		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUser()
		{
			_logger.LogInformation("Getting all user profiles");
			var userProfiles = await _userProfileRepository.GetAllAsync();
			return Ok(userProfiles);
		}

		// GET: User/5
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetByIdAsync(int id)
		{
			_logger.LogInformation($"Getting user profile with ID {id}");
			var userProfile = await _userProfileRepository.GetByIdAsync(id);

			if (userProfile == null)
			{
				_logger.LogWarning($"User profile with ID {id} not found");
				return NotFound();
			}
			return Ok(userProfile);
		}

		// POST: /User
		[HttpPost]
		public async Task<ActionResult<User>> CreateUser(UserDTO userDTO)
		{
			_logger.LogInformation("Adding a new user profile");
			var userData = _mapper.Map<User>(userDTO);
			await _userProfileRepository.AddAsync(userData);
			_logger.LogInformation("Successfully added");
			return CreatedAtAction(nameof(GetUser), new { id = userData.Id }, userData);
		}

		// PUT: User/5
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, User userProfile)
		{
			if (id != userProfile.Id)
			{
				_logger.LogWarning($"User profile ID mismatch: {id} != {userProfile.Id}");
				return BadRequest();
			}

			_logger.LogInformation($"Updating user profile with ID {id}");
			await _userProfileRepository.UpdateAsync(userProfile);
			_logger.LogInformation($"User profile with ID {id} is updated");
			return NoContent();
		}

		// DELETE: User/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			_logger.LogInformation($"Deleting user profile with ID {id}");
			await _userProfileRepository.DeleteAsync(id);
			_logger.LogInformation($"User profile with ID {id} is deleted");
			return NoContent();
		}

		[HttpGet("test-error")]
		public IActionResult TestError()
		{
			throw new Exception("This is a test exception.");
		}
	}
}
