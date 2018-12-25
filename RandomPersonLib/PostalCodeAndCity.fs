module PostalCodeCityState

/// A class representing postal code, city and state of a person's address.
[<NoEquality;NoComparison>]
type PostalCodeCityState(postalCode: string, city: string, state: string) =
    member __.PostalCode = postalCode
    member __.City = city
    member __.State = state
