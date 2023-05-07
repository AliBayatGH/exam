using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Application.DTOs;
using Exam.Domain.Entities;
using FluentValidation;

namespace Exam.Application.Validator
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.LastName).NotEmpty();
            RuleFor(user => user.NationalCode)
                .NotEmpty()
                .Length(10)
                .Matches("^[0-9]+$")
                .WithMessage("National code must be 10 digits.");
            RuleFor(user => user.PhoneNumber)
                .NotEmpty()
                .Matches("^[0-9]+$")
                .WithMessage("Phone number must be numeric.")
                .Matches(@"^09\d{9}$")
                .WithMessage("Phone number must start with 09 and have 11 digits.");
        }
    }
}
