namespace BuyMyHouse.Domain.Mortgages;

public class OfferCalculatorConfig
{
    public decimal MaxDebt { get; set; } = 100_000;

    public decimal MinimalIncomeRequired { get; set; } = 45_000;

    public int DefaultLockedYears { get; set; } = 5;

    public int DefaultTotalYears { get; set; } = 30;
}