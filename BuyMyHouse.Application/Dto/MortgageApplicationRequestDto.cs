using BuyMyHouse.Domain.Mortgages;

namespace BuyMyHouse.Application.Dto;

public class MortgageApplicationRequestDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public string? Email { get; set; }
    public decimal? PersonalYearlyIncomeEuros { get; set; }
    public decimal? SpouseYearlyIncomeEuros { get; set; }

    public bool? HasPermanentEmployment { get; set; }
    public decimal? CurrentTotalDebt { get; set; }


    public MortgageApplication MapToEntity()
    {
        return new MortgageApplication()
        {
            ApplicationId = Guid.Empty,
            FirstName = FirstName!,
            LastName = LastName!,
            Email = Email!,
            PersonalYearlyIncomeEuros = PersonalYearlyIncomeEuros ?? throw new ArgumentNullException(nameof(PersonalYearlyIncomeEuros)),
            SpouseYearlyIncomeEuros = SpouseYearlyIncomeEuros ?? decimal.Zero,
            HasPermanentEmployment = HasPermanentEmployment ?? throw new ArgumentException(nameof(HasPermanentEmployment)),
            CurrentTotalDebt = CurrentTotalDebt ?? decimal.Zero,
        };
    }
}