using Exam.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Domain.Entities;
using Exam.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Exam.Test.IntegrationTests
{
    public class UserRepositoryIntegrationTests : IDisposable
    {
        private readonly ExamDBContext _dbContext;
        private readonly UserRepository _userRepository;

        public UserRepositoryIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ExamDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            _dbContext = new ExamDBContext(options);
            _userRepository = new UserRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateUser()
        {
            // Arrange
            var user = new User
            {
                Name = "Sina",
                LastName = "Ajilyan",
                NationalCode = "3720241785",
                PhoneNumber = "09189721725"
            };

            // Act
            var userId = await _userRepository.CreateAsync(user);
            var result = await _userRepository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.NationalCode, result.NationalCode);
            Assert.Equal(user.PhoneNumber, result.PhoneNumber);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser()
        {
            // Arrange
            var user = new User
            {
                Name = "Sina",
                LastName = "Ajilyan",
                NationalCode = "3720241785",
                PhoneNumber = "09189721725"
            };
            var userId = await _userRepository.CreateAsync(user);

            // Act
            var result = await _userRepository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.NationalCode, result.NationalCode);
            Assert.Equal(user.PhoneNumber, result.PhoneNumber);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            // Arrange
            var user = new User
            {
                Name = "Sina",
                LastName = "Ajilyan",
                NationalCode = "3720241785",
                PhoneNumber = "09189721725"
            };
            var userId = await _userRepository.CreateAsync(user);

            var updatedUser = new User
            {
                Id = userId,
                Name = "ali",
                LastName = "bayat",
                NationalCode = "3750241786",
                PhoneNumber = "09129721726"
            };

            // Act
            await _userRepository.UpdateAsync(updatedUser);
            var result = await _userRepository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Name, result.Name);
            Assert.Equal(updatedUser.LastName, result.LastName);
            Assert.Equal(updatedUser.NationalCode, result.NationalCode);
            Assert.Equal(updatedUser.PhoneNumber, result.PhoneNumber);
        }
    }

}
