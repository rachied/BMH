using BuyMyHouse.Application.Dto;
using BuyMyHouse.Application.Dto.Error;
using BuyMyHouse.Application.Validation;
using BuyMyHouse.Data.Entity;
using BuyMyHouse.Data.Repository;
using LanguageExt;

namespace BuyMyHouse.Application.Service;

public class MortgageApplicationService : IMortgageApplicationService
{
    private readonly IMortgageApplicationRepository _repository;

    public MortgageApplicationService(IMortgageApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Either<BmhError, MortgageApplicationResponseDto>> SubmitApplication(MortgageApplicationRequestDto mortgageApplicationRequest)
    {
        var validator = new MortgageApplicationRequestValidator();

        var validationResult = await validator.ValidateAsync(mortgageApplicationRequest);

        if (!validationResult.IsValid)
        {
            return new BmhError(validationResult);
        }

        var entity = mortgageApplicationRequest.MapToEntity();

        var mortgageAppId = await _repository.CreateMortgageApplication(entity);
        
        return new MortgageApplicationResponseDto()
        {
            Message = $"Successfully submitted your mortgage application ({mortgageAppId}). " +
                      "Check your mailbox the next morning for your personalized offer!"
        };
    }
}