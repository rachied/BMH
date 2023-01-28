using FluentValidation;

namespace BuyMyHouse.Domain.Mortgages;

public class HealthyFinancialsValidator : AbstractValidator<MortgageApplication>
{
    public HealthyFinancialsValidator(OfferCalculatorConfig config)
    {
        RuleFor(x => x.CurrentTotalDebt)
            .LessThanOrEqualTo(config.MaxDebt);

        RuleFor(app => app)
            .Must(x => x.PersonalYearlyIncomeEuros > x.CurrentTotalDebt)
            .WithMessage("Personal yearly income must be greater than current total debt");
        
        RuleFor(app => app)
            .Must(x => x.PersonalYearlyIncomeEuros + x.SpouseYearlyIncomeEuros > config.MinimalIncomeRequired)
            .WithMessage("Combined income does not meet minimum requirements");

        RuleFor(app => app)
            .Must(app => app.PersonalYearlyIncomeEuros > config.MinimalIncomeRequired * 2)
            .When(app => app.HasPermanentEmployment == false)
            .WithMessage("Personal income and/or Employment status does not meet minimum requirements");
    }
}