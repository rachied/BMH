using System.Threading.Tasks;
using BuyMyHouse.Application.Dto;
using BuyMyHouse.Application.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuyMyHouse.AzureFunctions;

public class MortgageAppSubmissionHttpTrigger
{
    private readonly IMortgageApplicationService _service;
    
    public MortgageAppSubmissionHttpTrigger(IMortgageApplicationService service)
    {
        _service = service;
    }
    
    [FunctionName("MortgageAppSubmissionHttpTrigger")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
    {
        log.LogInformation("Received a new mortgage application!");
        var parseResult = req.DeserializeModel<MortgageApplicationRequestDto>();

        return await parseResult
            .Some(async dto => await HandleRequestDto(dto))
            .None(async () => await Task.FromResult(new BadRequestResult()));
        
    }

    private async Task<IActionResult> HandleRequestDto(MortgageApplicationRequestDto mortgageApplication)
    {
        var result = await _service.SubmitApplication(mortgageApplication);

        var httpResponse = result
            .Right<IActionResult>(x => new OkObjectResult(x))
            .Left(err => err.ToActionResult());

        return httpResponse;
    }
}