using BuyMyHouse.Data.Repository;
using BuyMyHouse.Domain.Mortgages;
using Microsoft.Extensions.Logging;

namespace BuyMyHouse.Application.Service;

public interface IMortgageOfferService
{
    public Task ProcessApplication(MortgageApplication mortgageApplication);
}

public class MortgageOfferService : IMortgageOfferService
{
    private readonly ILogger _logger;
    private readonly IMortgageOfferCalculator _calculator;
    private readonly IMortgageApplicationRepository _appRepository;
    private readonly IMortgageOfferRepository _offerRepository;

    public MortgageOfferService(
        IMortgageOfferCalculator calculator, 
        IMortgageApplicationRepository appRepository, 
        IMortgageOfferRepository offerRepository, 
        ILogger<MortgageOfferService> logger)
    {
        _calculator = new MortgageOfferCalculator(new OfferCalculatorConfig());
        _appRepository = appRepository;
        _offerRepository = offerRepository;
        _logger = logger;
    }

    private decimal GetCurrentMarketInterestRate() => 6.7M;

    public async Task ProcessApplication(MortgageApplication mortgageApplication)
    {
        var result = _calculator.CalculateMortgageOffer(mortgageApplication, GetCurrentMarketInterestRate());

        await result.Right(async offer =>
        {
            _logger.LogInformation("Persisting offer for mortgage application {AppId}", mortgageApplication.ApplicationId);
            mortgageApplication.ApplicationStatus = ApplicationStatus.OfferReady;
            await _appRepository.UpdateApplication(mortgageApplication);
            await _offerRepository.SaveOffer(offer);
        }).Left(async failure =>
        {
            _logger.LogWarning("Mortgage application {AppId} was denied with reason {Reason}", mortgageApplication.ApplicationId, failure.Reason);
            mortgageApplication.ApplicationStatus = ApplicationStatus.OfferNotAvailable;
            await _appRepository.UpdateApplication(mortgageApplication);
        });
    }
}