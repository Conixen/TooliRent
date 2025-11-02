using FluentValidation;
using TooliRent.DTOs.AuthDTOs;


namespace TooliRent.Validators.AuthValidators
{
    public class LogInDTOVali : AbstractValidator<LogInDTO>
    {
        public LogInDTOVali()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
