using System.Threading.Tasks;
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BuyMyCompany.AzureFunctions;

public static class MortgageAppOfferProcessingQueueTrigger
{
    [FunctionName("MortgageAppOfferProcessingQueueTrigger")]
    public static async Task RunAsync([QueueTrigger("mortgage-app-offer-processing-queue", Connection = "AzureWebJobsStorage")] string mortgageAppJson, ILogger log)
    {
        log.LogInformation($"Generating an offer for Mortgage application: {mortgageAppJson}");
        
    }
}