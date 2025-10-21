using FluentValidation;
using TooliRent.DTO_s.OrderDetailsDTOs;
namespace TooliRent.Validators.OrderDetailsValidators
{
    public class UpdateOrderDTOVali : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOVali()
        {
            RuleFor(x => x.Date2Hire)
                .NotEmpty().WithMessage("Hire date is required");

            RuleFor(x => x.Date2Return)
                .NotEmpty().WithMessage("Return date is required")
                .GreaterThan(x => x.Date2Hire).WithMessage("Return date must be after hire date");

            RuleFor(x => x.ToolId)
                .GreaterThan(0).WithMessage("Valid tool must be selected");
        }
    }
}
