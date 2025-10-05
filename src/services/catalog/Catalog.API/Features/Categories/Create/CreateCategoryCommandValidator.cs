using FluentValidation;
using Catalog.API.Features.Categories.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required")
        .Length(4,60).WithMessage("Name must be  4-60 characters");
    }
}
