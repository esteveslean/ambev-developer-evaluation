using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;


public class AddressValidator : AbstractValidator<AddressDTO>
{
    public AddressValidator()
    {
        RuleFor(address => address.City)
            .NotEmpty()
            .MinimumLength(3).WithMessage("City must be at least 3 characters long.")
            .MaximumLength(65).WithMessage("City cannot be longer than 50 characters.");
        
        RuleFor(address => address.Street)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Street must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Street cannot be longer than 50 characters.");
        
        RuleFor(address => address.Number)
            .GreaterThan(0).WithMessage("Number must be greater than 0.")
            .LessThan(10000).WithMessage("Number must be less than 10,000.");
        
        RuleFor(a => a.Zipcode)
            .NotEmpty()
            .Matches(@"^\d{5}-\d{3}$").WithMessage("Zipcode must be in format 12345-678");
    }
}