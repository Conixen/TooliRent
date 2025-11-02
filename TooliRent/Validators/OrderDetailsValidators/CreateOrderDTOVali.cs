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

            RuleFor(x => x.ToolIds)
              .NotEmpty().WithMessage("At least one tool must be selected")
              .Must(list => list != null && list.Count > 0).WithMessage("Tool list cannot be empty");
        }
    }
}
