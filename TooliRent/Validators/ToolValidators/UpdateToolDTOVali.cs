using FluentValidation;
using TooliRent.DTO_s.ToolsDTOs;

namespace TooliRent.Validators.ToolValidators
{
    public class UpdateToolDTOVali : AbstractValidator<UpdateToolDTO>
    {
        public UpdateToolDTOVali() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tool name is required")
                .MaximumLength(100).WithMessage("Tool name cannot exceed 100 characters");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required")
                .MaximumLength(50).WithMessage("Brand cannot exceed 50 characters");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required")
                .MaximumLength(50).WithMessage("Model cannot exceed 50 characters");

            RuleFor(x => x.SerialNumber)
                .MaximumLength(100).WithMessage("Serial number cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.PricePerDay)
                 .NotEmpty().WithMessage("Price per day is required")
                 .GreaterThan(0).WithMessage("Price per day must be a positive value");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required")
                .GreaterThan(0).WithMessage("Category ID must be a positive integer");
        }
    }
}
