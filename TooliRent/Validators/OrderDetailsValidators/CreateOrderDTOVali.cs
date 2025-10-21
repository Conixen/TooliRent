using FluentValidation;
using TooliRent.DTO_s.OrderDetailsDTOs;
namespace TooliRent.Validators.OrderDetailsValidators
{
    public class CreateOrderDTOVali : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOVali() 
        {
            RuleFor(x => x.Date2Hire)
                .NotEmpty().WithMessage("Hire date is required")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Hire date cannot be in the past");

            RuleFor(x => x.Date2Return)
                .NotEmpty().WithMessage("Return date is required")
                .GreaterThan(x => x.Date2Hire).WithMessage("Return date must be after hire date");

            RuleFor(x => x.ToolId)
                .GreaterThan(0).WithMessage("Valid tool must be selected");

        }
    }
}
