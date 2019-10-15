namespace RandomPersonLib

/// Enum representing a person's gender.
type Gender =
| Male   = 1
| Female = 2

/// Enum representing a person's country.
type Country =
| Denmark     = 1
| Finland     = 2
| Iceland     = 3
| Netherlands = 4
| Norway      = 5
| Sweden      = 6
| USA         = 7

/// Enum representing a person's creditcard issuer.
type CardIssuer =
| AmericanExpress = 1
| DinersClub      = 2
| Discover        = 3
| MasterCard      = 4
| Visa            = 5

/// Enum representing how the birthdate should be generated.
type BirthDateMode =
| DefaultCalendarYearRange = 1
| ManualCalendarYearRange  = 2
| ManualAgeRange           = 3
| Manual                   = 4
