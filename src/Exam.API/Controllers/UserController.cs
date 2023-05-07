using AutoMapper;
using Exam.Application.DTOs;
using Exam.Application.Interfaces;
using Exam.Application.Validator;
using Exam.Domain.Entities;
using Exam.Infrastructure.Wrapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {       
        public IUserService UserService { get; set; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }


        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await UserService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User Id Is Invalid.");
            }

            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(UserCreateDto data)
        {
            
            var res = await UserService.CreateUserAsync(data);
            if (res.Succeeded)
                return Ok("User Created");
            return BadRequest(res);
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult> UpdateUser(UserUpdateDto data)
        {
           var updateResult = await UserService.UpdateUserAsync(data);
            if (updateResult.Succeeded)
                return Ok("User Updated Successfully.");
            return Ok(updateResult);
        }
    }
}
