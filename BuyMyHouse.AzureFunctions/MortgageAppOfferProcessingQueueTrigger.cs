using System;
using System.Text.Json;
using System.Threading.Tasks;
using BuyMyHouse.Application.Service;
using BuyMyHouse.Domain.Mortgages;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BuyMyHouse.AzureFunctions;

public class MortgageAppOfferProcessingQueueTrigger
{
    private readonly IMortgageOfferService _service;

    public MortgageAppOfferProcessingQueueTrigger(IMortgageOfferService service)
    {
        _service = service;
    }

    [FunctionName("MortgageAppOfferProcessingQueueTrigger")]
    public async Task RunAsync([QueueTrigger("mortgage-app-offer-processing-queue", Connection = "AzureWebJobsStorage")] string mortgageAppJson, ILogger log)
    {
        log.LogInformation($"Generating an offer for Mortgage application: {mortgageAppJson}");

        var mortgageApplication = JsonSerializer.Deserialize<MortgageApplication>(mortgageAppJson) 
                                  ?? throw new ApplicationException("Deserializing failed for mortgage app: " + mortgageAppJson);

        await _service.ProcessApplication(mortgageApplication);
    }
}