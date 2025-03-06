using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class NameValidator : AbstractValidator<NameDTO>
{
    public NameValidator()
    {
        RuleFor(user => user.Firstname)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Firstname must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Firstname cannot be longer than 50 characters.");
        
        RuleFor(user => user.Lastname)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Lastname must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Lastname cannot be longer than 50 characters.");
    }
}