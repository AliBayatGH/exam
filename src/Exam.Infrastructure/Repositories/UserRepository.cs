using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Exam.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Exam.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ExamDBContext _dbContext;

        public UserRepository(ExamDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.Id);
            existingUser.NationalCode=user.NationalCode;
            existingUser.PhoneNumber=user.PhoneNumber;
            existingUser.Name= user.Name;
            existingUser.LastName = user.LastName;
            await _dbContext.SaveChangesAsync();
        }
    }
}
