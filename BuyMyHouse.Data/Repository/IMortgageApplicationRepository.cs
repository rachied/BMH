using BuyMyHouse.Data.Entity;

namespace BuyMyHouse.Data.Repository;

public interface IMortgageApplicationRepository
{
    public Task<Guid> CreateMortgageApplication(MortgageApplication entity);

    public Task<List<MortgageApplication>> GetPendingApplications();
}