namespace BuyMyHouse.Domain.Mortgages;

public enum ApplicationStatus
{
    Pending = 1,
    OfferReady = 2,
    OfferNotAvailable = 3,
    OfferRejected = 4,
    OfferAccepted = 5,
    Archived = 6,
}