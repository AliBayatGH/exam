using Exam.Application.DTOs;
using FluentValidation;

namespace Exam.Application.Validator;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(user => user.Id).GreaterThan(0);
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