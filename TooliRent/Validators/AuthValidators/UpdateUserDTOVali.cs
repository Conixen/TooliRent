using FluentValidation;
using TooliRent.DTOs.AuthDTOs;
namespace TooliRent.Validators.AuthValidators
{
    public class UpdateUserDTOVali : AbstractValidator<UpdateUserDTO>
    {
       public UpdateUserDTOVali()
       {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(75).WithMessage("Last name cannot exceed 75 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(150).WithMessage("Email cannot exceed 150 characters");
        }
    }
}
