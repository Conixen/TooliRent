using FluentValidation;
using TooliRent.DTO_s.CategoryDTOs;


namespace TooliRent.Validators.CategoryValidators
{
    public class CreateCategoryDTOVali : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOVali()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MinimumLength(2).WithMessage("Category name must be at least 2 characters")
                .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(255).WithMessage("Description cannot exceed 255 characters");
        }
    }
}
