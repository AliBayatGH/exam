using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.DTOs;
using Exam.Application.Interfaces;
using Exam.Application.Validator;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Exam.Infrastructure.Wrapper;

namespace Exam.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<TResponse<IEnumerable<string>>> CreateUserAsync(UserCreateDto userToCreate)
        {
            var validator = new UserCreateDtoValidator();
            var result = await validator.ValidateAsync(userToCreate);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage);
                return new TResponse<IEnumerable<string>>(errors)
                    { Succeeded = false, Message = "Inputs Invalid" };
            }
            var user = _mapper.Map<User>(userToCreate);

            
            var creationresult = await _userRepository.CreateAsync(user);
            return new TResponse<IEnumerable<string>>(){Succeeded=true,Message = $"id = {creationresult}" ,Data=new List<string>(){creationresult.ToString()}};
        }

       

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<TResponse<IEnumerable<string>>> UpdateUserAsync(UserUpdateDto data)
        {
            var validator = new UserUpdateDtoValidator();
            var result = await validator.ValidateAsync(data);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage);
                return new TResponse<IEnumerable<string>>(errors)
                    { Succeeded = false, Message = "Inputs Invalid" };
            }
            var user = _mapper.Map<User>(data);

            await _userRepository.UpdateAsync(user);
            return new TResponse<IEnumerable<string>>() { Succeeded = true, Message = "Successfully Updated" };
        }
    }
}
