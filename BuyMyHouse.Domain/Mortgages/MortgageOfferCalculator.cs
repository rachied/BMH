using LanguageExt;

namespace BuyMyHouse.Domain.Mortgages;

public interface IMortgageOfferCalculator
{
    Either<NoOfferAvailable, MortgageOffer> CalculateMortgageOffer(MortgageApplication application, decimal currentInterestRatePercentage);
}

public class MortgageOfferCalculator : IMortgageOfferCalculator
{
    private readonly OfferCalculatorConfig _config;

    public MortgageOfferCalculator(OfferCalculatorConfig config)
    {
        _config = config;
    }

    public Either<NoOfferAvailable, MortgageOffer> CalculateMortgageOffer(MortgageApplication application, decimal currentInterestRatePercentage)
    {
        var validator = new HealthyFinancialsValidator(_config);

        var validationResult = validator.Validate(application);

        if (!validationResult.IsValid)
        {
            return new NoOfferAvailable(validationResult);
        }

        var combinedIncome = application.PersonalYearlyIncomeEuros + application.SpouseYearlyIncomeEuros;
        var debtPenalty = application.CurrentTotalDebt * 2;

        var mortgageAmount = (combinedIncome * 4) - debtPenalty;

        return new MortgageOffer()
        {
            LockedInterestPercentage = currentInterestRatePercentage,
            YearsLockedInterest = _config.DefaultLockedYears,
            MortgageAmount = mortgageAmount,
            MonthlyPaymentAmount = mortgageAmount / (_config.DefaultTotalYears * 12),
            Years = _config.DefaultTotalYears,
            MortgageApplicationId = application.ApplicationId,
            OfferExpiresUtc = DateTime.UtcNow.AddDays(2),
        };
    }
    
    
}