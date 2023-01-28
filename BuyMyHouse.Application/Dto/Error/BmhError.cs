using FluentValidation.Results;

namespace BuyMyHouse.Application.Dto.Error;

public class BmhError
{
    public BmhError(ValidationResult validationResult)
    {
        Type = BmhErrorType.ValidationError;
        ValidationResult = validationResult;
        Message = string.Empty;
    }
    
    public BmhError(BmhErrorType type, string message)
    {
        Type = type;
        Message = message;
    }

    public ValidationResult? ValidationResult { get; init; }
    public BmhErrorType Type { get; init; }
    public string Message { get; init; }
}