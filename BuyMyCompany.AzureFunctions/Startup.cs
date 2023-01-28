using System.Data;
using System.Data.SqlClient;
using System.IO;
using BuyMyHouse.Application.Service;
using BuyMyHouse.Data.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
[assembly: FunctionsStartup(typeof(BuyMyCompany.AzureFunctions.Startup))]
namespace BuyMyCompany.AzureFunctions;


public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = builder.GetContext().Configuration;
        
        builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(config.GetConnectionString("DbConnection")));
        builder.Services.AddTransient<IMortgageApplicationRepository, MortgageApplicationRepository>();
        builder.Services.AddTransient<IMortgageApplicationService, MortgageApplicationService>();
        builder.Services.AddLogging();
    }
    
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);

        var context = builder.GetContext();

        builder.ConfigurationBuilder
            .AddJsonFile(
                Path.Combine(context.ApplicationRootPath, "appsettings.json"),
                optional: true, reloadOnChange: false)
            .AddJsonFile(
                Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"),
                optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
    }
}