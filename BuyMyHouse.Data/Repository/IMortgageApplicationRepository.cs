using BuyMyHouse.Domain.Mortgages;

namespace BuyMyHouse.Data.Repository;

public interface IMortgageApplicationRepository
{
    public Task<Guid> CreateMortgageApplication(MortgageApplication entity);

    public Task<List<MortgageApplication>> GetPendingApplications();

    public Task UpdateApplication(MortgageApplication entity);
}