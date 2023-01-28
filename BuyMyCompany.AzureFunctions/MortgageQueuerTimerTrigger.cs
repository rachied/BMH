using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using BuyMyHouse.Data.Repository;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;

namespace BuyMyCompany.AzureFunctions;

public class MortgageQueuerTimerTrigger
{
    private readonly IMortgageApplicationRepository _repository;

    public MortgageQueuerTimerTrigger(IMortgageApplicationRepository repository)
    {
        _repository = repository;
    }

    [FunctionName("MortgageQueuerTimerTrigger")]
    public async Task RunAsync([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log, [Queue("mortgage-app-offer-processing-queue", Connection = "AzureWebJobsStorage")] QueueClient queueClient)
    {
        log.LogInformation($"Nightly Mortgage Batch queuer triggered at: {DateTime.UtcNow}");

        var pendingApplications = await _repository.GetPendingApplications();
        
        log.LogInformation($"Found {pendingApplications.Count} pending mortgage applications");

        var tasks = pendingApplications
            .Select(x => JsonSerializer.Serialize(x))
            .Select(queueClient.SendMessageAsync);

        await Task.WhenAll(tasks);
        
        log.LogInformation($"Finished queueing {pendingApplications.Count} mortgage applications");
    }
}