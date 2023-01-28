use BuyMyHouse
go

create table MortgageApplications
(
    FirstName                 varchar(128)                not null,
    LastName                  varchar(128)                not null,
    Email                     varchar(128)                not null,
    PersonalYearlyIncomeEuros decimal                     not null,
    SpouseYearlyIncomeEuros   decimal                     not null,
    HasPermanentEmployment    tinyint                     not null,
    CurrentTotalDebt          decimal                     not null,
    ApplicationStatus         varchar(128)                not null,
    ApplicationId             uniqueidentifier            not null
        constraint MortgageApplications_pk
            primary key,
    SubmittedAtUtc            datetime2 default getdate() not null
)
go

create table MortgageOffers
(
    MortgageApplicationId    uniqueidentifier not null
        constraint MortgageOffers_pk
            primary key,
    MortgageAmount           decimal          not null,
    Years                    int              not null,
    YearsLockedInterest      int              not null,
    LockedInterestPercentage decimal,
    MonthlyPaymentAmount     decimal,
    OfferExpiresUtc          datetime2        not null
)
go

