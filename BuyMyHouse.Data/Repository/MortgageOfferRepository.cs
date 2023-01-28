using System.Data;
using BuyMyHouse.Domain.Mortgages;
using Dapper.Contrib.Extensions;

namespace BuyMyHouse.Data.Repository;

public class MortgageOfferRepository : IMortgageOfferRepository
{
    private readonly IDbConnection _connection;

    public MortgageOfferRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task SaveOffer(MortgageOffer offer)
    {
        await _connection.InsertAsync(offer);
    }
}