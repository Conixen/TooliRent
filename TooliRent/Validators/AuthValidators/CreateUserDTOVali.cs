using FluentValidation;
using TooliRent.DTOs.AuthDTOs;
namespace TooliRent.Validators.AuthValidators
{
    public class CreateUserDTOVali : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOVali()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("First name is required and should not exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(75)
                .WithMessage("Last name is required and should not exceed 75 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.");

        }
    }
}
