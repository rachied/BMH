using FluentValidation.Results;

namespace BuyMyHouse.Domain.Mortgages;

public class NoOfferAvailable
{
    public NoOfferAvailable(ValidationResult validationResult)
    {
        Reason = validationResult.Errors.Select(x => x.ErrorMessage).First();
    }
    
    public string Reason { get; set; }
}