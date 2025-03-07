using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class RatingValidator : AbstractValidator<RatingDTO>
{
    public RatingValidator()
    {
        RuleFor(rating => rating.Rate).InclusiveBetween(0, 5);
        RuleFor(rating => rating.Count).GreaterThanOrEqualTo(0);
    }
}