using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.DTOs;
using Exam.Application.Interfaces;
using Exam.Application.Services;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Exam.Infrastructure.DBContext;
using Exam.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Exam.Test.IntegrationTests
{
    public class UserServiceIntegrationTests : IDisposable
    {
        private readonly ExamDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public UserServiceIntegrationTests()
        {
            // Set up the database context
            var options = new DbContextOptionsBuilder<ExamDBContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
            _dbContext = new ExamDBContext(options);

            // Set up the AutoMapper profile
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            // Set up the user repository
            _userRepository = new UserRepository(_dbContext);

            // Set up the user service
            _userService = new UserService(_userRepository, _mapper);
        }

        [Fact]
        public async Task CreateUserAsync_ValidInput_ReturnsSuccessResultWithId()
        {
            // Arrange
            var userToCreate = new UserCreateDto
            {
                Name = "sina",
                LastName = "Ajilyan",
                NationalCode = "3720241785",
                PhoneNumber = "09189721725"
            };

            // Act
            var result = await _userService.CreateUserAsync(userToCreate);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Single(result.Data);
            Assert.True(int.TryParse(result.Data.First(), out int id));
            var createdUser = await _userRepository.GetByIdAsync(id);
            Assert.NotNull(createdUser);
            Assert.Equal(userToCreate.Name, createdUser.Name);
            Assert.Equal(userToCreate.LastName, createdUser.LastName);
            Assert.Equal(userToCreate.NationalCode, createdUser.NationalCode);
            Assert.Equal(userToCreate.PhoneNumber, createdUser.PhoneNumber);
        }

        [Fact]
        public async Task CreateUserAsync_InvalidInput_ReturnsFailedResultWithErrors()
        {
            // Arrange
            var userToCreate = new UserCreateDto
            {
                Name = "sina",
                LastName = "Ajilyan",
                NationalCode = "789",
                PhoneNumber = "1234568"
            };

            // Act
            var result = await _userService.CreateUserAsync(userToCreate);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Inputs Invalid", result.Message);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserExists_ReturnsUser()
        {
            // Arrange
            var userToCreate = new UserCreateDto
            {
                Name = "sina",
                LastName = "Ajilyan",
                NationalCode = "3720241785",
                PhoneNumber = "09189721725"
            };
            var createdUserId = await _userService.CreateUserAsync(userToCreate);

            // Act
            var result = await _userService.GetUserByIdAsync(int.Parse(createdUserId.Data.First()));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userToCreate.Name, result.Name);
            Assert.Equal(userToCreate.LastName, result.LastName);
            Assert.Equal(userToCreate.NationalCode, result.NationalCode);
            Assert.Equal(userToCreate.PhoneNumber, result.PhoneNumber);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var userId = 12345;

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserAsync_Should_Update_User()
        {
            // Arrange
            var user = new User
            {
                Name = "sina",
                LastName = "Ajilyan",
                NationalCode = "3720241785",
                PhoneNumber = "09189721725"
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var updatedUser = new UserUpdateDto
            {
                Id = user.Id,
                Name = "ali",
                LastName = "Bayat",
                NationalCode = "3720241786",
                PhoneNumber = "09189781726"
            };

            // Act
            var response = await _userService.UpdateUserAsync(updatedUser);

            // Assert
            Assert.True(response.Succeeded);
            Assert.Equal("Successfully Updated", response.Message);

            var result = await _dbContext.Users.FindAsync(user.Id);
            Assert.NotNull(result);
            Assert.Equal("ali", result.Name);
            Assert.Equal("Bayat", result.LastName);
        }


    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
