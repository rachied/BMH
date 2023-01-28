using BuyMyHouse.Application.Dto;
using BuyMyHouse.Application.Dto.Error;
using LanguageExt;

namespace BuyMyHouse.Application.Service;

public interface IMortgageApplicationService
{
    public Task<Either<BmhError, MortgageApplicationResponseDto>> SubmitApplication(MortgageApplicationRequestDto mortgageApplicationRequest);
}