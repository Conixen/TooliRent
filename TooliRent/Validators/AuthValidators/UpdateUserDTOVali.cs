using FluentValidation;
using TooliRent.DTOs.AuthDTOs;
namespace TooliRent.Validators.AuthValidators
{
    public class UpdateUserStatusDTOVali : AbstractValidator<UpdateUserStatusDTO>
    {
        public UpdateUserStatusDTOVali()
        {
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive status is required");
        }
    }
}
