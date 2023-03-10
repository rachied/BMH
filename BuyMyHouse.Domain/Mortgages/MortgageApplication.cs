using Dapper.Contrib.Extensions;

namespace BuyMyHouse.Domain.Mortgages;

public class MortgageApplication
{
    [Key]
    public Guid ApplicationId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Email { get; set; }
    public decimal PersonalYearlyIncomeEuros { get; set; }
    public decimal SpouseYearlyIncomeEuros { get; set; }

    public bool HasPermanentEmployment { get; set; }
    public decimal CurrentTotalDebt { get; set; }

    public ApplicationStatus ApplicationStatus { get; set; }
}