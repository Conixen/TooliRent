using FluentValidation;
using TooliRent.DTO_s.ToolsDTOs;

namespace TooliRent.Validators.ToolValidators
{
    public class CreateToolDTOVali : AbstractValidator<CreateToolDto>
    {
        public CreateToolDTOVali()
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

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.PricePerDay)
                .GreaterThan(0).WithMessage("Price per day must be greater than 0")
                .LessThanOrEqualTo(10000).WithMessage("Price per day cannot exceed 10000");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Valid category must be selected");

            RuleFor(x => x.SerialNumber)
                .MaximumLength(100).WithMessage("Serial number cannot exceed 100 characters")
                .When(x => !string.IsNullOrEmpty(x.SerialNumber));
        }
    }
}
