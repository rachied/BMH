namespace BuyMyHouse.Domain.Mortgages;

public class MortgageOffer
{
    public Guid MortgageApplicationId { get; set; }

    public decimal MortgageAmount { get; set; }

    public int Years { get; set; }

    public int YearsLockedInterest { get; set; }

    public decimal LockedInterestPercentage { get; set; }

    public decimal MonthlyPaymentAmount { get; set; }

    public DateTime OfferExpiresUtc { get; set; }
}