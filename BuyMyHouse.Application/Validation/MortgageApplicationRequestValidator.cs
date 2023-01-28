using BuyMyHouse.Application.Dto;
using FluentValidation;

namespace BuyMyHouse.Application.Validation;

public class MortgageApplicationRequestValidator : AbstractValidator<MortgageApplicationRequestDto>
{
    public MortgageApplicationRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("Email address is required.")
            .EmailAddress()
                .WithMessage("A valid email address is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithMessage("First name is required.");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage("Last name is required.");

        RuleFor(x => x.HasPermanentEmployment)
            .NotNull()
                .WithMessage("Employment status is required.");

        RuleFor(x => x.PersonalYearlyIncomeEuros)
            .NotNull()
                .WithMessage("Personal yearly income is required.")
            .GreaterThanOrEqualTo(0)
                .WithMessage("Personal yearly income must be a positive amount.");

        RuleFor(x => x.SpouseYearlyIncomeEuros)
            .Must(x => x is null or >= 0)
                .WithMessage("Spouse yearly income must be a positive amount when entered.");
        
        RuleFor(x => x.CurrentTotalDebt)
            .Must(x => x is null or >= 0)
            .WithMessage("Current total debt must be a positive amount when entered.");
    }
}