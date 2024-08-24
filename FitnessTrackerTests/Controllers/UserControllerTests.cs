
using FitnessTracker.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FitnessTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using AutoMapper;
using FitnessTracker.Interface;
using FitnessTracker.Mapper;
using Microsoft.AspNetCore.Mvc;
using FitnessTracker.Model;
using FitnessTracker.DTO;

namespace FitnessTracker.Controllers.Tests
{
	public class UserControllerTests
	{
		private readonly Mock<IUserRepository> _mockRepo;
		private readonly Mock<ILogger<UserController>> _mockLogger;
		private readonly IMapper _mapper;
		private readonly UserController _controller;
		public UserControllerTests()
		{
			_mockRepo = new Mock<IUserRepository>();
			_mockLogger = new Mock<ILogger<UserController>>();
			var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
			_mapper = config.CreateMapper();
			_controller = new UserController(_mockRepo.Object, _mapper, _mockLogger.Object);
		}

		[Fact]
		public async Task GetUser_ReturnsOkResult_WithListOfUser()
		{
			// Arrange
			var userProfiles = new List<User> { new User { Id = 1, Name = "John Doe" } };
			_mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(userProfiles);

			// Act
			var result = await _controller.GetUser();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<List<User>>(okResult.Value);
			Assert.Single(returnValue);
		}

		[Fact]
		public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

			// Act
			var result = await _controller.GetByIdAsync(1);

			// Assert
			Assert.IsType<NotFoundResult>(result.Result);
		}

		[Fact]
		public async Task GetUser_ReturnsOkResult_WithUser()
		{
			// Arrange
			var user = new User { Id = 1, Name = "John Doe" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

			// Act
			var result = await _controller.GetByIdAsync(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<User>(okResult.Value);
			Assert.Equal(user.Id, returnValue.Id);
		}

		[Fact]
		public async Task CreateUser_ReturnsCreatedAtActionResult_WithUser()
		{
			// Arrange
			var userDto = new UserDTO { Name = "John Doe", Weight = 70, Height = 175, BirthDate = new DateTime(1990, 1, 1) };
			var user = _mapper.Map<User>(userDto);
			_mockRepo.Setup(repo => repo.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.CreateUser(userDto);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
			var returnValue = Assert.IsType<User>(createdAtActionResult.Value);
			Assert.Equal(user.Name, returnValue.Name);
		}

		[Fact]
		public async Task UpdateUser_ReturnsBadRequest_WhenIdMismatch()
		{
			// Arrange
			var user = new User { Id = 1, Name = "John Doe" };

			// Act
			var result = await _controller.UpdateUser(2, user);

			// Assert
			Assert.IsType<BadRequestResult>(result);
		}

		[Fact]
		public async Task UpdateUser_ReturnsNoContent_WhenUpdateIsSuccessful()
		{
			// Arrange
			var user = new User { Id = 1, Name = "John Doe" };
			_mockRepo.Setup(repo => repo.UpdateAsync(user)).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.UpdateUser(1, user);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task DeleteUser_ReturnsNoContent_WhenDeleteIsSuccessful()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.DeleteUser(1);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}
	}
}
