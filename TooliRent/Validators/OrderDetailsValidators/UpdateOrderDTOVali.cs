using FluentValidation;
using TooliRent.DTO_s.OrderDetailsDTOs;
namespace TooliRent.Validators.OrderDetailsValidators
{
    public class UpdateOrderDTOVali : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOVali()
        {
            RuleFor(x => x.Date2Hire)
                .NotEmpty();


            RuleFor(x => x.Date2Return)
                .NotEmpty();

            RuleFor(x => x.ToolId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
