using FluentValidation;
using TooliRent.DTO_s.CategoryDTOs;


namespace TooliRent.Validators.CategoryValidators
{
    public class CreateCategoryDTOVali : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOVali()
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
