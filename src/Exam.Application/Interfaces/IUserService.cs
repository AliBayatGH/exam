using Exam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Application.DTOs;
using Exam.Infrastructure.Wrapper;

namespace Exam.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<TResponse<IEnumerable<string>>> CreateUserAsync(UserCreateDto doctor);
        Task<TResponse<IEnumerable<string>>> UpdateUserAsync(UserUpdateDto doctor);
    }
}
