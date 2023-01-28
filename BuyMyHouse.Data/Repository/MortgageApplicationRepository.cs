using System.Data;
using BuyMyHouse.Domain.Mortgages;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BuyMyHouse.Data.Repository;

public class MortgageApplicationRepository : IMortgageApplicationRepository
{
    private readonly IDbConnection _connection;

    public MortgageApplicationRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Guid> CreateMortgageApplication(MortgageApplication entity)
    {
        entity.ApplicationId = Guid.NewGuid();
        entity.ApplicationStatus = ApplicationStatus.Pending;
        
        await _connection.InsertAsync(entity);

        return entity.ApplicationId;
    }

    public async Task<List<MortgageApplication>> GetPendingApplications()
    {
        const string query = "SELECT * FROM [dbo].[MortgageApplications] WHERE ApplicationStatus = @Status";
        var result = await _connection.QueryAsync<MortgageApplication>(query, new { Status = ApplicationStatus.Pending });

        return result.ToList();
    }

    public async Task UpdateApplication(MortgageApplication entity)
    {
        await _connection.UpdateAsync(entity);
    }
}