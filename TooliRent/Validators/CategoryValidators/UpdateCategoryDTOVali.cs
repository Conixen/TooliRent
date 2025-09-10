using FluentValidation;
using TooliRent.DTO_s.CategoryDTOs;

namespace TooliRent.Validators.CategoryValidators
{
    public class UpdateCategoryDTOVali : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDTOVali()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
