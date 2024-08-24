using AutoMapper;
using FitnessTracker.Controllers;
using FitnessTracker.DTO;
using FitnessTracker.Interface;
using FitnessTracker.Mapper;
using FitnessTracker.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FitnessTrackerTests.Controllers
{
	public class RunningActivityControllerTests
	{
		private readonly Mock<IRunningActivityRepository> _mockRepo;
		private readonly Mock<ILogger<RunningActivityController>> _mockLogger;
		private readonly IMapper _mapper;
		private readonly RunningActivityController _controller;

		public RunningActivityControllerTests()
		{
			_mockRepo = new Mock<IRunningActivityRepository>();
			_mockLogger = new Mock<ILogger<RunningActivityController>>();
			var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
			_mapper = config.CreateMapper();
			_controller = new RunningActivityController(_mockRepo.Object, _mapper, _mockLogger.Object);
		}

		[Fact]
		public async Task GetRunningActivities_ReturnsOkResult_WithListOfRunningActivities()
		{
			// Arrange
			var runningActivities = new List<RunningActivity> { new RunningActivity { Id = 1, Location = "Park" } };
			_mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(runningActivities);

			// Act
			var result = await _controller.GetRunningActivities();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<List<RunningActivity>>(okResult.Value);
			Assert.Single(returnValue);
		}

		[Fact]
		public async Task GetRunningActivity_ReturnsNotFound_WhenRunningActivityDoesNotExist()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((RunningActivity)null);

			// Act
			var result = await _controller.GetRunningActivity(1);

			// Assert
			Assert.IsType<NotFoundResult>(result.Result);
		}

		[Fact]
		public async Task GetRunningActivity_ReturnsOkResult_WithRunningActivity()
		{
			// Arrange
			var runningActivity = new RunningActivity { Id = 1, Location = "Park" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(runningActivity);

			// Act
			var result = await _controller.GetRunningActivity(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<RunningActivity>(okResult.Value);
			Assert.Equal(runningActivity.Id, returnValue.Id);
		}

		[Fact]
		public async Task PostRunningActivity_ReturnsCreatedAtActionResult_WithRunningActivity()
		{
			// Arrange
			var runningActivityDto = new RunningActivityDTO { Location = "Park", StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Distance = 5, UserProfileId = 1 };
			var runningActivity = _mapper.Map<RunningActivity>(runningActivityDto);
			_mockRepo.Setup(repo => repo.AddAsync(It.IsAny<RunningActivity>())).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.CreateRunningActivity(runningActivityDto);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
			var returnValue = Assert.IsType<RunningActivity>(createdAtActionResult.Value);
			Assert.Equal(runningActivity.Location, returnValue.Location);
		}

		[Fact]
		public async Task PutRunningActivity_ReturnsBadRequest_WhenIdMismatch()
		{
			// Arrange
			var runningActivity = new RunningActivity { Id = 1, Location = "Park" };

			// Act
			var result = await _controller.UpdateRunningActivity(2, runningActivity);

			// Assert
			Assert.IsType<BadRequestResult>(result);
		}

		[Fact]
		public async Task PutRunningActivity_ReturnsNoContent_WhenUpdateIsSuccessful()
		{
			// Arrange
			var runningActivity = new RunningActivity { Id = 1, Location = "Park" };
			_mockRepo.Setup(repo => repo.UpdateAsync(runningActivity)).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.UpdateRunningActivity(1, runningActivity);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task DeleteRunningActivity_ReturnsNoContent_WhenDeleteIsSuccessful()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.DeleteRunningActivity(1);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}
	}
}
