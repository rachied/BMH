using BuyMyHouse.Domain.Mortgages;

namespace BuyMyHouse.Data.Repository;

public interface IMortgageOfferRepository
{
    public Task SaveOffer(MortgageOffer offer);
}