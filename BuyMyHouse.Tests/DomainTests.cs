using BuyMyHouse.Domain.Mortgages;
using FluentAssertions;
using LanguageExt.UnitTesting;

namespace BuyMyHouse.Tests;

public class DomainTests
{
    [Fact]
    public void OfferCalculatorReturnsNoOfferAvailable_When_Debt_Exceeds_Maximum()
    {
        var config = new OfferCalculatorConfig()
        {
            MaxDebt = 100,
        };


        var mortgageApp = new MortgageApplication()
        {
            ApplicationId = Guid.NewGuid(),
            ApplicationStatus = ApplicationStatus.Pending,
            CurrentTotalDebt = config.MaxDebt + 1, // <-- Exceeds the max debt we set previously
            PersonalYearlyIncomeEuros = 100_000,
            HasPermanentEmployment = true,
        };

        var calculator = new MortgageOfferCalculator(config);

        var result = calculator.CalculateMortgageOffer(mortgageApp, decimal.Zero);
        
        // Result should be 'Left' - of type NoOfferAvailable, and have a non-empty Reason property
        result.ShouldBeLeft(noOfferAvailableMsg =>
        {
            noOfferAvailableMsg.Reason.Should().NotBeNullOrEmpty();
        });
    }
}